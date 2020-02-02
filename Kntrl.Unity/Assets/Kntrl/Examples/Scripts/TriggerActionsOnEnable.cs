using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerActionsOnEnable : MonoBehaviour
{
    public UnityEvent whenEnabled;
    public UnityEvent whenDisabled;
    public float delay;
    public bool waitOneFrame = true;

    void OnEnable()
    {
        if (delay > 0 || waitOneFrame)
        {
            StartCoroutine(DelayedEnableRoutine());
        }
        else
            whenEnabled.Invoke();
    }
    IEnumerator DelayedEnableRoutine()
    {
        if (waitOneFrame) yield return null;
        yield return new WaitForSeconds(delay);
        whenEnabled.Invoke();
    }
    void OnDisable()
    {
        whenDisabled.Invoke();
    }

}
