using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RawImage))]
public class KntrlValueHistoryGraph : KntrlDiagUtilBase, IKntrlValueSourcePointer, IKntrlValueSource
{
    RawImage rawImage;
    public enum SamplingMode { sampleAndHold, average }
    public SamplingMode samplingMode;
    public Vector2Int resolution = new Vector2Int(40, 8);
    [SerializeField] Vector2Int _lastResolution;
    public int samplesToEverage = 2;
    Color32[] colors;
    public bool drawLine = true;
    Color onColorStart;
    protected override void OnEnable()
    {
        base.OnEnable();
        OnValidate();
        onColorStart = onColor;
        PrepareTexture();
    }
    float incomingValue;
    int frameCounter;
    Texture2D texture;
    int[] vals;
    void PrepareTexture()
    {
        if (colors == null || colors.Length != resolution.x * resolution.y)
        {
            colors = new Color32[resolution.x * resolution.y];

            texture = new Texture2D(resolution.x, resolution.y);
            texture.wrapMode = TextureWrapMode.Clamp;
            if (rawImage == null) rawImage = GetComponent<RawImage>();
            rawImage.texture = texture;

        }
        // if (texture==null || texture.wrapMode!=resolution.x)
        // {

        // }
        if (vals == null || vals.Length != resolution.x)

            vals = new int[resolution.x];
    }
    protected override void OnValidate()
    {
        base.OnValidate();
        if (samplesToEverage < 1) samplesToEverage = 1;
        if (resolution.x < 10) resolution.x = 10;
        if (resolution.y < 1) resolution.y = 1;
        PrepareTexture();
        DrawTexture();
        // transitionVisualizer.sourceTransform = inputSelector.referenceGameObjectTransform;
        if (onColorStart != onColor)
        {
            onColorStart = onColor;
            transitionVisualizer.color = onColor * 0.7f;
        }

    }
    int writeIndex = 0;

    void AddToHistory(float val)
    {
        if (val > 1) val = 1;
        if (val < 0) val = 0;
        int heightValue = 2 + Mathf.FloorToInt((resolution.y - 4) * val);
        writeIndex++;
        if (writeIndex >= vals.Length) writeIndex = 0;
        vals[writeIndex] = heightValue;
        DrawTexture();
    }
    public Color onColor = new Color(0, 1, 0.3f);
    public Color offColor = new Color(0, 0, 0.0f);
    public void ClearHistory()
    {
        vals = new int[resolution.x];
        PrepareTexture();
        DrawTexture();
    }
    void DrawTexture()
    {

        if (drawLine)
        {
            int len = colors.Length;
            for (int i = 0; i < len; i++)
                colors[i] = offColor;

            int lastPoint = 0;//vals[(writeIndex + resolution.x - 1) % resolution.x];

            for (int i = 0; i < resolution.x - 1; i++)
            {
                int thisPoint = vals[i];
                // if (end < start)
                // {
                //     int temp = start;
                //     start = end;
                //     end = temp;
                // }
                // end++;
                int start = lastPoint;
                int end = thisPoint;
                if (thisPoint - lastPoint < 0)
                {
                    end = lastPoint;
                    start = thisPoint;
                }

                for (int y = start; y <= end; y++)
                {
                    int thisIndex = y * resolution.x + i - writeIndex;
                    if (thisIndex >= len) thisIndex -= len;
                    if (thisIndex < 0) thisIndex += len;
                    colors[thisIndex] = onColor;

                }

                lastPoint = thisPoint;

                // int startIndex =

                // if (endindex < 0) endindex = 0;
                // if (endindex >= len) endindex = len - 1;
                // // for (int j = start; j <= end; j++)
                // {
                //     int index = j * resolution.x + i;
                //     if ((index > 0 && index < colors.Length))
                //         colors[index] = onColor;
                // }
                //                Debug.Log("i=" + i + " start " + start + " end " + end);
            }

        }
        else
        {
            for (int i = 0; i < resolution.x; i++)
                for (int j = 0; j < resolution.y; j++)
                {
                    int index = (j + 1) * resolution.x + i - writeIndex;
                    if (index < 0) index += colors.Length;
                    if (index >= colors.Length) index -= colors.Length;

                    colors[index] = (vals[i] > j) ? onColor : offColor;
                }

        }
        texture.SetPixels32(colors);
        texture.Apply();
    }
    float thisInput;
    void Update()
    {
        frameCounter++;
        if (samplingMode == SamplingMode.average)
        {
            thisInput = inputSelector.GetValue();
            incomingValue += thisInput;
            if (frameCounter >= samplesToEverage)
            {
                frameCounter = 0;
                incomingValue /= samplesToEverage;
                AddToHistory(incomingValue);
                incomingValue = 0;
            }
        }
        else // sample and hold mode
        {
            if (frameCounter >= samplesToEverage)
            {
                thisInput = inputSelector.GetValue();
                frameCounter = 0;
                AddToHistory(thisInput);
            }
        }
    }

    public float GetValue()
    {
        return thisInput;
    }

    public IKntrlValueSource GetSource()
    {
        return inputSelector.valueSource;
    }
}
