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
    public enum SliderUseMode { none, readSourceValueFromSlider, writeInputToSlider, writeProcessed };
    public SliderUseMode sliderUseMode;

    Slider mySlider;
    IKntrlProcessValue[] processors;
    [ReadOnly] int totalProcessors;
    [ReadOnly] int activeProcessors;

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

        transitionVisualizer = GetComponent<TransitionVisualizer>();
        if (transitionVisualizer == null) transitionVisualizer = gameObject.AddComponent<TransitionVisualizer>();
        mySlider = GetComponent<Slider>();
        if (mySlider != null)
        {
            sliderUseMode = SliderUseMode.readSourceValueFromSlider;
            // inputSelector.referenceGameObject = mySlider.gameObject;
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
        if (mySlider == null)
            mySlider = GetComponent<Slider>();
        // if (mySlider != null && mySlider.value != inputSelector.currentValueInput)
        // {
        //     mySlider.value = inputSelector.currentValueInput;

        // }
        if (mySlider == null)
            sliderUseMode = SliderUseMode.none;
        UpateTransitionVisualiser();
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
    [SerializeField]

    void Update()
    {
        if (sliderUseMode == SliderUseMode.readSourceValueFromSlider)
        {
            inputSelector.currentValueInput = mySlider.value;
        }
        else
        {
            // lastInputValue = inputSelector.GetValue();
        }
        var input = inputSelector.GetValue();
        // lastOutputValue = ProcessValue(input);
        if (sliderUseMode == SliderUseMode.writeInputToSlider)
            SetSliderValue(input);
        else
     if (sliderUseMode == SliderUseMode.writeProcessed)
            SetSliderValue(inputSelector.modSection.lastOutputValue);

    }
    void SetSliderValue(float f)
    {
        if (f == lastValueSetSliderValue) return;
        mySlider.value = f;
    }

    public float GetValue()
    {
        return inputSelector.modSection.lastOutputValue;
    }

    public void SetValue(float f)
    {//will ignore slider
        inputSelector.SetValue(f);
    }
}
