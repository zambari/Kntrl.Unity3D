using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[System.Serializable]
public class ModSection
{
    [Range(0, 1)]
    // [HideInInspector]
    public float lastInputValue;
    [Header("Curve")]
    public AnimationCurve animationCurve = zExt.LinearCurve();

    public bool useCurve;
    [Header("Damper")]

    public Damper damper = new Damper();
    public bool useDamper;

    [Header("Delay")]
    public DelayValue delayValue = new DelayValue(.5f);

    public bool useDelay;



    [Header("Ranges")]

    public Vector2 inputMap = new Vector2(0, 1);
    public bool useInputMap;

    public Vector2 outputRange = new Vector2(0, 1);

    public Vector2 clampMinMax = new Vector2(0, 1);
    public bool invert;
    public bool square;

    [Range(0, 1)]
    public float lastOutputValue;
    public void OnValidate()
    {
        delayValue.OnValidate();
    }

    public float ProcessValue(float f)
    {
        lastInputValue = f;
        if (useInputMap)
            f = inputMap.Map(f);
        if (useCurve)
            f = animationCurve.Evaluate(f);
        if (useDamper)
        {
            damper.targetValue = f;
            f = damper.UpdatedValue();
        }
        if (useDelay)
        {
            delayValue.EnqueueValue(f);
            f = delayValue.OutputValue();
        }

        // if (useOuputRange)
        f = outputRange.Map(f);
        if (square) f *= f;

        if (f < clampMinMax.x) f = clampMinMax.x;
        if (f > clampMinMax.y) f = clampMinMax.y;
        if (invert) f = clampMinMax.y - f;
        lastOutputValue = f;
        return f;
    }

}
