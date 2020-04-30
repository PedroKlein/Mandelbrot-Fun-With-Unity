using TMPro;
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

    private bool isInputOn = false;
    private enum Options { ImaginaryPlane, MandelbrotSet, JuliaSet};

    private void Start()
    {
        material = mandelbrotMaterial;

        image = GetComponent<RawImage>();
    }
    private void FixedUpdate()
    {
        if(isInputOn)
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

    public void SetInputs(bool status)
    {
        isInputOn = status;
    }

    public void SetShader(bool status)
    {
        image.enabled = status;
    }

    public void SetJuliaSetMode()
    {
        image.material = juliaSetMaterial;
        material = juliaSetMaterial;
    }

    public void SetMandelbrotMode()
    {
        image.material = mandelbrotMaterial;
        material = mandelbrotMaterial;
    }

    public bool GetShaderStatus()
    {
        return image.enabled;
    }

    public void SetShaderIterations(int iterations)
    {
        material.SetInt("_Iteretions", iterations);
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

    public void ResetPosAndZoom()
    {
        position = new Vector2(0, 0);
        scale = 4;
    }
}
    
