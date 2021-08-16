// Author:WangJunYao
// Compass
// Can get the direction of mainCamera and set the direction to CompassLine and CompassText
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class Compass : MonoBehaviour
    {

        private float LastzRotation;

        //GameObject mainCamera;
        [SerializeField] private CompassLine CompassLineLeft;
        [SerializeField] private CompassLine CompassLineRight;
        [SerializeField] private Text CompassText;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //For Test
            //changeCompass();
        }

        // Need to be called when direction changed
        public void changeCompass(float zRotation)
        {
            // Scroll the CompassLine
            if (LastzRotation != zRotation)
            {
                CompassLineLeft.Scroll(zRotation);
                CompassLineRight.Scroll(zRotation);
                LastzRotation = zRotation;
            }
            // Change CompassText
            CompassText.text = Math.Round(zRotation, 0).ToString();
            if (CompassText.text == "360") CompassText.text = "0";
            // Debug.Log(zRotation);
        }
    }
}