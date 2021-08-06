// NOTE: This file has not been modified. USELESS now.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Unity.NJUCS.UI
{
	public class UIManager : MonoBehaviour
	{
		private static UIManager _instance;
		public static UIManager Instance { get { return _instance; } }

		public Transform UIParent;

		public string ResourcesDir = "UIPrefabs";

		//�������UI��ջ
		private Stack<UIBase> UIStack = new Stack<UIBase>();
		//����,Ԥ����
		private Dictionary<string, GameObject> UIObjectDic = new Dictionary<string, GameObject>();
		//�����ֵ�
		private Dictionary<string, UIBase> currentUIDic = new Dictionary<string, UIBase>();

		void Start()
		{
			_instance = this;
			LoadAllUIObject();
		}


		/// <summary>
		/// ��ջ
		/// </summary>
		public UIBase PushUIPanel(string UIname)
		{
			if (UIStack.Count > 0)
			{
				UIBase old_topUI = UIStack.Peek();
				old_topUI.DoOnPausing();
			}
			UIBase new_topUI = GetUIBase(UIname);
			new_topUI.DoOnEntering();
			UIStack.Push(new_topUI);
			foreach (string ui in currentUIDic.Keys)
			{
				if (ui == UIname)
				{
					return new_topUI;
				}
			}
			new_topUI.UILayer = currentUIDic.Count;
			currentUIDic.Add(UIname, new_topUI);
			new_topUI.transform.SetSiblingIndex(new_topUI.UILayer);
			return new_topUI;
		}

		public int StackCount()
		{
			return UIStack.Count;
		}

		/// <summary>
		/// �ö��ķ���
		/// </summary>
		/// <param name="UIname">U iname.</param>
		public void UpUIPanel(string UIname)
		{
			if (UIStack.Count < 2)
			{
				return;
			}
			UIBase oldUI = UIStack.Peek();
			oldUI.DoOnPausing();
			UIBase UITarget = GetUIBase(UIname);
			List<UIBase> list = new List<UIBase>(UIStack.ToArray());
			List<UIBase> li = new List<UIBase>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].UIName != UIname)
				{
					li.Add(list[i]);
				}
			}
			li.Add(UITarget);
			UIStack.Clear();
			int layer = 6;
			foreach (var item in li)
			{
				UIStack.Push(item);
				item.UILayer = layer;
				layer++;
			}
			UIStack.Peek().DoOnResuming();
		}

		public UIBase GetUIBase(string UIname)
		{
			foreach (string name in currentUIDic.Keys)
			{
				if (name == UIname)
				{
					return currentUIDic[name];
				}
			}
			//���ֵ��еõ�UI
			GameObject UIPrefab = UIObjectDic[UIname];
			GameObject UIObject = GameObject.Instantiate<GameObject>(UIPrefab);
			UIObject.transform.SetParent(UIParent, false);
			UIBase uibase = UIObject.GetComponent<UIBase>();
			return uibase;
		}

		/// <summary>
		/// ��ջ,��������
		/// </summary>
		public void PopUIPanel()
		{
			if (UIStack.Count == 0)
				return;

			UIBase old_topUI = UIStack.Pop();
			old_topUI.DoOnExiting();
			old_topUI.UILayer = -1;
			if (UIStack.Count > 0)
			{
				UIBase new_topUI = UIStack.Peek();
				new_topUI.DoOnResuming();
			}
		}

		/// <summary>
		/// ��̬��������UIԤ����
		/// </summary>
		private void LoadAllUIObject()
		{
			string path = Application.dataPath + "/Game/Resources/" + ResourcesDir;
			DirectoryInfo folder = new DirectoryInfo(path);
			foreach (FileInfo file in folder.GetFiles("*.prefab"))
			{
				int index = file.Name.LastIndexOf('.');
				string UIName = file.Name.Substring(0, index);
				string UIPath = ResourcesDir + "/" + UIName;
				GameObject UIObject = Resources.Load<GameObject>(UIPath);
				UIObjectDic.Add(UIName, UIObject);
			}
		}
	}
}