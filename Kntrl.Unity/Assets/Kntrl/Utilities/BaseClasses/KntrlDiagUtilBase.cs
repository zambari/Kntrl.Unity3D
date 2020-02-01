using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  [ExecuteInEditMode]

public abstract class KntrlDiagUtilBase : MonoBehaviour
{

    protected IKntrlValueSource valueSource;
    [ReadOnly] [SerializeField] string status = "none";
    protected float lastInput;
    protected virtual void OnEnable()
    {
        lastInput = -1;
        GetSource();
    }
    void GetSource()
    {
        valueSource = GetComponentInParent<IKntrlValueSource>();
        if (valueSource == null)
        {
            status = " no value source in parent ";
        }
        else
        {
            status = valueSource.GetType() + " on " + valueSource.name;
        }
    }
    
}

