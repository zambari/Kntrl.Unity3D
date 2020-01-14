using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

https://github.com/zambari/Kntrl.Unity3D

*/

[ExecuteInEditMode]
public class KntrlValue : KntrlValueBase
{
    Slider slider;
    void Reset()
    {
        slider = GetComponent<Slider>();
        if (slider != null)
        {
            processing.applyProcessed = false;
            SetValue(slider.value);
        }

    }
    // bool ant
    void Awake()
    {
        slider = GetComponent<Slider>();
        if (slider != null)
            slider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    bool antiFeedback;
    void OnSliderValueChanged(float f)
    {
        // if (antiFeedback) return;
        if (!externalSource)
        {
            // if (!processing.applyProcessed && )
            // antiFeedback = true;
            //if ( !processing.applyProcessed) 
            SetValue(f);
            // antiFeedback = false;
        }

    }
    public override void ApplyValue(float f)
    {
        if (slider != null)
            if (processing.applyToSlider || externalSource)
            {
                if (processing.applyProcessed)
                {
                    slider.value = f;
                    //_currentValue=f;
                }
                else
                {
                    slider.value = _currentValue;
                }
            }
    }
    protected override void OnValidate()
    {
        if (slider == null) slider = GetComponent<Slider>();
        base.OnValidate();
    }


}
