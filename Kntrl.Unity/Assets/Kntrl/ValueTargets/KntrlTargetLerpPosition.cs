using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlTargetLerpPosition : KntrlTargetterBase
{

    public Vector3 start;
    public Vector3 end;
    [Header("Check to enable editing")]
    public bool dontApply;
    [ExposeMethodInEditor]
    void SaveStart()
    {
        start = transform.localPosition;
        dontApply = false;
    }
    [ExposeMethodInEditor]
    void SaveEnd()
    {
        end = transform.localPosition;
        dontApply = false;
    }
    public override void ApplyValue(float f)
    {
        if (!dontApply)
            transform.localPosition = Vector3.Lerp(start, end, f);
    }
    void Reset()
    {
        SaveStart();
        SaveEnd();

    }
    [ExposeMethodInEditor]
    void SaveOffset()
    {
        Vector3 target = Vector3.Lerp(start, end, inputSelector.GetValue());
        Vector3 delta = transform.localPosition - target;
        start += delta;
        end += delta;
    }
}
