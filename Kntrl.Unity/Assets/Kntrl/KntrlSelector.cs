using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//zmiana
[System.Serializable]
public class KntrlSelector : GenericSelector<IKntrlValueSource>
{
    //[HideInInspector] 
    public float currentValueOutput;
    [Range(0, 1)]
    [SerializeField] protected float _currentValueInput;
    [Range(0, 1)] [SerializeField] protected float _lastValueInput;
    bool _wasValueChangedInInspector;


    // [ReadOnly]
    // [SerializeField]
    // string message = "";
    public ModSection modSection = new ModSection();
    public float Process(float inVal)
    {
        return modSection.ProcessValue(_currentValueInput); ;
    }
    public bool overrideValue;


    public float GetValue()
    {
        if (!overrideValue)
        {
            if (valueSource == null) //&& slider == null
            {
                if (referenceGameObject == null) return _currentValueInput;
                // System.Type thistype=typeof(IKntrlValueSource<K>);
                valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
                if (valueSource == null) referenceGameObject = null;
            }
            if (valueSource != null)
            {
                if (_currentValueInput.Equals(_lastValueInput)) // ???
                {
                    currentValueInput = _currentValueInput;
                }
                else
                {
                    currentValueInput = valueSource.GetValue();
                }
            }
        }
        return currentValueOutput;
    }

    public override void OnValidate(MonoBehaviour source)
    {
        base.OnValidate(source);
        if (referenceGameObject == null && lastGameObject != null)
        {
            overrideValue = false;
        }
        if (_currentValueInput == _lastValueInput)
        {
            _lastValueInput = _currentValueInput;
            _wasValueChangedInInspector = true;
            // Debug.Log("move in inspector");
        }
        if (overrideValue || valueSource == null)
            currentValueInput = _currentValueInput;
        else
            currentValueInput = valueSource.GetValue();


        modSection.OnValidate();
    }

    public bool valueChangedInInspector
    {
        get
        {
            if (_wasValueChangedInInspector)
            {
                _wasValueChangedInInspector = false;
                return true;
            }
            return false;

        }
    }

    public float currentValueInput
    {
        get { return _currentValueInput; }
        set
        {
            _currentValueInput = value;
            _lastValueInput = value;
            currentValueOutput = Process(value);
        }
    }
    // public virtual float Process(float inVal)
    // {
    //     return inVal;
    // }

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
    //             valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
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
