using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class KntrlInputToggle : MonoBehaviour, IKntrlValueSource
{
    Toggle toggle;
    public KntrlSelector inputA = new KntrlSelector();
    public KntrlSelector inputB = new KntrlSelector();
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener((x) => useInputB = x);
    }
    public bool useInputB
    {
        get { return _useInputB; }
        set
        {
            if (toggle == null) toggle = GetComponent<Toggle>();
            if (_useInputB != value)
            {
                _useInputB = value;

                toggle.isOn = value;
            }

        }
    }
    [SerializeField] bool _useInputB;

    void OnValidate()
    {
        useInputB = useInputB;
        var visualizers = GetComponents<TransitionVisualizer>();
        while (visualizers.Length < 2)
        {
            gameObject.AddComponent<TransitionVisualizer>();
            visualizers = GetComponents<TransitionVisualizer>();
        }
        visualizers[0].sourceTransform = inputA.referenceGameObjectTransform;
        visualizers[1].sourceTransform = inputB.referenceGameObjectTransform;
    }

    public float GetValue()
    {
        if (useInputB) return inputB.GetValue(); else return inputA.GetValue();
    }
}
