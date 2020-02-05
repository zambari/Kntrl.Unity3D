using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Z;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Z.PanelAnim;
// v.0.1

[ExecuteInEditMode]
public class PanelLayoutAnimator : MonoBehaviour, IShowHide, IApply, IKntrlValueSource
{

    public bool startHidden = true;

    [HideInInspector]
    [SerializeField] int direction = 1;
    bool _animate;
    public OpacitySetup opacitySetup = new OpacitySetup();
    public LayoutSetup layoutSetup = new LayoutSetup();

    public RectTransformSetup rectTransformSetup = new RectTransformSetup();

    public float layoutPreferresSize
    {
        get { if (layoutSetup.layoutElement != null) return layoutSetup.GetSize(); return 0; }
        set
        {
            layoutSetup.savedPrefferedSize = value;

        }
    }
    public float layoutClosedSize
    {
        get { return layoutSetup.savedSmallSize; ; }
        set
        {
            layoutSetup.savedSmallSize = value;
        }
    }
    public bool animate
    {
        get { return _animate; }
        set
        {
            // Debug.Log("setinganim " + value);
            if (!_animate && value)
            {
                if (direction < 0 && events.onShowStart != null) events.onShowStart.Invoke();
                if (direction > 0 && events.onHideStart != null) events.onHideStart.Invoke();
            }
            else
            {
                if (_animate && !value)
                {
                    if (direction < 0 && events.onShowComplete != null && phase == 0) events.onShowComplete.Invoke();
                    if (direction > 0 && events.onHideComplete != null && phase == 1) events.onHideComplete.Invoke();
                }
            }
            _animate = value;
        }
    }
    [Header("Events")]
    public EventSet events = new EventSet();
    public bool deactivateOnHide;
    [Header("Delays")]
    public float delayStart;
    public float delayHide;

    public bool useSmoothStep = true;
    [Range(0, 2)]
    public float animationSpeed = 1;

    [Header("Preview")]
    [Range(0, 1)]
    [SerializeField]
    float phase = 1;
    public bool previewTransitions;

    [ExposeMethodInEditor]
    public void SaveHiddenState()
    {
        phase = 0;
        rectTransformSetup.OnValidate(this);
        rectTransformSetup.SaveHiddenState();

    }
    [ExposeMethodInEditor]
    public void SaveShownState()
    {
        phase = 1;
        rectTransformSetup.OnValidate(this);
        rectTransformSetup.SaveShownState();
        layoutSetup.savedSmallSize = layoutSetup.GetSize();
    }

    void Awake()
    {
        events.onShowComplete.AddListener(() => Debug.Log(name + " showcomplete " + Time.frameCount + " phase " + phase, gameObject));
        events.onHideComplete.AddListener(() => Debug.Log(name + " onHideComplete " + Time.frameCount + " phase " + phase, gameObject));
        events.onHideStart.AddListener(() => Debug.Log(name + " onHideStart " + Time.frameCount + " phase " + phase, gameObject));
        events.onShowStart.AddListener(() => Debug.Log(name + " onShowStart " + Time.frameCount + " phase " + phase, gameObject));
        if (startHidden && Application.isPlaying)
        {
            phase = 0;
            // previewTransitions = true;
            bool currrentdev = deactivateOnHide;
            deactivateOnHide = false;
            Apply(0);
            deactivateOnHide = currrentdev;
            // if (deactivateOnHide) gameObject.SetActive(false); 
            // else
            // Hide();
        }
        // else
        // {
        //     phase = 1;
        //     Apply(1);
        //     Show();
        // }

    }


    void Reset()
    {
        OnValidate();
        SaveCurrentSize();
    }
    bool delayrunning;

    void OnEnable()
    {
        OnValidate();
        previewTransitions=false;
    }
    void OnValidate()
    {
        layoutSetup.OnValidate(this);
        opacitySetup.OnValidate(this);
        rectTransformSetup.OnValidate(this);

        if (previewTransitions)
        {
            Apply(phase);
        }
    }

    [ExposeMethodInEditor]
    void SaveCurrentSize()
    {
        layoutSetup.savedPrefferedSize = layoutSetup.GetSize();
    }

    [ExposeMethodInEditor]
    public void Hide()
    {
        // if (!Application.isPlaying)
        // {
        //     phase = 0;
        //     Apply(0);
        // }
        // else
        previewTransitions = true;
        {
            direction = -1;
            if (delayHide > 0)
            {
                StartCoroutine(DelayedStart(delayHide));
            }
            else
                animate = true;
        }
    }

#if UNITY_EDITOR
    [ExposeMethodInEditor]
    void ShowNow()
    {
        UnityEditor.EditorApplication.delayCall += Show;
    }
#endif
    public void Show()
    {
        previewTransitions = true;
        direction = 1;
        gameObject.SetActive(true);
        if (phase == 0)
        {
            Apply(0);
        }
        // if (Application.isPlaying)
        // {
        if (delayStart > 0)
        {
            StartCoroutine(DelayedStart(delayStart));
        }
        else
            animate = true;
        // else
        // {
        //     phase = 1;
        //     direction = -1;
        //     Apply(1);
        // }
    }
    IEnumerator DelayedStart(float time)
    {
        if (delayrunning) yield break;
        delayrunning = true;
        yield return new WaitForSeconds(time);
        animate = true;
        delayrunning = false;

    }
    float lastPhase;
    public void Apply(float f)
    {
        // phase =phase = f;

        if (f > 0) gameObject.SetActive(true); else if (deactivateOnHide && direction < 0 && lastPhase > 0) gameObject.SetActive(false);
        lastPhase = f;

        previewTransitions = true;
        f = useSmoothStep ? Mathf.SmoothStep(0, 1, f) : f;
        if (f > 1) f = 1; else if (f < 0) f = 0;
        rectTransformSetup.Apply(f);
        layoutSetup.Apply(f);
        opacitySetup.Apply(f);
        events.sweepEvent.Invoke(events.outputRange.Map(f));
    }
    void Update()
    {
        if (animate)
        {
            float delta = Time.deltaTime * animationSpeed * animationSpeed * direction;
            phase += delta;
            //                Debug.Log(name + " phase is " + phase + " delt " + delta);
            if (phase > 1)
            {
                phase = 1;
            }
            else
             if (phase < 0)
            {
                phase = 0;
            }

            // Apply(appliedPhase);
            if (phase == 1)
            {
                direction = -1;
                animate = false;
            }
            if (phase == 0)
            {
                Debug.Log("phase " + phase);

                animate = false;
            }
            Apply(phase);
        }
        if (phase == 0 && direction == -1)
        {
            events.onHideComplete.Invoke();
            direction = 1;
            if (deactivateOnHide)
                gameObject.SetActive(false);
        }
        else
        if (phase == 1 && direction == 1)
        { direction = -1; }
    }

    public float GetValue()
    {
        return phase;
    }


}

