using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Unity.NJUCS.Game
{
    public class GameFlowManager : MonoBehaviour
    {
        ObjectiveManager objectiveManager;
        private void Awake()
        {
            //Debug.Log("Game starts");
            ObjectiveManager objectiveManager = FindObjectOfType<ObjectiveManager>();
            //objectiveManager.RegisterObjective(FindObjectOfType<ObjectiveDock>());
            EventManager.AddListener<AllObjectivesCompletedEvent>(OnAllObjectivesCompleted);
        }

        void Start()
        {
            
        }

        void OnAllObjectivesCompleted(GameEvent AllObjectivesCompletedEvent)
        {
            Debug.Log("OnAllObjectivesCompleted");
            Application.Quit();
        }

        private void OnDestroy()
        {
            Destroy(objectiveManager);
        }
    }
}