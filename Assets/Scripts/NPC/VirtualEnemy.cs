using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Game;

namespace Unity.NJUCS.NPC
{
    public class VirtualEnemy : MonoBehaviour
    {
        //private string enemyName;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //void OnEnable()
        //{
        //    EnemyManager.Instance.AddEnemy(this);
        //}

        //void OnDisable()
        //{
        //    if (EnemyManager.IsInitialized)
        //    {
        //        //FIXME:
        //        EnemyManager.Instance.RemoveEnemy(this);
        //    }
        //}
        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
