using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMode : MonoBehaviour
{
    [SerializeField] GameObject viewObjects;
    [SerializeField] ApplyShader shaderControler;
    [SerializeField] GameObject explorerMode;
    [SerializeField] MandelbrotGen mandelbrotGen;

    [SerializeField] MoveDot cPoint;
    [SerializeField] MoveDot zPoint;


    private enum Options { ImaginaryPlane, MandelbrotSet, JuliaSet };

    private void Start()
    {
        explorerMode.SetActive(false);
        explorerMode.GetComponent<Toggle>().isOn = false;
    }
    public void UpdateOptions(int option)
    {
        shaderControler.ResetPosAndZoom();

        switch (option)
        {
            case (int)Options.ImaginaryPlane:
                cPoint.ResetPosition();
                zPoint.SetPosition(new Vector3(0.5f, -0.2f, 0f));

                shaderControler.SetShader(false);
                explorerMode.SetActive(false);
                explorerMode.GetComponent<Toggle>().isOn = false;
                break;
            case (int)Options.MandelbrotSet:
                explorerMode.SetActive(true);
                shaderControler.SetShader(true);
                shaderControler.SetMandelbrotMode();

                zPoint.ResetPosition();
                cPoint.SetPosition(new Vector3(-0.5f, 0.2f, 0f));
                break;
            case (int)Options.JuliaSet:
                explorerMode.SetActive(true);
                shaderControler.SetShader(true);
                shaderControler.SetJuliaSetMode();

                cPoint.ResetPosition();
                zPoint.SetPosition(new Vector3(0.5f, -0.2f, 0f));
                break;
            default:
                break;
        }
    }

    public void ToggleExploreMode(bool toggle)
    {
        shaderControler.ResetPosAndZoom();
        shaderControler.SetInputs(toggle);
        viewObjects.SetActive(!toggle);
    }

    public void UpdateLineIterations(float value)
    {
        mandelbrotGen.SetIteration((int)value);
    }

    public void UpdateShaderIterations(float value)
    {
        shaderControler.SetShaderIterations((int)value);
    }
}
