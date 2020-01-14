using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;

public class KntrlDelay : KntrlValueProcessorBase
{

    public DelayValue delayedValue = new DelayValue(.5f);
    protected void OnValidate()
    {
        delayedValue.OnValidate();
    }
    public float delayTime {get {return delayedValue.delay; } set { delayedValue.delay=value;}}
    public override float ProcessValue(float input)
    {
        delayedValue.EnqueueValue(input);
        return delayedValue.OutputValue();
    }
    

}
