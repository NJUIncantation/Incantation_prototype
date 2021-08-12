using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }*/



public class MouseManager : Singleton<MouseManager>
{

    public Texture2D point, doorway, attack, target, arrow; 

    private Vector2 cursorPoint;

    RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;//鼠标点击Ground事件
    public event Action<GameObject> OnEnemyClicked;//鼠标点击Enemy事件

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        cursorPoint = new Vector2(16,16);
    }

    void Update()
    {
        //Debug.Log("get into function Update\n");
        SetCursorTexture();
        MouseControl();
        //Debug.Log("leave from function Update\n");
    }

    

    void SetCursorTexture()
    {
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hitInfo))
        {
            //change the cursor//
            switch(hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, cursorPoint, CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, cursorPoint, CursorMode.Auto);
                    break;
            }
        }
    }

    void MouseControl()
    {
        if(Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            //Debug.Log("MouseButtonDown\n");
            if(hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                //Debug.Log("Hit the Ground\n");
                OnMouseClicked?.Invoke(hitInfo.point);
            }
            if(hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("Hit the Enemy\n");
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
        }
    }
}
