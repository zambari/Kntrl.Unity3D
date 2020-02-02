using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
[RequireComponent(typeof(TransitionVisualizer))]
public abstract class KntrlDiagUtilBase : MonoBehaviour
{

    public KntrlSelector inputSelector = new KntrlSelector();
    protected float lastInput;
   protected TransitionVisualizer transitionVisualizer;
    protected virtual void OnEnable()
    {
        lastInput = -1;
        GetSource();
    }
    protected virtual void OnValidate()
    {
        GetSource();
        inputSelector.OnValidate(this);
        if (transitionVisualizer == null)
            transitionVisualizer = GetComponent<TransitionVisualizer>();
        if (transitionVisualizer == null) transitionVisualizer = gameObject.AddComponent<TransitionVisualizer>();
        transitionVisualizer.sourceTransform = inputSelector.referenceGameObjectTransform;
    }
    void GetSource()
    {
        if (inputSelector.referenceGameObject == null)
        {
            var vs = GetComponentInParent<IKntrlValueSource>();
            if (vs != null)
            {
                inputSelector.referenceGameObject = vs.gameObject;
                inputSelector.OnValidate(this);
            }
        }
    }

}

