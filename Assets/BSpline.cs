using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSpline : MonoBehaviour
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
        
        for (float t = 0; t < 1; t += 0.001f)
        {
            Vector3 _t1 = Mathf.Pow(1 - t, 3) * point1.transform.position;
            Vector3 _t2 = (3 * Mathf.Pow(t, 3) - 6 * Mathf.Pow(t, 2) + 4) * point2.transform.position;
            Vector3 _t3 = (-3 * Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2) + 3 * t + 1) * point3.transform.position;
            Vector3 _t4 = Mathf.Pow(t, 3) * point4.transform.position;
            Vector3 _t5 = (_t1 + _t2 + _t3 + _t4)*(1.0f/6.0f);
            Vector3 t0 = 1.0f/6.0f * (
                        Mathf.Pow(1 - t, 3) * point1.transform.position
                        +(3*Mathf.Pow(t,3)-6*Mathf.Pow(t,2)+4)*point2.transform.position
                        +(-3*Mathf.Pow(t,3) + 3*Mathf.Pow(t,2) + 3*t+1)*point3.transform.position
                        +Mathf.Pow(t,3)*point4.transform.position
                        );

            float tN = t + 0.001f;
            Vector3 t1 = (1.0f / 6.0f) * (Mathf.Pow(1 - tN, 3) * point1.transform.position
             + (3 * Mathf.Pow(tN, 3) - 6 * Mathf.Pow(tN, 2) + 4) * point2.transform.position
             + (-3 * Mathf.Pow(tN, 3) + 3 * Mathf.Pow(tN, 2) + 3 * tN + 1) * point3.transform.position
             + Mathf.Pow(tN, 3) * point4.transform.position);

            
            Debug.DrawLine(t0, t1, new Color(0, 0, 0, 1), 0);
        }

        Debug.DrawLine(point1.transform.position, point2.transform.position, new Color(1, 0, 0, 1));
        Debug.DrawLine(point3.transform.position, point4.transform.position, new Color(1, 0, 0, 1));

    }
}
