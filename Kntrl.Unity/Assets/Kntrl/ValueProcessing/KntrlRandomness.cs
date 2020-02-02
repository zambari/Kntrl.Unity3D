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
    public float randomAmount { get { return dampedTargetter.randomAmount; } set { dampedTargetter.randomAmount = value; } }
    public float minTime { get { return dampedTargetter.timeRandomRange.x; } set { dampedTargetter.timeRandomRange.x = value; } }
    public float maxTime { get { return dampedTargetter.timeRandomRange.y; } set { dampedTargetter.timeRandomRange.y = value; } }
    public float smothTime { get { return dampedTargetter.damper.smoothTime; } set { dampedTargetter.damper.smoothTime = value; } }
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
