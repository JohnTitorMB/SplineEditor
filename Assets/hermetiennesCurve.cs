using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hermetiennesCurve : MonoBehaviour
{
    [SerializeField]
    float controleScale = 1.0f;

    [SerializeField]
    GameObject Cpoint;

    GameObject point1;
    GameObject point2;
    GameObject Point3;
    GameObject Point4;

     

    // Start is called before the first frame update
    void Start()
    {
        point1 = Instantiate(Cpoint, new Vector3(-1, 0,0),Quaternion.identity);
        point2 = Instantiate(Cpoint, new Vector3(1, 0, 0), Quaternion.identity);
        Point3 = Instantiate(Cpoint, new Vector3(-1, 1, 0), Quaternion.identity);
        Point4 = Instantiate(Cpoint, new Vector3(1, 1, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        for (float t = 0; t < 1; t += 0.001f)
        {             
            /*
            MyMatrix1x4 T = new MyMatrix1x4(Mathf.Pow(t, 3), Mathf.Pow(t, 2), t, 1);
            MyMatrix1x4 T2 = new MyMatrix1x4(Mathf.Pow(t + 0.001f, 3), Mathf.Pow(t + 0.001f, 2), t + 0.001f, 1);
            MyMatrix4x4 M = new MyMatrix4x4(2, -2, 1, 1,
                                    -3, 3, -2, -1,
                                    0, 0, 1, 0,
                                    1, 0, 0, 0);

            MyMatrix3x4 G = new MyMatrix3x4(point1.transform.position, point2.transform.position, point1.transform.position + (Point3.transform.position - point1.transform.position)*controleScale, point2.transform.position + (Point4.transform.position - point2.transform.position) * controleScale);

            Vector3 TMG = (T * M * G).ToVector3();
            Vector3 TMG2 = (T2 * M * G).ToVector3();*/
            
            Vector3 TMG = (2 * Mathf.Pow(t, 3) - 3 * Mathf.Pow(t, 2) + 1) * point1.transform.position
                         + (-2 * Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2)) * point2.transform.position
                         + (Mathf.Pow(t, 3) - 2 * Mathf.Pow(t, 2) + t) * Point3.transform.position
                          + (Mathf.Pow(t, 3) - Mathf.Pow(t, 2)) * Point4.transform.position;

            float tN = t + 0.001f;
            Vector3 TMG2 = (2 * Mathf.Pow(tN, 3) - 3 * Mathf.Pow(tN, 2) + 1) * point1.transform.position
             + (-2 * Mathf.Pow(tN, 3) + 3 * Mathf.Pow(tN, 2)) * point2.transform.position
             + (Mathf.Pow(tN, 3) - 2 * Mathf.Pow(tN, 2) + tN) * Point3.transform.position
              + (Mathf.Pow(tN, 3) - Mathf.Pow(tN, 2)) * Point4.transform.position;
            
            Debug.DrawLine(TMG, TMG2, new Color(0, 0, 0, 1), 0);
        }

        Debug.DrawLine(point1.transform.position, Point3.transform.position, new Color(1, 0, 0, 1));
        Debug.DrawLine(point2.transform.position, Point4.transform.position, new Color(1, 0, 0, 1));

    }
}
