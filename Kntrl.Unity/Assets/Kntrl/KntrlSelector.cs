using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//zmiana
[System.Serializable]
public class KntrlSelector : GenericSelector<float, IKntrlValueSource<float>>
{


    // [ReadOnly]
    // [SerializeField]
    // string message = "";
    public ModSection modSection = new ModSection();
    public override float Process(float inVal)
    {
        return modSection.ProcessValue(_currentValueInput); ;
    }
    public override void OnValidate(MonoBehaviour source)
    {
        base.OnValidate(source);
        modSection.OnValidate();
    }
    // [Range(0, 1)]
    // public 
    // [HideInInspector] public float currentValueOutput = 0.5f;

    // public Transform referenceGameObjectTransform { get { if (referenceGameObject == null) return null; return referenceGameObject.transform; } }
    // bool _wasValueChangedInInspector;
    // public bool valueChangedInInspector
    // {
    //     get
    //     {
    //         if (_wasValueChangedInInspector)
    //         {
    //             _wasValueChangedInInspector = false;
    //             return true;
    //         }
    //         return false;

    //     }
    // // }
    // public void OnValidate(MonoBehaviour source)
    // {
    //     if (_currentValueInput != _lastValueInput)
    //     {
    //         _lastValueInput = _currentValueInput;
    //         _wasValueChangedInInspector = true;
    //         // Debug.Log("move in inspector");
    //     }
    //     if (referenceGameObject != null)
    //     {
    //         if (valueSource == null || lastGameObject != referenceGameObject)
    //             valueSource = referenceGameObject.GetComponent<IKntrlValueSource<float>>();
    //         if (valueSource == null)
    //         {
    //             referenceGameObject = null;
    //             lastGameObject = null;
    //             overrideValue = true;
    //         }
    //         else
    //         {
    //             if (lastGameObject == null)
    //                 overrideValue = false;

    //         }
    //         lastGameObject = referenceGameObject;
    //     }

    //     if (valueSource == null)
    //     {
    //         message = "[No source]";
    //         referenceGameObject = null;
    //     }
    //     message = name;
    //     // else
    //     //     message = valueSource.name + " [" + valueSource.GetType() + "]";

    //     // if (overrideValue)
    //     // {
    //     //     if (referenceGameObject == null) message = " [Manual] / [No Source]";
    //     //     else
    //     //         message = "[Override]";
    //     // }

    //     lastGameObject = referenceGameObject;
    //     modSection.OnValidate();
    //     if (overrideValue || valueSource == null)
    //         currentValueInput = _currentValueInput;
    //     else
    //         currentValueInput = valueSource.GetValue();

    //GetValue();
}


// public void SetValue(float f)
// {
//     // if (!overrideValue)
//     {
//         currentValueInput = f;
//         // if (slider != null && slider.value != f)
//         // {
//         //     slider.value = f;
//         //     return;
//         // }
//     }
//     if (referenceGameObject == null) return;
// }


/// <summary>
/// Handles null case
/// </summary>
/// <value></value>
// public string name
// {
//     get
//     {
//         string n = "[no source]";
//         if (referenceGameObject != null && valueSource != null)
//             n = referenceGameObject.name + " [" + valueSource.GetType() + "]";
//         if (overrideValue) n += " [Override]";
//         return n;
//     }
