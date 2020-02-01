using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayGroupControl : MonoBehaviour
{
    [Range(0, 5f)]
    public float baseTime = 0.1f;
    public float baseDelayOffset = 0.3f;
    public bool reverse;

    void OnValidate()
    {
        if (baseDelayOffset<0) baseDelayOffset=0;
        
        SetBaseTime(baseTime);
    }
    void Start()
    {
        OnValidate();
    }
    public void SetBaseTime(float f)
    {
        var delays = GetComponentsInChildren<KntrlDelay>();
        for (int i = 0; i < delays.Length; i++)
        {
            if (reverse)
            {
                delays[i].delayTime = (delays.Length - i) * f + baseDelayOffset;
            }
            else
            {
                delays[i].delayTime = (i + 1) * f + baseDelayOffset;
            }

        }
    }

}
