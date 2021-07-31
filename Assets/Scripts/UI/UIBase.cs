using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity.NJUCS.UI
{
	//ǿ���������
	[RequireComponent(typeof(CanvasGroup))]
	public class UIBase : MonoBehaviour
	{

		public string UIName = "";
		public int UILayer = 0;
		protected CanvasGroup canvasGroup;
		protected virtual void Awake() { canvasGroup = GetComponent<CanvasGroup>(); }

		/// <summary>
		/// ����
		/// </summary>
		public virtual void DoOnEntering() { }

		/// <summary>
		/// ����
		/// </summary>
		public virtual void DoOnPausing() { }

		/// <summary>
		/// ����
		/// </summary>
		public virtual void DoOnResuming() { }

		/// <summary>
		/// �˳�
		/// </summary>
		public virtual void DoOnExiting() { }

	}
}