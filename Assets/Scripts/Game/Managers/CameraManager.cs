using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.NJUCS.Game
{
    public class CameraManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private Dictionary<string, GameObject> Cameras = new Dictionary<string, GameObject>();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CreateCamera(string name, GameObject gameObject)
        {
            if (Cameras.ContainsKey(name))
            {
                return;
            }
            Debug.Log("An actor is created: " + name);
            Cameras.Add(name, gameObject);
        }

        public int AmountOfCameras()
        {
            return Cameras.Count;
        }

        public void DeleteCamera(string name)
        {
            Debug.Log("An actor is deleted: " + name);
            Cameras.Remove(name);
        }

        public GameObject FindCameraByName(string name)
        {
            GameObject gameObject = null;
            Cameras.TryGetValue(name, out gameObject);
            return gameObject;
        }

        public List<GameObject> FindCameraThatHasComponent(string type)
        {
            List<GameObject> CameraThatMatch = new List<GameObject>();
            foreach (GameObject go in Cameras.Values)
            {
                var component = go.GetComponent(type);
                if (component != null)
                {
                    CameraThatMatch.Add(go);
                }
            }
            return CameraThatMatch;
        }
    }
}
