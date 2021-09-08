using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowheadSwing : MonoBehaviour
{
    public float Cycle = 2;
    public float Speed = 1;

    private float m_Curtime;
    private float m_Lasttime;
    private void Start()
    {
        m_Curtime = Time.time;
        m_Lasttime = Time.time;
    }

    private void Update()
    {
        m_Curtime = Time.time;
        float scale = Mathf.Sin(2 * Mathf.PI * (m_Curtime - m_Lasttime) / Cycle);
        gameObject.transform.position += scale * Speed * Time.deltaTime * Vector3.up;
    }
}
