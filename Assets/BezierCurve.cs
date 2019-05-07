using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Linq.Expressions;
public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    GameObject Cpoint;

    GameObject point1;
    GameObject point2;
    GameObject point3;
    GameObject point4;



    // Start is called before the first frame update
    void Start()
    {
        point1 = Instantiate(Cpoint, new Vector3(-1, 0, 0), Quaternion.identity);
        point2 = Instantiate(Cpoint, new Vector3(-1, 1, 0), Quaternion.identity);
        point3 = Instantiate(Cpoint, new Vector3(1, 1, 0), Quaternion.identity);
        point4 = Instantiate(Cpoint, new Vector3(1, 0, 0), Quaternion.identity);
 
    }

    // Update is called once per frame
    void Update()
    {
        for (float t = 0.0f; t < 1; t += 0.001f)
        {
            /*
            Vector3 t0 = Mathf.Pow(1 - t, 3) * point1.transform.position
                        +3 *t * Mathf.Pow(1-t,2)*point2.transform.position
                        + 3 * Mathf.Pow(t,2)*(1-t)*point3.transform.position 
                        + Mathf.Pow(t,3)*point4.transform.position;

            float tN = t + 0.001f;
            Vector3 t1 = Mathf.Pow(1 - tN, 3) * point1.transform.position
             + 3 * tN * Mathf.Pow(1 - tN, 2) * point2.transform.position
             + 3 * Mathf.Pow(tN, 2) * (1 - tN) * point3.transform.position
             + Mathf.Pow(tN, 3) * point4.transform.position;
             */


            MyMatrix1x4 T = new MyMatrix1x4(Mathf.Pow(t, 3), Mathf.Pow(t, 2), t, 1);
            MyMatrix1x4 T2 = new MyMatrix1x4(Mathf.Pow(t + 0.001f, 3), Mathf.Pow(t + 0.001f, 2), t + 0.001f, 1);

            MyMatrix4x4 M = new MyMatrix4x4(-1,  3, -3, 1,
                                             3, -6,  3, 0,
                                            -3,  3,  0, 0,
                                             1,  0,  0, 0);
            MyMatrix3x4 G = new MyMatrix3x4(point1.transform.position, point2.transform.position, point3.transform.position, point4.transform.position);

            Vector3 TMG = (T * M * G).ToVector3();
            Vector3 TMG2 = (T2 * M * G).ToVector3();
            Debug.DrawLine(TMG, TMG2, new Color(0, 0, 0, 1), 0);



        }
        Debug.DrawLine(point1.transform.position, point2.transform.position, new Color(1, 0, 0, 1));
        Debug.DrawLine(point3.transform.position, point4.transform.position, new Color(1, 0, 0, 1));

    }
}


