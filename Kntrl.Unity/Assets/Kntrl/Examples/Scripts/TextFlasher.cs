using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TextFlasher : MonoBehaviour
{
    public Color colorHigh = Color.white;

    public Color colorLow = new Color(0.5f, 0.5f, 0.5f);
    public float timeOn = .2f;
    public float timeOff = .6f;
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Flasher());
    }
    IEnumerator Flasher()
    {
        Text text = GetComponent<Text>();
        yield return new WaitForSeconds(Random.value * (timeOff + timeOn));
        while (true)
        {
            text.color = colorHigh;
            yield return new WaitForSeconds(timeOn);
            text.color = colorLow;
            yield return new WaitForSeconds(timeOff);
        }
    }
}
