using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DamperOverShoot : Damper
{
    [Header("Overshoot")]
    [Range(0, 10)]
    public float deltaInputGain = 2.7f;
    public bool useOverShoot = false;
    // [Range(0, 2)]

    public float overshootRestoreSpeed = 2f;
    public float deltaOutputGain = 2;
    float lastTarget;
    float realTarget;
    float deltaAccumulator;
    // public float maxVelocity = .3f;
    public float targetValueOvershoot
    {
        get { return targetValue; }
        set
        {
            realTarget = value;
            if (useOverShoot)
            {
                float delta = (value - lastTarget);
                deltaAccumulator += deltaInputGain * deltaInputGain * delta * Time.deltaTime;
                // if (delta >
                //                 overshootLimit
                // targetValue = value;
                // BlendRealTarget();
            }
            // else
            targetValue = value;
            lastTarget = value;

        }
    }
    void BlendRealTarget()
    {
        // _targetValue += (realTarget + _targetValue) * overshootRestoreSpeed * Time.deltaTime;
        // _targetValue = Mathf.Lerp(_targetValue, realTarget, overshootRestoreSpeed * overshootRestoreSpeed * Time.deltaTime * (multismooth ? smoothTime : 1));
        //if (velocity > maxVelocity) velocity = maxVelocity;
        //if (velocity < -maxVelocity) velocity = -maxVelocity;
    }
    public float UpdatedOverShoot()
    {
        if (useOverShoot)
        {
            // BlendRealTarget();
            deltaAccumulator *= (1 - overshootRestoreSpeed * Time.deltaTime);
            return UpdatedValue() + deltaAccumulator * deltaOutputGain;
        }
        return UpdatedValue();

    }
    //public float




}
