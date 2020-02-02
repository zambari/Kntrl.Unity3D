using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class KntrlSourceNameDisplay : KntrlDiagUtilBase
{
    // IKntrlValueSource lastValueSource;
    Text text;
    float checkEveryNthSecond = 5;
    float nextCheck;
    string lastLabel;
    IKntrlValueSourcePointer sourcePointer;
    protected override void OnEnable()
    {
        base.OnEnable();
        TryToGetSource();
        lastLabel = null;
        PrintName();
    }
    protected void Reset()
    {
        name = "SourceNameDisplay";
    }
    void TryToGetSource()
    {
        if (inputSelector.referenceGameObject == null)
        {
            sourcePointer = GetComponentInParent<IKntrlValueSourcePointer>();
            if (sourcePointer != null) inputSelector.referenceGameObject = sourcePointer.gameObject;
        }
        else
        {
            sourcePointer = inputSelector.referenceGameObject.GetComponent<IKntrlValueSourcePointer>();
        }
    }
    protected override void OnValidate()
    {
        TryToGetSource();
        base.OnValidate();
        PrintName();
    }

    void PrintName()
    {
        string label = "source unknown";
        if (sourcePointer != null)
        {
            IKntrlValueSource source = sourcePointer.GetSource();

            if (source != null)
            {
                label = source.gameObject.name + " [" + source.GetType() + "]";
            }
            else
            {
                label = inputSelector.name;
            }

        }
        if (text == null) text = GetComponent<Text>();
        if (label != lastLabel) text.text = label;
        lastLabel = label;
    }
    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkEveryNthSecond;
            PrintName();
        }

    }
}
