using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class KntrlValueBase : MonoBehaviour, IKntrlValueSource, IKntrlValueTarget
{
    void OnDrawGizmos()
    {
        if (_valueSource != null)
        {
            Gizmos.color = Color.white * .3f;
            Gizmos.DrawLine(transform.position, _valueSource.transform.position);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (_valueSource != null)
        {
            Vector3 pos = transform.position;
            Vector3 target = _valueSource.transform.position;
            Vector3 delta = target - pos;
            target = pos + delta * .35f;
            Gizmos.color = Color.yellow * .5f;
            Gizmos.DrawLine(transform.position, target);
        }
    }
    public bool externalSource
    {
        get { return _externalSource; }
        set
        {
            _externalSource = value;

        }
    }
    [Header("Drag other Kntrl")]
    public GameObject _valueSource;
    [SerializeField] [HideInInspector] GameObject _lastValueSource;
    [ReadOnly] [SerializeField] string sourceClassName = "none";
    [SerializeField] bool _externalSource = true;
    [SerializeField]
    [Range(0, 1)]
    protected float _currentValue = 0.5f;
    IKntrlValueSource valueProvider;

    IKntrlProcessValue[] processors = new IKntrlProcessValue[0];

    [SerializeField]
    protected KntrlSourceSection processing = new KntrlSourceSection();

    protected virtual void OnValidate()
    {
        GetProcessors();
        CheckValueProvider();

        SetValue(_currentValue);
    }
    void Start()
    {
        GetProcessors();
        CheckValueProvider();
        if (valueProvider == null) externalSource = false;
    }
    void GetProcessors()
    {
        processors = GetComponents<IKntrlProcessValue>();
        processing.processorCount = processors.Length;

    }
    public float GetValue()
    {
        return processing.outputPreview;
    }
    public void SetValue(float f)
    {

        if (processing.onlyWhenChanged && _currentValue == f) return;
        _currentValue = f;
        if (processing.applyProcessed)
        {
            processing.outputPreview = ProcessValue(f);
        }
        else
        {
            processing.outputPreview = f;
        }
        ApplyValue(processing.outputPreview);
    }
    // else 


    float ProcessValue(float f)
    {
        processing.usedProcessorCount = 0;
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
                    processing.usedProcessorCount++;
                }
            }
        }
        catch (System.Exception e)
        {
            GetProcessors();
            throw (e);
        }
        return f;
    }

    void CheckValueProvider()
    {
        valueProvider = null;
        // if (_valueSource == null)
        // {
        //     externalSource = false;

        // }
        if (_valueSource != null)
        {
            bool foundSelf = false;
            var providers = _valueSource.GetComponents<IKntrlValueSource>();
            if (providers.Length > 0)
            {

                for (int i = providers.Length - 1; i >= 0; i--)
                {
                    if (providers[i] != this as IKntrlValueSource)
                    {
                        //                  if (valueProvider == null)
                        //                        Debug.Log("found  value provider " + providers[i].GetType(), gameObject);
                        valueProvider = providers[i];
                        break;
                    }
                    else
                    {
                        foundSelf = true;
                    }
                }
            }
            else
            {
#if UNITY_EDITOR
                Slider slider = _valueSource.GetComponent<Slider>();
                if (slider != null)
                {
                    var val = slider.gameObject.AddComponent<KntrlValue>();
                    UnityEditor.Undo.RegisterCreatedObjectUndo(val, "ValueProvider");
                    valueProvider = val;
                }
#endif
            }
            if (valueProvider == null)
            {
                _valueSource = null;

                if (foundSelf)
                {
                    Debug.Log("You cannoy use the same object as processing and target, please select another object or add more processing components", gameObject);
                }
                else
                {
                    Debug.Log("this gameobejct does not implement IKntrlValueProviedr", gameObject);
                }
            }
        }
        if (_lastValueSource == null && _valueSource != null)
        {
            _lastValueSource = _valueSource;
            externalSource = true;
        }

        if (valueProvider == null)
        {
            sourceClassName = "None";
        }
        else
        {
            sourceClassName = valueProvider.GetType().ToString();
            if (!externalSource) sourceClassName += " [Set to External to enable]";
        }

    }

    /// <summary>
    /// Main method to override
    /// </summary>
    /// <param name="f"></param>

    public virtual void ApplyValue(float f)
    {

    }
    void Update()
    {

        if (externalSource && valueProvider != null && valueProvider.enabled)
        {
            // float outputPreview = ProcessValue(valueProvider.GetValue());
            // _currentValue = outputPreview;
            // SetValue(_currentValue);
            SetValue(valueProvider.GetValue());
        }
        else
        {
            if (processing.alwaysUpdate)
            {
                SetValue(_currentValue);
            }
        }
    }
}
