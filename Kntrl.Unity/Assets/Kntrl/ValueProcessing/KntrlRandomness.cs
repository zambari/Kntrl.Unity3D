using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Z;

public class KntrlRandomness : KntrlValueProcessorBase
{

    public DampedTargetter dampedTargetter = new DampedTargetter();
    // bool isFirstFrame;
    // void OnEnable()
    // {
    //     isFirstFrame = true;

    // }
    public override float ProcessValue(float input)
    {
        // {
        //     isFirstFrame = false;
        //     dampedTargetter.damper.InitializeValue(input);
        // }
        // dampedTargetter.targetValue = input;
        return input + dampedTargetter.UpdatedValue() - 1;
    }


}
