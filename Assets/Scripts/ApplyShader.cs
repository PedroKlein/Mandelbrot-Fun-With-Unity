using UnityEngine;
using UnityEngine.UI;

public class ApplyShader : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material mandelbrotMaterial;
    [SerializeField] Material juliaSetMaterial;

    [Header("Options")]
    [SerializeField] Vector2 position = new Vector2(0,0);
    [SerializeField] float scale = 4;

    private Vector2 smoothPosition;
    private float smoothScale;

    private RawImage image;
    private Material material;

    private MeshRenderer meshSphere;
    private MandelbrotGen mandelbrotGen;
    private enum Options { ImaginaryPlane, MandelbrotSet, JuliaSet};

    private void Start()
    {
        material = mandelbrotMaterial;

        mandelbrotGen = FindObjectOfType <MandelbrotGen>();
        meshSphere = FindObjectOfType<MeshRenderer>();
        image = GetComponent<RawImage>();
    }
    private void FixedUpdate()
    {
        HandleInput();
        UpdateShader();
    }

    private void UpdateShader()
    {
        smoothPosition = Vector2.Lerp(smoothPosition, position, .05f);
        smoothScale = Mathf.Lerp(smoothScale, scale, .05f);

        float aspect = Screen.width / (float)Screen.height;

        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect < 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        material.SetVector("_Area", new Vector4(smoothPosition.x, smoothPosition.y, scaleX, scaleY));
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
            scale *= 0.99f;
            
        else if (Input.GetKey(KeyCode.LeftShift))
            scale *= 1.01f;

        if (Input.GetKey(KeyCode.A))
            position.x -= .01f * scale;

        if (Input.GetKey(KeyCode.D))
            position.x += .01f * scale;

        if (Input.GetKey(KeyCode.S))
            position.y -= .01f * scale;

        if (Input.GetKey(KeyCode.W))
            position.y += .01f * scale;
    }

    public void UpdateOptions(int option)
    {

        switch (option)
        {
            case (int)Options.ImaginaryPlane:

                mandelbrotGen.SetLineRenderer(true);
                meshSphere.enabled = true;
                image.enabled = false;
                break;
            case (int)Options.MandelbrotSet:

                mandelbrotGen.SetLineRenderer(false);
                meshSphere.enabled = false;
                image.enabled = true;
                image.material = mandelbrotMaterial;
                material = mandelbrotMaterial;            
                break;
            case (int)Options.JuliaSet:

                mandelbrotGen.SetLineRenderer(false);
                meshSphere.enabled = false;
                image.enabled = true;
                image.material = juliaSetMaterial;
                material = juliaSetMaterial;
                break;
            default:
                break;
        }
    }

    public bool GetShaderStatus()
    {
        return image.enabled;
    }

    public void SetZPosition(Vector2 pos)
    {
        if(material.Equals(mandelbrotMaterial))
            material.SetVector("_ZPos", new Vector4(pos.x, pos.y, 0, 0));
    }

    public void SetCPosition(Vector2 pos)
    {
        if(material.Equals(juliaSetMaterial))
            material.SetVector("_CPos", new Vector4(pos.x, pos.y, 0, 0));
    }
}
    
