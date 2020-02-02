using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[ExecuteInEditMode]
public class KntrlTargetUniversal : KntrlTargetterBase
{

    public bool alwaysUpdate = true;
    public FloatEvent driverEvent = new FloatEvent();
    float lastValue = -1;
    void OnEnable()
    {
         lastValue = -1; 
    }

    void Update()
    {
    
            ApplyValue(ProcessValue(inputSelector.GetValue()));
    }

    protected override void OnValidate()
    {
        base.OnValidate();
    }
    public override void ApplyValue(float v)
    {
        if (v!=lastValue)
        {
            lastValue=v;
            driverEvent.Invoke(v);
        }

    }

}
