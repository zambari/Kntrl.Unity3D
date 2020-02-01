using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(RawImage))]
public class KntrlValueHistoryGraph : KntrlDiagUtilBase
{
    RawImage rawImage;
    public Vector2Int resolution = new Vector2Int(40, 8);
    public int samplesToEverage = 2;
    Color32[] colors;
    protected override void OnEnable()
    {
        base.OnEnable();
        OnValidate();
    }
    float incomingValue;
    int frameCounter;
    Texture2D texture;
    int[] vals;
    void OnValidate()
    {
        if (samplesToEverage < 1) samplesToEverage = 1;
        if (resolution.x < 10) resolution.x = 10;
        if (resolution.y < 1) resolution.y = 1;
        if (colors == null || colors.Length != resolution.x * resolution.y)
        {
            colors = new Color32[resolution.x * resolution.y];
            vals = new int[resolution.x];
            texture = new Texture2D(resolution.x, resolution.y);
            texture.wrapMode = TextureWrapMode.Clamp;
            if (rawImage == null) rawImage = GetComponent<RawImage>();
            rawImage.texture = texture;
        }
    }
    int writeIndex = 0;
    void AddToHistory(float val)
    {
        int heightValue = Mathf.FloorToInt((resolution.y-2) * val*.5f+1);
        // if (heightValue >= resolution.y-1) heightValue = resolution.y-1;
        vals[writeIndex] = heightValue;
        writeIndex++;
        if (writeIndex >= vals.Length) writeIndex = 0;
        DrawTexture();
    }
    public Color onColor = new Color(0, 1, 0.5f);
    public Color offColor = new Color(0, 0, 0.0f);
    void DrawTexture()
    {
        for (int i = 0; i < resolution.x; i++)
            for (int j = 0; j < resolution.y; j++)
            {
                int index = j * resolution.x + i - writeIndex;
                if (index < 0) index += colors.Length;
                if (index >= colors.Length) index -= colors.Length;
                colors[index] = vals[i] > j ? onColor : offColor;
            }

        texture.SetPixels32(colors);
        // public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors
        texture.Apply();
    }
    void Update()
    {
        float thisInput = valueSource.GetValue();

        incomingValue += thisInput;
        frameCounter++;
        if (frameCounter >= samplesToEverage)
        {
            frameCounter = 0;
            incomingValue /= samplesToEverage;
            AddToHistory(incomingValue);
        }

    }
}
