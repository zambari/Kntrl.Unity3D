using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

https://github.com/zambari/Kntrl.Unity3D

*/

[ExecuteInEditMode]
public class KntrlValue : MonoBehaviour, IKntrlValueSource, IKntrlValueTarget, IKntrlGetProcessors
{


    [Header("Drag other Kntrl")]
    public KntrlSelector inputSelector = new KntrlSelector();
    TransitionVisualizer transitionVisualizer;
    public enum SliderUseMode { none, readSourceValueFromSlider, writeInputToSlider, writeProcessed, auto };
    public SliderUseMode sliderUseMode = SliderUseMode.auto;

    Slider slider;
    IKntrlProcessValue[] processors;
    [ReadOnly] [SerializeField] int totalProcessors;
    [ReadOnly] [SerializeField] int activeProcessors;

    void OnEnable()
    {
        GetProcessors();
    }
    public void GetProcessors()
    {
        processors = GetComponents<IKntrlProcessValue>();
        totalProcessors = processors.Length;
        activeProcessors = 0;

    }
    void Reset()
    {
        if (name=="GameObject") name="KntrlValue "+zExt.RandomString(4);
        if (name.Contains("Slider")) name+=" (KntrlValue)";
 
        transitionVisualizer = GetComponent<TransitionVisualizer>();
        if (transitionVisualizer == null) transitionVisualizer = gameObject.AddComponent<TransitionVisualizer>();
        slider = GetComponent<Slider>();
        if (slider != null)
        {
            sliderUseMode = SliderUseMode.auto;
            // inputSelector.referenceGameObject = slider.gameObject;
        }

        // #if UNITY_EDITOR
        //         for (int i = 0; i < 10; i++)
        //             UnityEditorInternal.ComponentUtility.MoveComponentUp(this);
        // #endif
        OnValidate();

    }

    float ProcessValue(float f)
    {

        activeProcessors = 0;
        try
        {
            for (int i = 0; i < processors.Length; i++)
            {
                if (processors[i] == null)
                {
                    GetProcessors();
                    return ProcessValue(f);
                }
                if (processors[i].enabled)
                {
                    f = processors[i].GetProcessedValue(f);
                    activeProcessors++;
                }
            }
        }
        catch (System.Exception e)
        {
            GetProcessors();
            //    throw (e);
        }
        return f;
    }

    protected virtual void OnValidate()
    {
        if (inputSelector.referenceGameObject == gameObject)
        {
        }

        inputSelector.OnValidate(this);

        UpateTransitionVisualiser();

        if (slider == null)
            slider = GetComponent<Slider>();

        if (slider == null)
            sliderUseMode = SliderUseMode.none;
        else
        {
         //   sliderUseMode = SliderUseMode.auto;
        }

        if (sliderUseMode == SliderUseMode.auto)
        {
            float currentSliderValue = slider.value;
            thisInputValue = inputSelector.GetValue();
            if (currentSliderValue != thisInputValue)
                slider.value = thisInputValue;

        }
    }

    void UpateTransitionVisualiser()
    {
        if (transitionVisualizer == null) transitionVisualizer = gameObject.GetComponent<TransitionVisualizer>();
        if (transitionVisualizer != null)
        {
            if (inputSelector.referenceGameObject == null)
                transitionVisualizer.sourceTransform = null;
            else
                transitionVisualizer.sourceTransform = inputSelector.referenceGameObjectTransform;
        }
    }
    float lastValueSetSliderValue;
    float thisInputValue;
    float lastInputValue;
    void Update()
    {
        thisInputValue = inputSelector.GetValue();
        if (thisInputValue != lastInputValue)
        {
            SetSliderValue(thisInputValue);
        }
        lastInputValue = thisInputValue;
        if (sliderUseMode == SliderUseMode.auto)//||
        {
            inputSelector.currentValueInput = slider.value;
        }

        if (sliderUseMode == SliderUseMode.readSourceValueFromSlider)//||
        {
            inputSelector.currentValueInput = slider.value;
        }
        else
        {
            // thisInputValue = inputSelector.GetValue();
        }
        // lastOutputValue = ProcessValue(input);
        if (sliderUseMode == SliderUseMode.writeInputToSlider)
            SetSliderValue(thisInputValue);
        else
     if (sliderUseMode == SliderUseMode.writeProcessed)
            SetSliderValue(inputSelector.modSection.lastOutputValue);

    }
    void SetSliderValue(float f)
    {
        if (f == lastValueSetSliderValue) return;
        if (slider == null) return;
        slider.value = f;
    }

    public float GetValue()
    {
        return inputSelector.modSection.lastOutputValue;
    }

    public void SetValue(float f)
    {
        inputSelector.SetValue(f);
    }
}
