using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KntrlGenerator : MonoBehaviour, IKntrlValueSource<float>
{
    public enum Shape { Ramp, Sin, Saw, RampInv }

    [Range(0, 2)]
    public float _speed = 1f;
    public float speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            speedCubed = _speed * _speed * _speed;
        }
    }
    [SerializeField] Shape _shape;
    public Shape shape { get { return _shape; } set { _shape = value; } }
    public void SetShapeFromInt(int i)
    {
        shape = (Shape)i;
    }
    float phase;

    [Range(0, 1)]
    [SerializeField]
    float outputPreview;
    float twoPi;
    float speedCubed;
    void OnValidate()
    {
        speed = speed;
    }
    void Awake()
    {
        twoPi = Mathf.PI * 2;
        OnValidate();
    }
    float GetValue(float phase)
    {
        switch (shape)
        {
            default:
                return phase;
            case Shape.RampInv:
                return 1 - phase;
            case Shape.Sin:
                return Mathf.Sin(phase * twoPi) * 0.5f + 0.5f;
            case Shape.Saw:
                float v = phase * 2;
                if (v <= 1) return v;
                return 2 - v;
        }
    }
    public float GetValue()
    {
        return outputPreview;
    }
    void Update()
    {
        phase += Time.deltaTime * speedCubed;
        while (phase >= 1) phase -= 1;
        outputPreview = GetValue(phase);
    }

}
