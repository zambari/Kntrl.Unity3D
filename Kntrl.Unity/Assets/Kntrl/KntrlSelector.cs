using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//zmiana
[System.Serializable]
public class KntrlSelector
{
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
    public IKntrlValueSource valueSource;
    public IKntrlValueTarget valueTarget;
    // public Slider slider;
    [Range(0, 1)]
    [SerializeField] float _currentValueInput = 0.5f;
    float _lastValueInput;
    public float currentValueInput
    {
        get { return _currentValueInput; }
        set
        {
            _currentValueInput = value;
            _lastValueInput = value;
            currentValueOutput = modSection.ProcessValue(_currentValueInput);
        }
    }
    public KntrlSelector()
    {
    }

    public KntrlSelector(Vector2 outputMap, Vector2 clamp)
    {
        modSection = new ModSection();
        if (clamp == Vector2.zero)
            clamp = outputMap;
        modSection.outputMap = outputMap;
        modSection.clampMinMax = clamp;
    }

    public KntrlSelector(Vector2 outputMap, bool square = false)
    {
        modSection = new ModSection();
        modSection.outputMap = outputMap;
        modSection.square = square;
    }
    public bool overrideValue = true;
    [ReadOnly]
    [SerializeField]
    string message = "";
    public ModSection modSection = new ModSection();
    public float currentValueOutput = 0.5f;
    public Transform referenceGameObjectTransform { get { if (referenceGameObject == null) return null; return referenceGameObject.transform; } }
    bool _wasValueChangedInInspector;
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
    float lastValidateTime;
    public void OnValidate(MonoBehaviour source)
    {
        lastValidateTime = Time.time;
        if (_currentValueInput != _lastValueInput)
        {
            _lastValueInput = _currentValueInput;
            _wasValueChangedInInspector = true;
            // Debug.Log("move in inspector");
        }
        if (referenceGameObject != null)
        {
            if (valueSource == null || lastGameObject != referenceGameObject)
                valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
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
        message = name;
        // else
        //     message = valueSource.name + " [" + valueSource.GetType() + "]";

        // if (overrideValue)
        // {
        //     if (referenceGameObject == null) message = " [Manual] / [No Source]";
        //     else
        //         message = "[Override]";
        // }

        lastGameObject = referenceGameObject;
        modSection.OnValidate();
        if (overrideValue || valueSource == null)
            currentValueInput = _currentValueInput;
        else
            currentValueInput = valueSource.GetValue();

        //GetValue();
    }
    // public bool ov2;

    bool ignoreLackOfOnValidate = false;
    int warningCount = 0;
    public float GetValue()
    {
        if (_currentValueInput != _lastValueInput && !ignoreLackOfOnValidate)
        {
            if (Time.time - lastValidateTime > 2)
            {
                Debug.LogError("please remember to call KntrlSelecto's OnValidate when callback is recieved by your mono");
                warningCount++;
                if (warningCount > 3) ignoreLackOfOnValidate = true;
            }
        }
        if (!overrideValue)
        {
            if (valueSource == null) //&& slider == null
            {
                if (referenceGameObject == null) return _currentValueInput;

                valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
                if (valueSource == null) referenceGameObject = null;
            }
            if (valueSource != null)
            {
                if (_currentValueInput != _lastValueInput)
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
    public void SetValue(float f)
    {
        // if (!overrideValue)
        {
            currentValueInput = f;
            // if (slider != null && slider.value != f)
            // {
            //     slider.value = f;
            //     return;
            // }
        }
        if (referenceGameObject == null) return;
        if (valueTarget == null) valueTarget = referenceGameObject.GetComponent<IKntrlValueTarget>();
        if (valueTarget == null)
        {
            // Debug.Log("invalid set");
            // referenceGameObject = null;
        }
        else
        {
            valueTarget.SetValue(f);//  may cause loops
        }
    }


    /// <summary>
    /// Handles null case
    /// </summary>
    /// <value></value>
    public string name
    {
        get
        {
            string n = "[no source]";
            if (referenceGameObject != null && valueSource != null)
                n = referenceGameObject.name + " [" + valueSource.GetType() + "]";
            if (overrideValue) n += " [Override]";
            return n;
        }
    }
}

