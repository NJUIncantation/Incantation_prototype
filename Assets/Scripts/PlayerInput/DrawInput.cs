using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Unity.NJUCS.PlayerInput
{
    public class DrawInput : MonoBehaviour
    {

        public Material material;
        private List<Vector3> line_list;
        void Start()
        {
            line_list = new List<Vector3>();
        }
        void ClearOnClick()
        {
            line_list.Clear();
        }
        void Update()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (true)//hit.collider.gameObject.name == "plane")
                {
                    line_list.Add(Input.mousePosition);
                }
            }
        }

        void OnPostRender()
        {
            //���øò���ͨ����0ΪĬ��ֵ  
            material.SetPass(0);
            //���û���2Dͼ��  
            GL.LoadOrtho();
            //��ʾ��ʼ���ƣ��������ĸ�Ϊ�߶�  
            GL.Begin(GL.LINES);
            int size = line_list.Count;
            for (int i = 0; i < size - 1; i++)
            {
                Vector3 start = line_list[i];
                Vector3 end = line_list[i + 1];
                //�����߶�  
                Create_Line(start.x, start.y, end.x, end.y);
            }
            GL.End();
            //Debug.LogError("����"+Time.time);
        }

        void Create_Line(float x1, float y1, float x2, float y2)
        {
            //�����߶Σ���Ҫ����Ļ��ĳ������������������Ļ����  
            GL.Vertex(new Vector3(x1 / Screen.width, y1 / Screen.height, 0));
            GL.Vertex(new Vector3(x2 / Screen.width, y2 / Screen.height, 0));
        }

    }

}
