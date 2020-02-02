using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Canvas))]
public class CanvasScaleChanger : MonoBehaviour
{
    Canvas canvasScaler;
    public ModSection modSection;
    [Range(0, 1)]
    [SerializeField]
    float outputPeview;
    float lastInput;
    float lastOutput;

    public void SetCanvasScale(float f)
    {
        lastInput = f;
    }
    void InternalSetCanvasScale(float f)
    {
        if (Mathf.Abs(f - lastOutput) < 0.01f) return;
        lastOutput = f;
        if (canvasScaler == null) canvasScaler = GetComponentInParent<Canvas>();
        canvasScaler.scaleFactor = f;
        // Debug.Log(canvasScaler.scaleFactor);
    }
    void Update()
    {
        outputPeview = modSection.ProcessValue(lastInput);
        InternalSetCanvasScale(outputPeview);
    }
}
