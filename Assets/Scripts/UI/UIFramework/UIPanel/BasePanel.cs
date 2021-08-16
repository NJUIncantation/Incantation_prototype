// Author:WangJunYao
// BaseUI, can control a loaded panel
// Enter, Pause, Resume, Exit

using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class BasePanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        public virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        
        // Enter panel
        // can only be called once
        public virtual void OnEnter()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        // Pause panel
        // can be seen, can't operate
        public virtual void OnPause()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = false;
        }

        // Resume panel
        // can be seen and can operate
        public virtual void OnResume()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        // Exit panel
        // close and exit panel, can't be seen and can't operate
        public virtual void OnExit()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }
}