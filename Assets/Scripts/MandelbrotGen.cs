using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System;
using UnityEngine;

public class MandelbrotGen : MonoBehaviour
{
    [SerializeField] GameObject initialPoint;
    [SerializeField] GameObject constantPoint;
    [SerializeField] int iterations = 5;
    [SerializeField] float _THRESHOLD = 10;

    LineRenderer line;
    

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = iterations + 1;

        GenerateSequence();
    }

    public void GenerateSequence()
    {
        line.SetPosition(0, initialPoint.transform.position);
        Vector3 point = initialPoint.transform.position;
        Vector3 constant = constantPoint.transform.position;

        for (int i = 1; i < iterations + 1; i++)
        {
            try{
                point = ComplexPow2(point) + constant;
                line.SetPosition(i, point);
            }
            catch(Exception)
            {
                for (int j = i; j < iterations + 1; j++)
                {
                    line.SetPosition(j, point);
                }
                break;
            }        
        }
    }

    private Vector3 ComplexPow2(Vector3 point)
    {
        Vector3 result;
        result.x = point.x * point.x - point.y * point.y;
        result.y = 2 * point.x * point.y;
        result.z = point.z;

        if(Mathf.Abs(point.x) > _THRESHOLD || Mathf.Abs(point.y) > _THRESHOLD)
        {
            throw new Exception("Threshold reached");
        }
            return result;
    }

}
