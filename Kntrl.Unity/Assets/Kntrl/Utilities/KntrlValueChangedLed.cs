using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KntrlValueChangedLed : KntrlDiagUtilBase
{
    Image image;
    public Color color = new Color(0, 1, 0.4f);
    public Color offColor = new Color(0.2f, .2f, 0.2f, .5f);
    void Awake()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }
    void OnValidate()
    {
        if (image == null) image = GetComponent<Image>();
        image.color = color;
    }
    bool wasLit;
    void Update()
    {
            float thisInput = inputSelector.GetValue();
            if (thisInput != lastInput)
            {
                lastInput = thisInput;
                if (!wasLit)
                {
                    image.color = color;
                    wasLit = true;
                }
            }
            else
            if (wasLit)
            {
                image.color = offColor;
                wasLit = false;
            }
    }

}
