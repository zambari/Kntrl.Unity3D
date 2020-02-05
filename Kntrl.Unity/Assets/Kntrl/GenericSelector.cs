using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//zmiana
[System.Serializable]

public class GenericSelector<K, T> where T : IKntrlValueSource<K>
{
    public bool overrideValue;
    [SerializeField] GameObject _referenceGameObject;
    public GameObject referenceGameObject
    {
        get { return _referenceGameObject; }
        set
        {
            if (_referenceGameObject == null && value != null)
            {
                overrideValue = false;
            }
            _referenceGameObject = value;
        }
    }
    GameObject lastGameObject;
    // public Slider slider;

    // public bool overrideValue = true;
    [ReadOnly]
    [SerializeField]
    string message = "";
    public T valueSource;

    [HideInInspector] public K currentValueOutput;
    [SerializeField] protected K _currentValueInput;
    [SerializeField] protected K _lastValueInput;
    public Transform referenceGameObjectTransform { get { if (referenceGameObject == null) return null; return referenceGameObject.transform; } }
    bool _wasValueChangedInInspector;

    public K GetValue()
    {
        if (!overrideValue)
        {
            if (valueSource == null) //&& slider == null
            {
                if (referenceGameObject == null) return _currentValueInput;
                // System.Type thistype=typeof(IKntrlValueSource<K>);
                valueSource = referenceGameObject.GetComponent<T>();
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
        currentValueInput = _currentValueInput;
        return currentValueOutput;
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


    public K currentValueInput
    {
        get { return _currentValueInput; }
        set
        {
            _currentValueInput = value;
            _lastValueInput = value;
            currentValueOutput = Process(value);
        }
    }
    public virtual K Process(K inVal)
    {
        return inVal;
    }

    public virtual void OnValidate(MonoBehaviour source)
    {
        if (_currentValueInput.Equals(_lastValueInput))
        {
            _lastValueInput = _currentValueInput;
            _wasValueChangedInInspector = true;
            // Debug.Log("move in inspector");
        }
        if (referenceGameObject != null)
        {
            if (valueSource == null || lastGameObject != referenceGameObject)
                valueSource = referenceGameObject.GetComponent<T>();
            if (valueSource == null)
            {
                referenceGameObject = null;
                lastGameObject = null;
                overrideValue = true;
            }
            else
            {
                if (lastGameObject == null)
                    overrideValue = false;

            }
            lastGameObject = referenceGameObject;
        }

        if (valueSource == null)
        {
            message = "[No source]";
            referenceGameObject = null;
        }
        else
            message = referenceGameObject.name + " [" + valueSource.GetType() + "]";

        if (overrideValue)
        {
            if (referenceGameObject == null) message = " [Manual] / [No Source]";
            else
                message = "[Override]";
        }

        lastGameObject = referenceGameObject;

        if (overrideValue || valueSource == null)
            currentValueInput = _currentValueInput;
        else
            currentValueInput = valueSource.GetValue();

        //GetValue();
    }




    // /// <summary>    
    // /// Handles null case
    // /// </summary>
    // /// <value></value>
    public string GetMessage()
    {
            string n = "[no source]";
            if (referenceGameObject != null && valueSource != null)
                n = referenceGameObject.name + " [" + valueSource.GetType() + "]";
            if (overrideValue) n += " [Override]";
            return n;
    }
}
