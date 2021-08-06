// Author:WangJunYao
// UIBase
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity.NJUCS.UI
{
	public class UIBase : MonoBehaviour
	{

		public string UIName = "";
		public int UILayer = 0;
		protected Canvas canvas;
		protected virtual void Start() { canvas = GetComponent<Canvas>(); }

		/// <summary>
		/// 进入
		/// </summary>
		public virtual void DoOnEntering() 
		{ 
			canvas.enabled = true;
		}

		/// <summary>
		/// 锁定
		/// </summary>
		public virtual void DoOnPausing() 
		{
			canvas.enabled = false;
		}

		/// <summary>
		/// 解锁
		/// </summary>
		public virtual void DoOnResuming() { }

		/// <summary>
		/// 退出
		/// </summary>
		public virtual void DoOnExiting() { }

	}
}