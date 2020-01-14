using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ColorSettings
{

    public bool lerpColor;
    public Color colorA;
    public Color colorB;

}
public class LerpAnimation : MonoBehaviour//, IFade
{
    //[Header("Warning")]1
    public ColorSettings colorSettings;
    // public Vector2 movementRange = new Vector2(0, 10);
    [Range(0, 1)]
    public float phase = 0;

    public AnimationCurve lightIntensityCurve = new AnimationCurve(new Keyframe(-0.01f, 0f, 0f, 0f), new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.1798613f, 1f, 0f, 0f), new Keyframe(0.799109f, 0.9606056f, -0.2813826f, -0.2813826f), new Keyframe(1f, 0f, 0f, 0f));
    public AnimationCurve positionCurve = zExt.LinearCurve();
    public bool fadeLight;
    public Vector3 start;
    public Vector3 end;
    // public bool randomizeStartPhase = true;
    [Range(0, 12)]
    public float lightIntensity = -1;
    new Light light;
    Vector3 right = Vector3.right;

    [Range(0, 2)]
    public float baseSpeed = 0.3f;

    [ExposeMethodInEditor]
    void SaveStart()
    {
        start = transform.position;
    }
    [ExposeMethodInEditor]
    void SaveEnd()
    {
        end = transform.position;

    }
    public bool preview = true;
    public Vector2 delayTimeRange = new Vector2(1, 3);

    float fadeAmt = 1;
    public void Fade(float f)
    {
        fadeAmt = f;
    
    }
    public DampedTargetter speedTargetter = new DampedTargetter();

    void Reset()
    {
        SaveStart();
        SaveEnd();

    }

    // }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(start + Vector3.up / 2, start - Vector3.up / 2);
        Gizmos.DrawLine(end + Vector3.up / 2, end - Vector3.up / 2);

    }

}
