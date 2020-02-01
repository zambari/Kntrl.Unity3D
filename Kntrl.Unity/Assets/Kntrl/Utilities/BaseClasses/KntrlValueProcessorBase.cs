using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public abstract class KntrlValueProcessorBase : MonoBehaviour, IKntrlProcessValue
{

    public InOutMonitor inOutMonitor = new InOutMonitor();
    protected virtual void Start() { } // enabling enabled checkbox
    public float GetProcessedValue(float input)
    {
        inOutMonitor.inputValue = input;
        float outVal = ProcessValue(input);
        inOutMonitor.outputValue = outVal;
        return outVal;
    }
    public virtual float ProcessValue(float input)
    {
        return input;
    }
    protected virtual void OnEnable()
    {
        Nofity();
    }

    void Nofity()
    {
        IKntrlGetProcessors getProcessors = GetComponent<IKntrlGetProcessors>();
        if (getProcessors != null) getProcessors.GetProcessors();
    }
    protected virtual void OnDisable()
    {
        Nofity();
    }
}
