using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class PauseUI : BasePanel
    {
        [SerializeField] Button ResumeButton;

        [SerializeField] Button OptionsButton;

        [SerializeField] Button ExitButton;

        private Canvas PauseCanvas;

        // Start is called before the first frame update
        void Start()
        {
            PauseCanvas = GetComponent<Canvas>();
            ResumeButton.onClick.AddListener(OnClickResumeButton);
            OptionsButton.onClick.AddListener(OnClickOptionsButton);
            ExitButton.onClick.AddListener(OnClickExitButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnClickResumeButton()
        {
            //Debug.Log("OnClickResumeButton");
            UIManager.Instance.PopPanel();
            //UIManager.Instance.PushPanel(UIPanelType.CombatUI);
        }
        private void OnClickOptionsButton()
        {
            //Debug.Log("OnClickOptionsButton");
            UIManager.Instance.PushPanel(UIPanelType.OptionsUI);
        }
        private void OnClickExitButton()
        {
            //Debug.Log("OnClickExitButton");
            UIManager.Instance.PopPanel();
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.StartUI);
        }
    }
}