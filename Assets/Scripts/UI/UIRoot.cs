// Author:WangJunYao
// UIRoot, Used to push UI and pop UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.NJUCS.UI
{
    public class UIRoot : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UIManager.Instance.PushPanel(UIPanelType.StartUI);
        }

    }
}