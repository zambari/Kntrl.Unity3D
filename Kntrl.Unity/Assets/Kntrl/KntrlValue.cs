using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

https://github.com/zambari/Kntrl.Unity3D

*/

[ExecuteInEditMode]
public class KntrlValue : KntrlValueBaseInternal //IKntrlValueTarget,
{

    public enum SliderUseMode { none, readSourceValueFromSlider, writeInputToSlider, writeProcessed, auto };
    public SliderUseMode sliderUseMode = SliderUseMode.auto;

    Slider slider;
    float lastValueSetSliderValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (slider == null) slider = GetComponent<Slider>();
    }

    protected override void Reset()
    {
        base.Reset();
        slider = GetComponent<Slider>();
        if (slider != null)
        {
            sliderUseMode = SliderUseMode.auto;
        }
        if (name.Contains("Slider")) name += " (KntrlValue)";
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (slider == null)
            slider = GetComponent<Slider>();

        if (slider == null)
            sliderUseMode = SliderUseMode.none;
    }

    void Update()
    {
        float thisInputValue = inputSelector.GetValue();
        outputPreview = ProcessValue(inputSelector.currentValueOutput);
        if (slider != null)
        {
            float currentSliderValue = slider.value;

            if ((sliderUseMode == SliderUseMode.auto && inputSelector.valueChangedInInspector) || sliderUseMode == SliderUseMode.writeInputToSlider || sliderUseMode == SliderUseMode.writeInputToSlider)
            {
                SetSliderValue(inputSelector.currentValueInput);
            }

            if (sliderUseMode == SliderUseMode.writeProcessed)
            {
                // outputPreview= ProcessValue(inputSelector.currentValueInput);
                SetSliderValue(outputPreview);
            }
            else
            {
                if (currentSliderValue != thisInputValue) // slider was moved
                {
                    inputSelector.currentValueInput = slider.value;
                }
            }
        }
       

    }
    void SetSliderValue(float f)
    {
        if (slider == null) return;
        if (f == slider.value) return;
        lastValueSetSliderValue = f;
        slider.value = f;
    }


}
