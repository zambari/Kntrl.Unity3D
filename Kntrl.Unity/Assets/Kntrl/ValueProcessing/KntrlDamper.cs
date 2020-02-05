using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlDamper : KntrlValueProcessorBase
{

    public Damper damper = new Damper();
    [Range(0, 1f)]
    [SerializeField] float _smoothTime=0.6f;
    public float smoothTime
    {
        get { return _smoothTime; }
        set { damper.smoothTime = value * value * value; }
    }

    protected void OnValidate()
    {
        smoothTime = _smoothTime;
    }
    bool isFirstFrame;
protected override    void OnEnable()
    {
        base.OnEnable();
        isFirstFrame = true;

    }
    public override float ProcessValue(float input)
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;
            damper.InitializeValue(input);
        }
        damper.targetValue = input;
        return damper.UpdatedValue();
    }

}
