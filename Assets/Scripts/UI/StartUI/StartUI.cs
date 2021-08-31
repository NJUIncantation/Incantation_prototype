using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Unity.NJUCS.UI
{
    public class StartUI : BasePanel
    {
        [SerializeField] Button PlayButton;

        [SerializeField] Button OptionsButton;

        [SerializeField] Button AboutButton;

        [SerializeField] Button ExitButton;

        private Canvas StartCanvas;

        // Start is called before the first frame update
        void Start()
        {
            StartCanvas = GetComponent<Canvas>();
            PlayButton.onClick.AddListener(OnClickPlayButton);
            OptionsButton.onClick.AddListener(OnClickOptionsButton);
            AboutButton.onClick.AddListener(OnClickAboutButton);
            ExitButton.onClick.AddListener(OnClickExitButton);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnClickPlayButton()
        {
            //Debug.Log("OnClickPlayButton");
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.CombatUI);
        }
        private void OnClickOptionsButton()
        {
            //Debug.Log("OnClickOptionsButton");
            UIManager.Instance.PushPanel(UIPanelType.OptionsUI);
        }
        private void OnClickAboutButton()
        {
            //Debug.Log("OnClickAboutButton");
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.AboutUI);
        }
        private void OnClickExitButton()
        {
            Debug.Log("OnClickExitButton");
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }
    }
}