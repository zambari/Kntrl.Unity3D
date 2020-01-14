using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KntrlMapCurve : KntrlValueProcessorBase
{
    public AnimationCurve tgransferCurve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
    public override float ProcessValue(float input)
    {
        return tgransferCurve.Evaluate(input);
    }

}
