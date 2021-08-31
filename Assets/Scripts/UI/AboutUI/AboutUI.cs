using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class AboutUI : BasePanel
    {
        [SerializeField] Button BackButton;

        private Canvas AboutCanvas;

        // Start is called before the first frame update
        void Start()
        {
            AboutCanvas = GetComponent<Canvas>();

            BackButton.onClick.AddListener(OnClickBackButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnClickBackButton()
        {
            //Debug.Log("OnClickBackButton");
            UIManager.Instance.PopPanel();
            UIManager.Instance.PushPanel(UIPanelType.StartUI);
        }
    }
}