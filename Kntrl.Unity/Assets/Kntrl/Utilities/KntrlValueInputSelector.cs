using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class KntrlValueInputSelector
{

    public GameObject kntrlvalueSource;
    private IKntrlValueSource valueSource;

    public float GetValue()

    {
        if (kntrlvalueSource == null) return 0.5f;
        if (valueSource == null) valueSource = kntrlvalueSource.GetComponent<IKntrlValueSource>();
        if (valueSource == null)
        {
            kntrlvalueSource = null; return 0.3f;
        }
        return valueSource.GetValue();
    }


}
