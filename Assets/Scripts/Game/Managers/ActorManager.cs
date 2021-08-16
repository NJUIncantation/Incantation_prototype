using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Unity.NJUCS.Game
{
    public class ActorManager : MonoBehaviour
    {
        private Dictionary<string, GameObject> Actors = new Dictionary<string, GameObject>();

        public UnityAction<string, GameObject> OnActorCreated;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CreateActor(string name, GameObject gameObject)
        {
            if(Actors.ContainsKey(name))
            {
                return;
            }
            Debug.Log("An actor is created: " + name);
            Actors.Add(name, gameObject);
            OnActorCreated?.Invoke(name, gameObject);
        }

        public int AmountOfActors()
        {
            return Actors.Count;
        }

        public void DeleteActor(string name)
        {
            Debug.Log("An actor is deleted: " + name);
            Actors.Remove(name);
        }

        public GameObject FindActorByName(string name)
        {
            GameObject gameObject = null;
            Actors.TryGetValue(name, out gameObject);
            return gameObject;
        }

        public List<GameObject> FindActorThatHasComponent(string type)
        {
            List<GameObject> actorThatMatch = new List<GameObject>();
            foreach(GameObject go in Actors.Values)
            {
                var component = go.GetComponent(type);
                if (component != null)
                {
                    actorThatMatch.Add(go);
                }
            }
            return actorThatMatch;
        }
    }
}

