using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamperGroupControl : MonoBehaviour
{


    [Range(0, 1f)]
    public float baseSmoothTime = 0.1f;
    public float startSmoothness = 0.1f;

    void OnValidate()
    {
        SetBaseTime(baseSmoothTime);
    }
    void Start()
    {
        OnValidate();
    }
    public void SetBaseTime(float f)
    {
        var delays = GetComponentsInChildren<KntrlDamper>();
        for (int i = 0; i < delays.Length; i++)
        {
            delays[i].smoothTime = i * f + startSmoothness;
        }

        var delays2 = GetComponentsInChildren<KntrlDamperOvershoot>();
        for (int i = 0; i < delays2.Length; i++)
        {
            delays2[i].smoothTime = i * f + startSmoothness;
        }
    }

    public void SetOvershootRestore(float f)
    {

        var delays2 = GetComponentsInChildren<KntrlDamperOvershoot>();
        for (int i = 0; i < delays2.Length; i++)
        {
            delays2[i].damper.overshootRestoreSpeed =f;
        }
    }


}
