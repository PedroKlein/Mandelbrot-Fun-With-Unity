﻿using UnityEngine;

public class MoveDot : MonoBehaviour
{
    [SerializeField] Material mandelbrotMat;
    [SerializeField] bool isZ = false;

    private bool selected;
    private MandelbrotGen mandelbrot;
    private ApplyShader applyShader;

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

            if (Input.GetMouseButtonUp(0))
            {
                selected = false;
            }

            CheckAndUpdateOption();
        }
    }

    private void CheckAndUpdateOption()
    {
        if (applyShader.GetShaderStatus())
        {
            if (isZ)
                applyShader.SetZPosition(transform.position);
            else
                applyShader.SetCPosition(transform.position);
        }
        else
            mandelbrot.GenerateSequence();
            
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

        CheckAndUpdateOption();
    }

}
