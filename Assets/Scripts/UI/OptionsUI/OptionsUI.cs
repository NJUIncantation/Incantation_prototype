using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class OptionsUI : BasePanel
    {
        [SerializeField] Button BackButton;

        private Canvas OptionsCanvas;

        // Start is called before the first frame update
        void Start()
        {
            OptionsCanvas = GetComponent<Canvas>();

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
        }
    }
}