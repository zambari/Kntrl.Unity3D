using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ShowHide
{
    public class ShowHideGroup : MonoBehaviour, IShowHide, IKntrlValueSource
    {
        public float waitBeforeTrigger = 0;
        public enum TriggerMode { ishowhide, iapply }
        public TriggerMode triggerMode = TriggerMode.iapply;
        public List<ShowHideSetup> objectsToPropagate = new List<ShowHideSetup>();
        public bool startHidden;

        [ExposeMethodInEditor]
        void GetChildrenHiders()
        {
            var showhides = GetComponentsInChildren<IShowHide>();
            objectsToPropagate.Clear();
            for (int i = 0; i < showhides.Length; i++)
            {
                var thisShowHide = showhides[i];
                if (thisShowHide != this as IShowHide)
                {
                    objectsToPropagate.Add(new ShowHideSetup(thisShowHide));
                }
            }
        }

        public bool attachToToggle = true;
        public bool invertToggle;
        [Range(0, 2)]
        public float speed=1;
        [Header("transiton preview")]
        [Range(0, 1)]
        [SerializeField]
        float phase;
        public bool applyPreview;
        void Start()
        {
            Toggle toggle = GetComponent<Toggle>();
            if (toggle == null) attachToToggle = false;
            else
                toggle.onValueChanged.AddListener(ShowOrHide);
            if (startHidden)
            {
                phase = 0;
                Apply(0);
            }
        }
        public void ShowOrHide(bool b)
        {
            // Debug.Log("slider says go " + b);
            if (invertToggle) b = !b;
            if (b) Show();
            else Hide();
        }
        [ExposeMethodInEditor]
        void RemoveEmpty()
        {
            for (int i = objectsToPropagate.Count - 1; i >= 0; i--)
            {
                if (objectsToPropagate[i].gameObject == null) objectsToPropagate.RemoveAt(i);
            }
        }
        void OnValidate()
        {
            for (int i = 0; i < objectsToPropagate.Count; i++)
            {
                objectsToPropagate[i].OnValidate(this);
            }
            if (applyPreview)
            {
                for (int i = 0; i < objectsToPropagate.Count; i++)
                    objectsToPropagate[i].Apply(phase);

            }
        }
        IEnumerator DelayedShow(GameObject targetObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (targetObject != null)
            {
                targetObject.Show();
                Debug.Log("Showing object " + targetObject.name + " after " + delay + " seconds");
            }
        }

        IEnumerator DelayedHide(GameObject targetObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (targetObject != null)
            {
                targetObject.Hide();
                Debug.Log("hiding object " + targetObject.name + " after " + delay + " seconds");
            }
        }
        public int direction = 1;
        public bool animate = false;

        public void Apply(float f)
        {
            for (int i = 0; i < objectsToPropagate.Count; i++)
            {
                objectsToPropagate[i].Apply(f);
            }
            phase=f;
        }

        [ExposeMethodInEditor]
        public void Hide()
        {

            if (triggerMode == TriggerMode.ishowhide)
            {
                applyPreview = false;
                for (int i = 0; i < objectsToPropagate.Count; i++)
                {
                    if (objectsToPropagate[i] != null && objectsToPropagate[i].gameObject != null && objectsToPropagate[i].use)
                    {
                        float thisDelay = objectsToPropagate[i].delayHide;
                        if (thisDelay <= 0)
                            objectsToPropagate[i].Hide();
                        else
                            StartCoroutine(DelayedHide(objectsToPropagate[i].gameObject, objectsToPropagate[i].delayHide));
                    }
                }
            }
            else
            {
                direction = -1;
                animate = true;
            }
        }
        [ExposeMethodInEditor]
        public void Show()
        {

            if (waitBeforeTrigger > 0 && Application.isPlaying)
            {
                StartCoroutine(TriggerWait());
            }
            else
            {
                TriggerShow();
            }
        }
        public void TriggerShow()
        {
            if (triggerMode == TriggerMode.ishowhide)
            {

                applyPreview = false;
                for (int i = 0; i < objectsToPropagate.Count; i++)
                {
                    if (objectsToPropagate[i] != null && objectsToPropagate[i].gameObject != null)
                    {
                        float thisDelay = objectsToPropagate[i].delayHide;
                        if (thisDelay <= 0)
                            objectsToPropagate[i].Hide();
                        else
                            StartCoroutine(DelayedShow(objectsToPropagate[i].gameObject, objectsToPropagate[i].delayHide));
                    }
                }
            }
            else
            {
                direction = 1;
                animate = true;
            }

        }
        IEnumerator TriggerWait()
        {
            yield return new WaitForSeconds(waitBeforeTrigger);
            TriggerShow();
        }
        void Update()
        {
            if (animate)
            {
                phase += Time.deltaTime * speed * speed * direction;
                if (phase > 1) { phase = 1; animate = false; }
                if (phase < 0) { phase = 0; animate = false; }
                Apply(phase);
            }
        }

        public float GetValue()
        {
            return phase;
        }
    }

}