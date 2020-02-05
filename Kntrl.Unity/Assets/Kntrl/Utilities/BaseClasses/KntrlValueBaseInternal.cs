using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("")]
public abstract class KntrlValueBaseInternal : MonoBehaviour, IKntrlGetProcessors, IKntrlValueSource<float>, IKntrlValueSourcePointer
{

    [Header("Drag other Kntrl")]
    public KntrlSelector inputSelector = new KntrlSelector();
    protected TransitionVisualizer transitionVisualizer;
    protected IKntrlProcessValue[] processors;
    [ReadOnly] [SerializeField] int totalProcessors;
    [ReadOnly] [SerializeField] int activeProcessors;
    [Range(0, 1)]
    [SerializeField] protected float outputPreview;
    public void GetProcessors()
    {
        processors = GetComponents<IKntrlProcessValue>();
        totalProcessors = processors.Length;
        activeProcessors = 0;

    }
    protected virtual void OnEnable()
    {
        GetProcessors();
        OnValidate();
    }
    protected virtual void OnValidate()
    {

        inputSelector.OnValidate(this);

        UpateTransitionVisualiser();
    }

    protected void UpateTransitionVisualiser()
    {
        if (transitionVisualizer == null) transitionVisualizer = gameObject.GetComponent<TransitionVisualizer>();
        if (transitionVisualizer != null)
        {
            if (inputSelector.referenceGameObject == null)
                transitionVisualizer.sourceTransform = null;
            else
                transitionVisualizer.sourceTransform = inputSelector.referenceGameObjectTransform;
        }
    }
    protected float ProcessValue(float f)
    {

        activeProcessors = 0;
        try
        {
            for (int i = 0; i < processors.Length; i++)
            {
                if (processors[i] == null)
                {
                    GetProcessors();
                    return ProcessValue(f);
                }
                if (processors[i].enabled)
                {
                    f = processors[i].GetProcessedValue(f);
                    activeProcessors++;
                }
            }
        }
        catch (System.Exception e)
        {
            GetProcessors();
            //    throw (e);
        }
        return f;
    }
    protected virtual void Reset()
    {
        if (name == "GameObject") name = "KntrlValue " + zExt.RandomString(4);
        if (name.Contains("Slider")) name += " (KntrlValue)";

        transitionVisualizer = GetComponent<TransitionVisualizer>();
        if (transitionVisualizer == null) transitionVisualizer = gameObject.AddComponent<TransitionVisualizer>();


        // #if UNITY_EDITOR
        //         for (int i = 0; i < 10; i++)
        //             UnityEditorInternal.ComponentUtility.MoveComponentUp(this);
        // #endif
        OnValidate();

    }

    public float GetValue()
    {
        outputPreview = ProcessValue(inputSelector.currentValueOutput);
        return outputPreview;
    }

    public IKntrlValueSource<float> GetSource()
    {
      return inputSelector.valueSource;
    }

}
