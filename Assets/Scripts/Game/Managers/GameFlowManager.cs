using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace Unity.NJUCS.Game
{
    public class GameFlowManager : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Game starts");
            EventManager.AddListener<GameOverEvent>(OnEnemyKilled);
        }

        void OnEnemyKilled(GameOverEvent evt)
        {
            Application.Quit();
        }
    }
}