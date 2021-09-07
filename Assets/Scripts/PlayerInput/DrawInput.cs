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
            //设置该材质通道，0为默认值  
            material.SetPass(0);
            //设置绘制2D图像  
            GL.LoadOrtho();
            //表示开始绘制，绘制累心改为线段  
            GL.Begin(GL.LINES);
            int size = line_list.Count;
            for (int i = 0; i < size - 1; i++)
            {
                Vector3 start = line_list[i];
                Vector3 end = line_list[i + 1];
                //绘制线段  
                Create_Line(start.x, start.y, end.x, end.y);
            }
            GL.End();
            //Debug.LogError("画线"+Time.time);
        }

        void Create_Line(float x1, float y1, float x2, float y2)
        {
            //绘制线段，需要将屏幕中某个点的像素坐标除以屏幕宽或高  
            GL.Vertex(new Vector3(x1 / Screen.width, y1 / Screen.height, 0));
            GL.Vertex(new Vector3(x2 / Screen.width, y2 / Screen.height, 0));
        }

    }

}
