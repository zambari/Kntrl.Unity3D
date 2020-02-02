using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Z.PanelAnim
{

    [System.Serializable]
    public class PanelAnimatorData
    {
        public bool cokolwiek;

    }

    [System.Serializable]
    public class OpacitySetup
    {

        public CanvasGroup canvasGroup;
        public bool applyToCanvasGroup = true;

        public AnimationCurve animationOpacity = zExt.LinearCurve();
        public bool useCurveForOpacity = false;
        public bool disableRaycastsWhenNotFullOpacity = true;
        public void OnValidate(MonoBehaviour source)
        {
            if (canvasGroup == null) canvasGroup = source.GetComponent<CanvasGroup>();
            if (canvasGroup == null) applyToCanvasGroup = false;
        }
        public void Apply(float f)
        {
            if (applyToCanvasGroup)

            {
                if (canvasGroup == null) { applyToCanvasGroup = false; return; }
                float opacity = f;
                if (useCurveForOpacity)
                {
                    opacity = animationOpacity.Evaluate(opacity);

                }
                canvasGroup.alpha = opacity;
                if (disableRaycastsWhenNotFullOpacity)
                {
                    canvasGroup.blocksRaycasts = f == 1;
                    canvasGroup.interactable = f == 1;
                }
            }
        }
    }


    [System.Serializable]
    public class RectTransformInfo
    {
        public Vector2 pivot;
        public Vector2 anchorMin;
        public Vector2 anchorMax;
        public Vector3 anchoredPositon;
        public Vector2 sizeDelta;
        public Quaternion rotation;
        public RectTransformInfo(RectTransform source)
        {
            anchoredPositon = source.anchoredPosition3D;
            anchorMin = source.anchorMin;
            anchorMax = source.anchorMax;
            pivot = source.pivot;
            sizeDelta = source.sizeDelta;
            rotation = source.localRotation;
        }

    }
    [System.Serializable]
    public class RectTransformSetup
    {
        public bool useRectTransform = false;
        public RectTransformInfo startInfo;
        public RectTransformInfo endInfo;
        [SerializeField] RectTransform rectTransform;
        public AnimationCurve animationCurve = zExt.LinearCurve();
        public bool useAnimationCurve;
        public void OnValidate(MonoBehaviour source)
        {
            if (rectTransform == null) rectTransform = source.GetComponent<RectTransform>();
            if (rectTransform == null) Debug.Log("no rect tranform?");
            else
            {
                if (startInfo == null) startInfo = new RectTransformInfo(rectTransform);
                if (endInfo == null) endInfo = new RectTransformInfo(rectTransform);
            }

        }
        public void Apply(float f)
        {
            if (!useRectTransform) return;
            if (rectTransform == null)
            {
                Debug.Log("no rectt transfomr, please call onvalidate");
                return;
            }
            Vector2 pivot = Vector2.Lerp(startInfo.pivot, endInfo.pivot, f);
            Vector2 anchormin = Vector2.Lerp(startInfo.anchorMin, endInfo.anchorMin, f);
            Vector2 anchormax = Vector2.Lerp(startInfo.anchorMax, endInfo.anchorMax, f);
            Vector2 sizeDelta = Vector2.Lerp(startInfo.sizeDelta, endInfo.sizeDelta, f);
            Vector3 anchoredPos = Vector3.Lerp(startInfo.anchoredPositon, endInfo.anchoredPositon, f);
            rectTransform.pivot = pivot;
            rectTransform.anchorMin = anchormin;
            rectTransform.anchorMax = anchormax;
            rectTransform.anchoredPosition3D = anchoredPos;
            rectTransform.sizeDelta = sizeDelta;
            if (useAnimationCurve) f = animationCurve.Evaluate(f);
            rectTransform.localRotation = Quaternion.Lerp(startInfo.rotation, endInfo.rotation, f);
        }
        public void SaveHiddenState()
        {
            if (rectTransform == null) Debug.Log("please call onvalidate first");
            startInfo = new RectTransformInfo(rectTransform);

        }
        public void SaveShownState()
        {
            if (rectTransform == null) Debug.Log("please call onvalidate first");
            endInfo = new RectTransformInfo(rectTransform);
        }

    }
    [System.Serializable]
    public class EventSet
    {
        public UnityEvent onHideComplete = new UnityEvent();
        public UnityEvent onShowComplete = new UnityEvent();
        public UnityEvent onShowStart = new UnityEvent();
        public UnityEvent onHideStart = new UnityEvent();
        public Vector2 outputRange = new Vector2(0, 1);
        [Header("Called with float phase")]
        public FloatEvent sweepEvent = new FloatEvent();
    }

    [System.Serializable]
    public class LayoutSetup
    {
        public bool applyToLayoutElement = true;
        public LayoutElement layoutElement;
        public float savedPrefferedSize = 100;
        public float savedSmallSize = 0;

        public bool useHorizontal = true;
        public AnimationCurve animationLayout = zExt.LinearCurve();
        public bool useCurveForLayout = false;
        public bool moreSmoothStep = true;

        public void OnValidate(MonoBehaviour source)
        {
            if (savedPrefferedSize <= 0) savedPrefferedSize = 10;
            if (layoutElement == null) layoutElement = source.GetComponent<LayoutElement>();
            if (layoutElement == null) applyToLayoutElement = false;
        }
        public void Apply(float f)
        {
            if (applyToLayoutElement)
            {
                float layoutval = f;
                if (useCurveForLayout) layoutval = animationLayout.Evaluate(layoutval);
                if (moreSmoothStep) layoutval = Mathf.SmoothStep(0, 1, layoutval);
                if (layoutElement == null) return;
                if (moreSmoothStep) f = Mathf.SmoothStep(0, 1, f);
                f = Mathf.Lerp(savedSmallSize, savedPrefferedSize, f);
                if (useHorizontal) layoutElement.preferredWidth = f; else layoutElement.preferredHeight = f;
            }
        }
        public float GetSize()
        {
            if (layoutElement == null) return -1;
            if (useHorizontal) return layoutElement.preferredWidth; else return layoutElement.preferredHeight;
        }
    }
}
