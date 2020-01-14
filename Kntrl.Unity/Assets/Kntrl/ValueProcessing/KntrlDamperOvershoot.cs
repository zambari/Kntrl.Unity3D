using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlDamperOvershoot : KntrlValueProcessorBase
{

    public DamperOverShoot damper = new DamperOverShoot();
    [Range(0, 1.5f)]
    [SerializeField] float _smoothTime = 0.3f;
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
    void OnEnable()
    {
        isFirstFrame = true;

    }
    public override float ProcessValue(float input)
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;
            damper.InitializeValue(input);
        }
        damper.targetValueOvershoot = input;
        return damper.UpdatedOverShoot();
    }

}
