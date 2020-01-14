using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;


public class KntrlExternalChain : KntrlValueProcessorBase
{
    public bool includeChildren;
    public GameObject sourceObject;
    [SerializeField] [ReadOnly] int processorCount;
    [SerializeField] [ReadOnly] int activeProcessors;
    IKntrlProcessValue[] processors = new IKntrlProcessValue[0];
    void OnEnable()
    {
        GetProcessors();
    }
    void OnValidate()
    {
        GetProcessors();
    }

    void GetProcessors()
    {

        if (sourceObject == null)
        {
            processors = new IKntrlProcessValue[0];
        }
        else
        {
            processors = includeChildren ? sourceObject.GetComponentsInChildren<IKntrlProcessValue>() : sourceObject.GetComponents<IKntrlProcessValue>();


        }
        processorCount = processors.Length;
    }
    public override float ProcessValue(float input)
    {
        if (!enabled) return input;
        activeProcessors = 0;
        try
        {
            for (int i = 0; i < processors.Length; i++)
            {
                if (processors[i].enabled)
                {
                    input = processors[i].GetProcessedValue(input);
                    activeProcessors++;
                }
            }
        }
        catch (System.Exception e)
        {
            GetProcessors();
            throw (e);
        }
        return input;
    }

}