using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ValueFromScrollbar : MonoBehaviour,IKntrlValueSource<float>
{
    Scrollbar scrollbar;
    void OnEnable()
    {

    }
    [Range(0, 1)]
    public float previewValue;
    public float GetValue()
    {
        if (scrollbar == null) scrollbar = GetComponent<Scrollbar>();
        previewValue = scrollbar.value;
        return previewValue;
    }

}
