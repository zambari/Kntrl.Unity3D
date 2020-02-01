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
    [Header("Damper")]

    public Damper damper = new Damper();
    public bool useDamper;

    [Header("Delay")]
    public DelayValue delayValue = new DelayValue(.5f);

    public bool useDelay;

    [Header("Curve")]
    public AnimationCurve animationCurve = zExt.LinearCurve();

    public bool useCurve;

    [Header("Ranges")]

    public Vector2 inputMap = new Vector2(0, 1);
    public bool useInputMap;

    public Vector2 outputRange = new Vector2(0, 1);

    public Vector2 clampMinMax = new Vector2(0, 1);
    public bool invert;
    public bool square;

    [Range(0, 1)]
    public float lastOutputValue;

    public float ProcessValue(float f)
    {
        lastInputValue = f;
        if (useInputMap)
            f = inputMap.Map(f);
        if (useDamper)
        {
            f = damper.UpdatedValue();
            damper.targetValue = f;
        }
        if (useDelay)
        {
            delayValue.EnqueueValue(f);
            f = delayValue.OutputValue();
        }
        if (useCurve)
            f = animationCurve.Evaluate(f);
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
