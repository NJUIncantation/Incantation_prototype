// Author:WangJunYao
// StateBar
// Can be used as HealthBar and ManaBar
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class StateBar : MonoBehaviour
    {

        private Image content;

        [SerializeField]
        private Text stateValue;

        private float currentFill;

        // Can only be set and get inside the class
        private float MyMaxValue { get; set; }

        private float currentValue;

        public float MycurrentValue
        {
            get
            {
                return currentValue;
            }

            //  Should be called when the state of character changed
            set
            {
                if (value > MyMaxValue)
                {
                    currentValue = MyMaxValue;
                }
                else if (value < 0)
                {
                    currentValue = 0;
                }
                else
                {
                    currentValue = value;
                }

                if (MyMaxValue == 0)
                {
                    currentFill = 0;
                }
                else
                {
                    currentFill = currentValue / MyMaxValue;
                }

                stateValue.text = Math.Round(MycurrentValue, 0).ToString() + "/" + MyMaxValue;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            content = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            content.fillAmount = currentFill;
        }

        // Initialize state of character, including health and mana
        // Should be called when creating characters
        public void Initialize(float currentValue, float maxValue)
        {
            MyMaxValue = maxValue;
            MycurrentValue = currentValue;
        }
    }
}