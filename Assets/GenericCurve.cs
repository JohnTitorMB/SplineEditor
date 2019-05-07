using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurveType
{
    HermitiennesCurve,
    BezierCurve,
    BSplineCurve,
    CatmullRom
}

public class GenericCurve : MonoBehaviour
{
    [SerializeField]
    float controleScale=1.0f;

    [SerializeField]
    CurveType curveType = CurveType.HermitiennesCurve;

    [SerializeField]
    GameObject Cpoint;

    [SerializeField]
    List<GameObject> points;

    GameObject point1;
    GameObject point2;
    GameObject point3;
    GameObject point4;

    void Start()
    {
        points = new List<GameObject>();
        points.Add(Instantiate(Cpoint, new Vector3(-1, 0, 0), Quaternion.identity));
        points.Add(Instantiate(Cpoint, new Vector3(-1, 1, 0), Quaternion.identity));
        points.Add(Instantiate(Cpoint, new Vector3(1, 1, 0), Quaternion.identity));
        points.Add(Instantiate(Cpoint, new Vector3(1, 0, 0), Quaternion.identity));
    }

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            int index = points.Count-1;
            points.Add(Instantiate(Cpoint, points[index].transform.position + new Vector3(0, 1, 0), Quaternion.identity));
            points.Add(Instantiate(Cpoint, points[index].transform.position + new Vector3(2, 1, 0), Quaternion.identity));
            points.Add(Instantiate(Cpoint, points[index].transform.position + new Vector3(2, 0, 0), Quaternion.identity));
        }
       
        if (curveType == CurveType.HermitiennesCurve)
            DisplayCurve(points[0].transform.position, points[3].transform.position, points[1].transform.position, points[2].transform.position);
        else
            DisplayCurve(points[0].transform.position, points[1].transform.position, points[2].transform.position, points[3].transform.position);

        Debug.DrawLine(points[0].transform.position, points[1].transform.position, new Color(1, 0, 0, 1));
        Debug.DrawLine(points[2].transform.position, points[3].transform.position, new Color(1, 0, 0, 1));

        for (int i=4; i < points.Count;i = i+3)
        {
            if (curveType == CurveType.HermitiennesCurve)
                DisplayCurve(points[i - 1].transform.position, points[i + 2].transform.position, points[i].transform.position, points[i + 1].transform.position);
            else
                DisplayCurve(points[i - 1].transform.position, points[i].transform.position, points[i + 1].transform.position, points[i + 2].transform.position);

            Debug.DrawLine(points[i-1].transform.position, points[i].transform.position, new Color(1, 0, 0, 1));
            Debug.DrawLine(points[i+1].transform.position, points[i+2].transform.position, new Color(1, 0, 0, 1));
        }


    }

    MyMatrix4x4 GetCurveTypeMatrix()
    {
        switch(curveType)
        {
            case CurveType.HermitiennesCurve:
                return new MyMatrix4x4(2, -2, 1, 1,
                                       -3, 3, -2, -1,
                                       0, 0, 1, 0,   
                                       1, 0, 0, 0);       

            case CurveType.BezierCurve:
                return new MyMatrix4x4(-1, 3, -3, 1,
                                       3, -6, 3, 0,
                                       -3, 3, 0, 0,
                                       1, 0, 0, 0);

            case CurveType.BSplineCurve:
                return 1.0f/6.0f * new MyMatrix4x4(-1, 3, -3, 1,
                                        3, -6, 3, 0,
                                       -3, 0, 3, 0,
                                        1, 4, 1, 0);

            case CurveType.CatmullRom:
                return 0.5f * new MyMatrix4x4(-1, 3, -3, 1,
                                                    2, -5, 4, -1,
                                                   -1, 0, 1, 0,
                                                    0, 2, 0, 0);
        }

        return null;
    }

    void DisplayCurve(Vector3 point1, Vector3 point2,Vector3 point3, Vector3 point4)
    {
        for (float t = 0.0f; t < 1; t += 0.001f)
        {
            MyMatrix1x4 T = new MyMatrix1x4(Mathf.Pow(t, 3), Mathf.Pow(t, 2), t, 1);
            MyMatrix1x4 T2 = new MyMatrix1x4(Mathf.Pow(t + 0.001f, 3), Mathf.Pow(t + 0.001f, 2), t + 0.001f, 1);
            MyMatrix4x4 M = GetCurveTypeMatrix();
            MyMatrix3x4 G = new MyMatrix3x4(point1, point2, point3, point4);
            /*
            if (curveType == CurveType.HermitiennesCurve)
                G = new MyMatrix3x4(point1.transform.position, point4.transform.position, point2.transform.position, point3.transform.position);
            */
            Vector3 TMG = (T * M * G).ToVector3();
            Vector3 TMG2 = (T2 * M * G).ToVector3();
            Debug.DrawLine(TMG, TMG2, new Color(0, 0, 0, 1), 0);
        }
    }
}
