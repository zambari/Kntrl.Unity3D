using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;

public class KntrlTargetRotation : KntrlTargetterBase
{

    public enum RotAxis { X, Y, Z }
    [Header("Rotator")]
    public RotAxis axis = RotAxis.Z;

    public Vector2 angleRange = new Vector2(-120, 120);
    public Transform targetTransform;

    void Reset()
    {
        targetTransform = transform;
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        if (targetTransform == null) targetTransform = transform;
    }

    public override void ApplyValue(float v)
    {
        if (targetTransform != null)
            switch (axis)
            {
                case RotAxis.X:
                    targetTransform.localRotation = Quaternion.Euler(-angleRange.Map(v), 0, 0);
                    break;
                case RotAxis.Y:
                    targetTransform.localRotation = Quaternion.Euler(0, -angleRange.Map(v), 0);
                    break;
                case RotAxis.Z:
                    targetTransform.localRotation = Quaternion.Euler(0, 0, -angleRange.Map(v));
                    break;
            }

    }

}
