using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class KntrlProximitySource : KntrlTargetterBase, IKntrlValueSource<float>
{
    [Header("Proximity source")]
    public Transform otherTransform;
    public bool useCurve;
    public float maxRange;

    public AnimationCurve curve = zExt.LinearCurveDown();
    [SerializeField] [ReadOnly] float currentDistance;
    protected override void OnValidate()
    {
        base.OnValidate();
        if (maxRange <= 0) maxRange = 1;
        //GetValue();

        // SetValue(_currentValue);

    }

    void OnDrawGizmos()
    {
        if (otherTransform != null)
        {

            Vector3 pos = transform.position;
            Vector3 target = otherTransform.position;
            Vector3 delta = target - pos;
            Vector3 midTarget = pos + delta * .35f;

            // Gizmos.color = Color.yellow * .8f;
            // Gizmos.color = Color.yellow * 0.6f;
            Gizmos.color = Color.blue * 0.7f;
            Gizmos.DrawLine(pos, midTarget);
            Gizmos.color = Color.blue * 0.3f;
            Gizmos.DrawLine(midTarget, target);
        }
    }
    float GetDistanceValue()
    {
        if (otherTransform == null) return .5f;
        currentDistance = Vector3.Distance(transform.position, otherTransform.position);
        float dist = currentDistance;
        dist /= maxRange;
        if (useCurve) dist = curve.Evaluate(dist);
        return dist;

    }
    void Update()
    {
        if (otherTransform != null)
        {
            SetValue(GetDistanceValue());
        }
        // _currentValue = ;

    }

}
