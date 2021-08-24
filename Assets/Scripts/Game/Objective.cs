using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Unity.NJUCS.Game
{
    public abstract class Objective : MonoBehaviour
    {
        [Tooltip("Ŀ������")]
        public string ObjectiveTitle;

        [Tooltip("Ŀ������")]
        public string ObjectiveDescription;

        [Tooltip("Ŀ���Ƿ�Ϊ��ѡ")]
        public bool IsOptional = true;

        [Tooltip("Ŀ���ӳ�ʱ��")]
        public float DelayVisible;


        public bool IsBlocking() => !(IsOptional || IsCompleted || !IsEnable);

        public static event Action<Objective> OnObjectiveCreated;
        public static event Action<Objective> OnObjectiveCompleted;

        public bool IsCompleted { get; private set; }
        public bool IsEnable { get; set; }

        protected virtual void Start()
        {
            OnObjectiveCreated?.Invoke(this);

            IsCompleted = false;
            IsEnable = true;
            DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
            displayMessage.Message = ObjectiveTitle;
            displayMessage.DelayBeforeDisplay = 0.0f;
            EventManager.Broadcast(displayMessage);
        }

        private void OnDestroy()
        {
            
        }


        public void UpdateObjective(string descriptionText, string counterText, string notificationText)
        {
            ObjectiveUpdateEvent evt = Events.ObjectiveUpdateEvent;
            evt.Objective = this;
            evt.DescriptionText = descriptionText;
            evt.CounterText = counterText;
            evt.NotificationText = notificationText;
            evt.IsComplete = IsCompleted;
            EventManager.Broadcast(evt);
        }

        public void CompleteObjective(string descriptionText, string counterText, string notificationText)
        {
            //Debug.Log("Completing Object");
            IsCompleted = true;
            IsEnable = false;

            UpdateObjective(descriptionText, counterText, notificationText);

            OnObjectiveCompleted?.Invoke(this);
        }
    }

}
