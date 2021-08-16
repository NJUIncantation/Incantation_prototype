// Author:WangJunYao
// CompassLine
// Used to scroll the CompassLineImage repeatedly
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class CompassLine : MonoBehaviour
    {
        // Store the Line image
        private Image Line;
        private Vector3 startPosition;

        // Use this for initialization
        void Start()
        {
            Line = GetComponent<Image>();
            startPosition = Line.GetComponent<RectTransform>().localPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Need to be called when direction changed
        public void Scroll(float zRotation)
        {
            Line.GetComponent<RectTransform>().localPosition = startPosition + new Vector3(-zRotation * 2, 0, 0);
            if (Line.GetComponent<RectTransform>().localPosition.x < -510.0f)
            {
                Line.GetComponent<RectTransform>().localPosition += new Vector3(1440.0f, 0, 0);
            }
            else if (Line.GetComponent<RectTransform>().localPosition.x >= 930.0f)
            {
                Line.GetComponent<RectTransform>().localPosition -= new Vector3(1440.0f, 0, 0);
            }
            Line.GetComponent<Image>().color = new Color(255, 255, 255, 0.4f);
        }
    }
}