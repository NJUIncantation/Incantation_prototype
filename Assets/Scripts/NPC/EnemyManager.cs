using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.NJUCS.Tools;

namespace Unity.NJUCS.NPC
{
    public class EnemyManager : Singleton<EnemyManager>
    {

        Dictionary<string, VirtualEnemy> enemiesDict = new Dictionary<string, VirtualEnemy>();
        List<VirtualEnemy> virtualEnemies = new List<VirtualEnemy>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Enemy Container Function
        public void AddEnemy(string name, VirtualEnemy enmey)
        {
            if(enemiesDict.ContainsKey(name))
            {
                Debug.Log("Enemy already existed!");
            }
            else
            {
                enemiesDict.Add(name, enmey);
            }
        }
        public void AddEnemy(VirtualEnemy enemy)
        {
            virtualEnemies.Add(enemy);
        }
        public void RemoveEnemy(VirtualEnemy enemy)
        {
            virtualEnemies.Remove(enemy);
        }

        public void RemoveEnemy(string name, VirtualEnemy enmey)
        {
            if (enemiesDict.ContainsKey(name))
            {
                enemiesDict.Remove(name);
            }
            else
            {
                Debug.Log("Enemy do not exist");
            }
        }
        #endregion

        public List<VirtualEnemy> GetEnemies()
        {
            List<VirtualEnemy> list;
            list = virtualEnemies;
            return list;
        }
    }
}
