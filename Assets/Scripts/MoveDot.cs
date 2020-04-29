using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDot : MonoBehaviour
{
    [SerializeField] Material mandelbrotMat;
    [SerializeField] bool isZ = false;

    private bool selected;
    MandelbrotGen mandelbrot;
    ApplyShader applyShader;

    void Start()
    {
        mandelbrot = FindObjectOfType<MandelbrotGen>();
        applyShader = FindObjectOfType<ApplyShader>();
    }

    void Update()
    {
        if (selected)
        {     
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;           
            mandelbrot.GenerateSequence();
            if (Input.GetMouseButtonUp(0))
            {
                selected = false;
            }

            if (applyShader.GetShaderStatus() && isZ)
            {
                applyShader.SetZPosition(transform.position);
            }
        }      
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0,0,0);
        mandelbrot.GenerateSequence();
        if (applyShader.GetShaderStatus() && isZ)
        {
            applyShader.SetZPosition(transform.position);
        }
    }

}
