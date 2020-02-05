using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlInputSwitch : MonoBehaviour, IKntrlValueSource<float>
{
    public KntrlSelector[] inputs;

    [Range(0, 1)]
    public float currentOutput;
    [ReadOnly] [SerializeField] string status;
    int _selectedInput;

    public int selectedInput
    {
        get { return _selectedInput; }
        set
        {
            if (value >= inputs.Length) value = inputs.Length - 1;
            if (value < 0) value = 0;
            if (inputs[value] != null)
            {
                _selectedInput = value;
                status = "input " + value;
            }
        }
    }
    TransitionVisualizer[] visualizers;
    public float GetValue()
    {
        currentOutput = inputs[selectedInput].GetValue();
        return currentOutput;
    }

    void HandleVisualizers()
    {
        visualizers = GetComponents<TransitionVisualizer>();
        while (visualizers.Length > inputs.Length)
        {
            DestroyImmediate(visualizers[visualizers.Length - 1]);
            visualizers = GetComponents<TransitionVisualizer>();
        }
        while (visualizers.Length < inputs.Length)
        {
            gameObject.AddComponent<TransitionVisualizer>();
            visualizers = GetComponents<TransitionVisualizer>();
        }
        Gradient gradient = zExt.HeatGradient();
        for (int i = 0; i < visualizers.Length; i++)
        {
            visualizers[i].color = gradient.Evaluate(i * 1f / visualizers.Length);
            visualizers[i].sourceTransform = inputs[i].referenceGameObject.transform;
        }
    }
    void OnValidate()
    {
        if (inputs == null || inputs.Length == 0)
        {
            inputs = new KntrlSelector[1];
            inputs[0] = new KntrlSelector();
        }
        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i] == null) inputs[i] = new KntrlSelector();
            inputs[i].OnValidate(this);
        }
        selectedInput = _selectedInput;
        HandleVisualizers();
    }
}
