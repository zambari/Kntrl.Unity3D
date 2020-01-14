using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlTargetLerpPosition : KntrlValueBase
{

    public Vector3 start;
    public Vector3 end;
    [ExposeMethodInEditor]
    void SaveStart()
    {
        start = transform.localPosition;
    }
    [ExposeMethodInEditor]
    void SaveEnd()
    {
        end = transform.localPosition;

    }
    public override void ApplyValue(float f)
    {
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
        Vector3 target = Vector3.Lerp(start, end, _currentValue);
        Vector3 delta = transform.localPosition - target;
        start += delta;
        end += delta;
    }
}
