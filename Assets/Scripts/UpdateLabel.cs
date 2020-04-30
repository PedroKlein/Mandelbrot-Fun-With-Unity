using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLabel : MonoBehaviour
{
    [SerializeField] Text sliderValueText;

    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateSliderValueText(float value)
    {
        sliderValueText.text = value.ToString();
    }

}
