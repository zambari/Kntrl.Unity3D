using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
[ExecuteInEditMode]
public class KntrlValueDisplay : KntrlDiagUtilBase
{
    Text text;
    protected override void OnEnable()
    {
        base.OnEnable();
        if (text == null) text = GetComponent<Text>();
        text.raycastTarget = false;
    }
    public float multiplier = 1;
    public string unit = "%";
    public bool addUnit;

    void PrintValue(float val)
    {

        if (text == null) return;
        val *= multiplier;
        string s = val.ToShortString();
        if (addUnit && !string.IsNullOrEmpty(unit))
        {
            s += " " + unit;
        }
        text.text = s;
    }
    void OnValidate()
    {
        if (text != null && inputSelector != null)
        {
            PrintValue(inputSelector.GetValue());
        }
    }
    void Update()
    {
        if (inputSelector != null)
        {
            float thisInput = inputSelector.GetValue();
            if (thisInput != lastInput)
            {
                lastInput = thisInput;
                PrintValue(thisInput);
            }
        }
    }
}
