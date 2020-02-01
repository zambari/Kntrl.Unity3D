using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//zmiana
[System.Serializable]
public class KntrlSelector
{
    public GameObject referenceGameObject;
    GameObject lastGameObject;
    private IKntrlValueSource valueSource;
    private IKntrlValueTarget valueTarget;
    // public Slider slider;
    [Range(0, 1)]
    public float currentValueInput = 0.5f;
    public bool usePreview = true;
    [ReadOnly]
    [SerializeField]
    string message = "";
    public ModSection modSection = new ModSection();
    // [Range(0, 1)]
    // public 
    [HideInInspector] public float currentValueOutput = 0.5f;

    public Transform referenceGameObjectTransform { get { if (referenceGameObject == null) return null; return referenceGameObject.transform; } }

    public void OnValidate(MonoBehaviour source)
    {

        if (referenceGameObject != null)
        {
            if (lastGameObject != referenceGameObject)
            {

                valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
                if (valueSource != null)
                    usePreview = false;
            }

            if (valueSource == null)
                message = "No source";
            else
                message = "Src:" + valueSource.GetType() + " " + valueSource.name;

        }
        else
        {
            usePreview = true;
        }
        if (usePreview) message = "[Manual]";
        lastGameObject = referenceGameObject;
        GetValue();
    }
    public float ProcessValue(float f)
    {
        return modSection.ProcessValue(f);
    }
    public float GetValue()

    {
        if (!usePreview)
        {
            if (valueSource == null) //&& slider == null
            {
                valueSource = referenceGameObject.GetComponent<IKntrlValueSource>();
                if (valueSource == null) referenceGameObject = null;
            }
            if (valueSource != null)
            {
                currentValueInput = valueSource.GetValue();
            }
        }
        currentValueOutput = ProcessValue(currentValueInput);
        return currentValueOutput;
    }
    public void SetValue(float f)
    {
        currentValueInput = f;
        usePreview = true;
        // if (slider != null && slider.value != f)
        // {
        //     slider.value = f;
        //     return;
        // }

        if (referenceGameObject == null) return;
        if (valueTarget == null) valueTarget = referenceGameObject.GetComponent<IKntrlValueTarget>();
        if (valueTarget == null)
        {
            Debug.Log("invalid set");
            referenceGameObject = null;
            return;
        }
        valueTarget.SetValue(f);//  may cause loops
    }
}
[System.Obsolete("use KntrlSelector instead")]
[System.Serializable]
public class KntrlValueInputSelector : KntrlSelector
{

}
