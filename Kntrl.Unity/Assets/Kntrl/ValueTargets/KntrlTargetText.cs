using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;
[RequireComponent(typeof(Text))]
[ExecuteInEditMode]
public class KntrlTargetText : KntrlTargetterBase
{
    public Vector2 scaleRange = new Vector2(0.4f, 3f);
    public Transform targetTransform;
    public float baseScale = 1;
    Text text;
    void Awake(){
        text=GetComponent<Text>();
    }
    // public float baseScale=1;
    void Reset()
    {
        targetTransform = transform;
        baseScale = transform.localScale.x;
        scaleRange = new Vector2(baseScale / 4, baseScale * 2);
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        if (targetTransform == null) targetTransform = transform;
    }

    public override void ApplyValue(float v)
    {
        // if (targetTransform != null)
        // {
        //     v = scaleRange.Map(v);
        //     switch (axis)
        //     {
        //         case ScaleAxis.All:
        //             targetTransform.localScale = new Vector3(v, v, v);
        //             break;
        //         case ScaleAxis.X:
        //             targetTransform.localScale = new Vector3(v, baseScale, baseScale);
        //             break;
        //         case ScaleAxis.Y:
        //             targetTransform.localScale = new Vector3(baseScale, v, baseScale);
        //             break;
        //         case ScaleAxis.Z:
        //             targetTransform.localScale = new Vector3(baseScale, baseScale, v);
        //             break;

        //     }
        // }
    }

}
