// Author:WangJunYao
// Avatar, mainly used to load avater of character

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.NJUCS.UI
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] Image avatar;

        // Start is called before the first frame update
        void Start()
        {
            // For Test
            // LoadAvatar("Player");
        }

        // Update is called once per frame
        //void Update()
        //{
            
        //}

        public void LoadAvatar(string AvatarName)
        {
            string path = "UIResources/Avatar/";
            path += AvatarName;
            //avatar.sprite = Resources.Load<Sprite>(path);
        }
    }
}
