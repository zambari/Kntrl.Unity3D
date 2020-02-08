using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class KntrlSourceSection
{
    public bool applyProcessed = true;

    [ReadOnly]
    public int processorCount;
    [ReadOnly]
    public int usedProcessorCount;

    [Range(0, 1)]
    [SerializeField]
    public float outputPreview;
    public bool onlyWhenChanged;
    public bool applyToSlider;
    public bool alwaysUpdate;

}
