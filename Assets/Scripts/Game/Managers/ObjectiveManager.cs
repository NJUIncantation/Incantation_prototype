using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unity.NJUCS.Game
{
    /// <summary>
    /// 管理一个scene或level的所有objective。objectiveManager应当只作用于一个level
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {
        private List<Objective> m_Objectives = new List<Objective>();
        private bool m_ObjectivesCompleted = false;
        public Objective TopObjective = null;

        void Awake()
        {
            Objective.OnObjectiveCreated += RegisterObjective;
        }

        void RegisterObjective(Objective objective)
        {
            Debug.Log("Objective created: " + objective.ObjectiveTitle);
            m_Objectives.Add(objective);
        }


        public void EnableObjective(string ObjectiveTitle)
        {
            foreach(Objective obj in m_Objectives)
            {
                if(obj.ObjectiveTitle == ObjectiveTitle)
                {
                    obj.IsEnable = true;
                    break;
                }
            }
        }

        public void DisableObjective(string ObjectiveTitle)
        {
            foreach (Objective obj in m_Objectives)
            {
                if (obj.ObjectiveTitle == ObjectiveTitle)
                {
                    obj.IsEnable = false;
                    break;
                }
            }
        }

        public void ClearAllObjective()
        {
            foreach (Objective obj in m_Objectives)
            {
                Destroy(obj);
            }
            m_Objectives.Clear();
        }

        void Update()
        {
            if (m_Objectives.Count == 0 || m_ObjectivesCompleted)
                return;

            for (int i = 0; i < m_Objectives.Count; i++)
            {
                if (m_Objectives[i].IsBlocking())
                {
                    return;
                }
            }

            m_ObjectivesCompleted = true;
            EventManager.Broadcast(Events.AllObjectivesCompletedEvent);
        }
        void OnDestroy()
        {
            Objective.OnObjectiveCreated -= RegisterObjective;
        }
    }



}
