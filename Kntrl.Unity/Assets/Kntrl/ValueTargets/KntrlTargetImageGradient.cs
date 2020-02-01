using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KntrlTargetImageGradient : KntrlTargetterBase
{

    public Gradient gradient = zExt.HeatGradient();
    Image image;
    public override void ApplyValue(float f)
    {
        if (image == null) image = GetComponent<Image>();
        image.color = gradient.Evaluate(f);
    }

}
