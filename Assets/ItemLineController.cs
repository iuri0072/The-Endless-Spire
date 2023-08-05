using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    public bool DrawLine;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
        DrawLine = true;
    }
    public void DestroyLine()
    {
        DrawLine = false;
        
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, Vector3.zero);
        }
    }
    private void Update()
    {
        if (DrawLine)
        {
            for (int i = 0; i < points.Length; i++)
            {
                lr.SetPosition(i, points[i].position);
            }
        }
        //else
        //{
        //    for (int i = 0; i < points.Length; i++)
        //    {
        //        lr.SetPosition(i, Vector3.zero);
        //    }
        //}
    }
}
