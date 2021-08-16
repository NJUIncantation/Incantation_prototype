// Author:WangJunYao
// UIManager, sinleton
// Some function can be called from outside

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.NJUCS.UI
{

    public class UIManager
    {
        readonly string panelPrefabPath = Application.dataPath + @"/Resources/UIPrefabs";

        //Singleton
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UIManager();
                return _instance;
            }
        }

        private Transform canvasTransform = null;
        public Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                    canvasTransform = GameObject.Find("Canvas").transform;
                return canvasTransform;
            }
        }

        private UIManager()
        {
            LoadUIPanelInfo();
        }

        //Store UIPanel(UIPanelType,UIPanelPath)
        private List<UIPanel> panelList = new List<UIPanel>();

        //Store <UIPanelType, BasePanel>
        private Dictionary<UIPanelType, BasePanel> panelDict = new Dictionary<UIPanelType, BasePanel>();

        //Store panel need to display
        private Stack<BasePanel> panelStack = new Stack<BasePanel>();

        //Push panel and display
        public void PushPanel(UIPanelType type)
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();

            //Judge if there have another panel in stack, if so, pause it
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            BasePanel panel = GetPanel(type);

            //Push panel and enter
            panelStack.Push(panel);

            panel.GetComponent<BasePanel>().OnEnter();
            Debug.Log(type + " is pushed");
        }

        //Pop panel out and exit 
        public void PopPanel()
        {
            if (panelStack == null)
                panelStack = new Stack<BasePanel>();

            if (panelStack.Count <= 0) return;

            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();

            if (panelStack.Count <= 0) return;

            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();
            Debug.Log("a panel is poped");
        }

        //Search UIPanel from panelList according to UIPanelType
        private UIPanel SearchPanelForType(UIPanelType type)
        {
            foreach (var item in panelList)
            {
                if (item.UIPanelType == type)
                    return item;
            }

            return null;
        }

        //Try to get BasePanle from panelDict according to UIPanelType
        private BasePanel TryGetValue(UIPanelType type)
        {
            BasePanel panel;
            panelDict.TryGetValue(type, out panel);

            return panel;
        }

        //Load all UIPrefab file
        private void LoadUIPanelInfo()
        {
            //Debug.Log(panelPrefabPath);
            DirectoryInfo folder = new DirectoryInfo(panelPrefabPath);

            panelList.Clear();

            foreach (FileInfo file in folder.GetFiles("*.prefab"))
            {

                UIPanelType type = (UIPanelType)Enum.Parse(typeof(UIPanelType), file.Name.Replace(".prefab", ""));
                string path = @"UIPrefabs/" + file.Name.Replace(".prefab", "");
                Debug.Log(path);
                bool UIPanelExistInList = false;

                UIPanel uIPanel = SearchPanelForType(type);

                if (uIPanel != null)
                {
                    uIPanel.UIPanelPath = path;
                    UIPanelExistInList = true;
                }

                if (UIPanelExistInList == false)
                {
                    UIPanel panel = new UIPanel
                    {
                        UIPanelType = type,
                        UIPanelPath = path
                    };
                    panelList.Add(panel);
                }
            }

            AssetDatabase.Refresh();//refresh resources
        }

        //Get needed BasePanel
        private BasePanel GetPanel(UIPanelType type)
        {
            if (panelDict == null)
                panelDict = new Dictionary<UIPanelType, BasePanel>();

            BasePanel panel = TryGetValue(type);

            //If not in the dic, try to find from the list
            if (panel == null)
            {
                string path = SearchPanelForType(type).UIPanelPath;
                if (path == null)
                    throw new Exception("Prefab of UIPanelType not found");

                if (Resources.Load(path) == null)
                    throw new Exception("Prefab of Path not found");

                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.transform.SetParent(CanvasTransform, false);

                panelDict.Add(type, instPanel.GetComponent<BasePanel>());

                //Debug.Log(panelDict);
                return instPanel.GetComponent<BasePanel>();
            }
            
            return panel.GetComponent<BasePanel>();
        }

    }
}