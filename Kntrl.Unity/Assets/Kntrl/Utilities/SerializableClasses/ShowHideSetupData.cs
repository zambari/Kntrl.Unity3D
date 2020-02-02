using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Z.ShowHide
{
    [System.Serializable]
    public class ShowHideSetup
    {
        [ReadOnly]
        public string name;
        public bool use = true;
        public GameObject gameObject;
        public bool hasShowHide;
        [Range(0, 1.5f)]
        public float delayShow;
        [Range(0, 1.5f)]
        public float delayHide;
        public Vector2 startAndLengthOfRange = new Vector2(0, .98f);
        [Range(0, 1)]
        [SerializeField] float applyPreview;
        IApply[] applyTargets = new IApply[0];
        public void Apply(float f)
        {

            if (gameObject != null)
            {

                f = (f - startAndLengthOfRange.x) * scalar;
                if (f < 0)
                    f = 0;
                else
                if (f > 1)
                    f = 1;
                applyPreview = f;
                foreach (var ia in applyTargets) ia.Apply(f);
            }
        }
        float scalar = 1;
        [SerializeField] [ReadOnly] int applyListeners = -1;
        [SerializeField] [HideInInspector] GameObject wasObject;
        public void OnValidate(MonoBehaviour source)
        {
            scalar = 1 / startAndLengthOfRange.y;
            if (startAndLengthOfRange.x < 0) startAndLengthOfRange.x = 0;
            if (startAndLengthOfRange.x > 0.99f) startAndLengthOfRange.x = 0.99f;
            if (startAndLengthOfRange.y < 0.01f) startAndLengthOfRange.y = 0.01f;
            if (startAndLengthOfRange.x + startAndLengthOfRange.y >= 1) startAndLengthOfRange.y = 1 - startAndLengthOfRange.x;
            if (startAndLengthOfRange.y > 1) startAndLengthOfRange.y = 1;
            IShowHide showHide = null;
            applyListeners = 0;
            if (gameObject != null)
            {
                wasObject = gameObject;

                applyTargets = gameObject.GetComponents<IApply>();
                applyListeners = applyTargets.Length;
                showHide = gameObject.GetComponent<IShowHide>();
                hasShowHide = (showHide != null);
                if (gameObject != wasObject)
                {
                    use = (hasShowHide);
                    wasObject = gameObject;
                }
            }
            if (gameObject == null)
            {
                name = "none";
                use = false;
            }

            else
            {
                name = gameObject.name + " [" + showHide.GetType() + "]";
            }
        }
        public ShowHideSetup() { }
        public ShowHideSetup(IShowHide source)
        {
            gameObject = source.gameObject;
        }

        public void Show()
        {
            gameObject.Show();
        }
        public void Hide()
        {
            gameObject.Hide();
        }
    }
}