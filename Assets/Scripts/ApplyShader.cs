using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ApplyShader : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] bool applyShader = false;

    [SerializeField] Vector2 position = new Vector2(0,0);
    [SerializeField] float scale = 4;

    private Vector2 smoothPosition;
    private float smoothScale;
    private RawImage image;

    private void Start()
    {
        image = GetComponent<RawImage>();
        ToggleShader(applyShader);
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

    public void ToggleShader(bool toggle)
    {
        image.enabled = toggle;
    }

    public bool GetShaderStatus()
    {
        return image.enabled;
    }

    public void SetZPosition(Vector2 pos)
    {
        material.SetVector("_ZPos", new Vector4(pos.x, pos.y, 0, 0));
    }
}
    
