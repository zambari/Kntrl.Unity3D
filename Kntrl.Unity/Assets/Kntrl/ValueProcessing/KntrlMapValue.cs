using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;


public class KntrlMapValue : KntrlValueProcessorBase
{
    [Header("Min and Max")]
    public Vector2 mapRange = new Vector2(0, 1);

    [Header("More settings")]
    public MoreMapSettings moreMapSettings = new MoreMapSettings();
    public float offset { get { return moreMapSettings.offset; } set { moreMapSettings.offset = value; } }

    public float minValue { get { return mapRange.x; } set { mapRange.x = value; } }
    public float maxValue { get { return mapRange.y; } set { mapRange.y = value; } }
    public override float ProcessValue(float input)
    {
        float mappedVal = mapRange.Map(input);
        mappedVal += moreMapSettings.offset;
        if (mappedVal < moreMapSettings.limits.x) mappedVal = moreMapSettings.limits.x;
        if (mappedVal > moreMapSettings.limits.y) mappedVal = moreMapSettings.limits.y;
        return mappedVal;
    }

}

[System.Serializable]
public class MoreMapSettings
{
    public float offset;
    public Vector2 limits = new Vector2(0, 1);

}