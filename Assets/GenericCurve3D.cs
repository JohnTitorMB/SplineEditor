using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCurve3D : MonoBehaviour
{
    GameObject point1;
    GameObject point2;
    GameObject point3;
    GameObject point4;

    GameObject point5;
    GameObject point6;
    GameObject point7;
    GameObject point8;

    GameObject point9;
    GameObject point10;
    GameObject point11;
    GameObject point12;

    GameObject point13;
    GameObject point14;
    GameObject point15;
    GameObject point16;

    [SerializeField]
    GameObject Cpoint;

    [SerializeField]
    Vector3[] vertices;

    [SerializeField]
    int[] indices;

    MeshFilter meshFilter;
    private void Start()
    {
        point1 = Instantiate(Cpoint, new Vector3(0, 0, 0), Quaternion.identity);
        point2 = Instantiate(Cpoint, new Vector3(0, 1, 1), Quaternion.identity);
        point3 = Instantiate(Cpoint, new Vector3(0, 1, 2), Quaternion.identity);
        point4 = Instantiate(Cpoint, new Vector3(0, 0, 3), Quaternion.identity);

        point5 = Instantiate(Cpoint, new Vector3(1, 0, 0), Quaternion.identity);
        point6 = Instantiate(Cpoint, new Vector3(1, 1, 1), Quaternion.identity);
        point7 = Instantiate(Cpoint, new Vector3(1, 1, 2), Quaternion.identity);
        point8 = Instantiate(Cpoint, new Vector3(1, 0, 3), Quaternion.identity);

        point9 = Instantiate(Cpoint, new Vector3(2, 0, 0), Quaternion.identity);
        point10 = Instantiate(Cpoint, new Vector3(2, 1, 1), Quaternion.identity);
        point11 = Instantiate(Cpoint, new Vector3(2, 1, 2), Quaternion.identity);
        point12 = Instantiate(Cpoint, new Vector3(2, 0, 3), Quaternion.identity);

        point13 = Instantiate(Cpoint, new Vector3(3, 0, 0), Quaternion.identity);
        point14 = Instantiate(Cpoint, new Vector3(3, 1, 1), Quaternion.identity);
        point15 = Instantiate(Cpoint, new Vector3(3, 1, 2), Quaternion.identity);
        point16 = Instantiate(Cpoint, new Vector3(3, 0, 3), Quaternion.identity);
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(GenereCurve3D());
    }

    IEnumerator GenereCurve3D()
    {
        Vector3 p1 = point1.transform.position;
        Vector3 p2 = point2.transform.position;
        Vector3 p3 = point3.transform.position;
        Vector3 p4 = point4.transform.position;
        Vector3 p5 = point5.transform.position;
        Vector3 p6 = point6.transform.position;
        Vector3 p7 = point7.transform.position;
        Vector3 p8 = point8.transform.position;
        Vector3 p9 = point9.transform.position;
        Vector3 p10 = point10.transform.position;

        Vector3 p11 = point11.transform.position;
        Vector3 p12 = point12.transform.position;
        Vector3 p13 = point13.transform.position;
        Vector3 p14 = point14.transform.position;

        Vector3 p15 = point15.transform.position;
        Vector3 p16 = point16.transform.position;

        Mesh mesh = meshFilter.mesh;
        vertices = new Vector3[100];
        indices = new int[600];
        int k = 0;
        int indice = 0;
        for (float s = 0.0f; s < 1.0f; s += 0.1f)
        {
            for (float t = 0.0f; t < 1; t += 0.1f)
            {
                MyMatrix1x4 T = new MyMatrix1x4(Mathf.Pow(t, 3), Mathf.Pow(t, 2), t, 1);
                MyMatrix1x4 T2 = new MyMatrix1x4(Mathf.Pow(t + 0.001f, 3), Mathf.Pow(t + 0.001f, 2), t + 0.001f, 1);

                MyMatrix1x4 S = new MyMatrix1x4(Mathf.Pow(s, 3), Mathf.Pow(s, 2), s, 1);
                MyMatrix1x4 S2 = new MyMatrix1x4(Mathf.Pow(s + 0.001f, 3), Mathf.Pow(s + 0.001f, 2), s + 0.001f, 1);

                MyMatrix4x4 M = new MyMatrix4x4(-1, 3, -3, 1,
                                                 3, -6, 3, 0,
                                                -3, 3, 0, 0,
                                                 1, 0, 0, 0);
                // MyMatrix3x4 G = new MyMatrix3x4(p1, p5, p9,p13);

                Vector3 TMG_1 = (S * M * new MyMatrix3x4(p1, p5, p9, p13)).ToVector3();
                Vector3 TMG_2 = (S * M * new MyMatrix3x4(p2, p6, p10, p14)).ToVector3();
                Vector3 TMG_3 = (S * M * new MyMatrix3x4(p3, p7, p11, p15)).ToVector3();
                Vector3 TMG_4 = (S * M * new MyMatrix3x4(p4, p8, p12, p16)).ToVector3();

                Vector3 TMGFinal = (T * M * new MyMatrix3x4(TMG_1, TMG_2, TMG_3, TMG_4)).ToVector3();

                Vector3 TMG2_1 = (S2 * M * new MyMatrix3x4(p1, p5, p9, p13)).ToVector3();
                Vector3 TMG2_2 = (S2 * M * new MyMatrix3x4(p2, p6, p10, p14)).ToVector3();
                Vector3 TMG2_3 = (S2 * M * new MyMatrix3x4(p3, p7, p11, p15)).ToVector3();
                Vector3 TMG2_4 = (S2 * M * new MyMatrix3x4(p4, p8, p12, p16)).ToVector3();

                Vector3 TMGFinal2 = (T * M * new MyMatrix3x4(TMG2_1, TMG2_2, TMG2_3, TMG2_4)).ToVector3();

                Debug.DrawLine(TMGFinal, TMGFinal2, new Color(0, 0, 0, 1), 10000);

                Debug.Log(TMGFinal);
                vertices[k] = TMGFinal;
                if(t+0.1f<1 && s + 0.1f < 1)
                {
                    indices[indice] = k+10;
                    indices[indice+1] = k;
                    indices[indice + 2] = k+1;

                    indices[indice+3] = k + 10;
                    indices[indice + 4] = k+1;
                    indices[indice + 5] = k + 11;
                    indice += 6;
                }

                k++;
            }
            
        }
        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;

        yield return null;

    }

    void Update()
    {
        Vector3 p1 = point1.transform.position;
        Vector3 p2 = point2.transform.position;
        Vector3 p3 = point3.transform.position;
        Vector3 p4 = point4.transform.position;
        Vector3 p5 = point5.transform.position;
        Vector3 p6 = point6.transform.position;
        Vector3 p7 = point7.transform.position;
        Vector3 p8 = point8.transform.position;
        Vector3 p9 = point9.transform.position;
        Vector3 p10 = point10.transform.position;

        Vector3 p11 = point11.transform.position;
        Vector3 p12 = point12.transform.position;
        Vector3 p13 = point13.transform.position;
        Vector3 p14 = point14.transform.position;

        Vector3 p15 = point15.transform.position;
        Vector3 p16 = point16.transform.position;

        Mesh mesh = meshFilter.mesh;
        vertices = mesh.vertices;
        int k = 0;
        for (float s = 0.0f; s < 1.0f; s += 0.1f)
        {
            for (float t = 0.0f; t < 1; t += 0.1f)
            {
                MyMatrix1x4 T = new MyMatrix1x4(Mathf.Pow(t, 3), Mathf.Pow(t, 2), t, 1);
                MyMatrix1x4 T2 = new MyMatrix1x4(Mathf.Pow(t + 0.001f, 3), Mathf.Pow(t + 0.001f, 2), t + 0.001f, 1);

                MyMatrix1x4 S = new MyMatrix1x4(Mathf.Pow(s, 3), Mathf.Pow(s, 2), s, 1);
                MyMatrix1x4 S2 = new MyMatrix1x4(Mathf.Pow(s + 0.001f, 3), Mathf.Pow(s + 0.001f, 2), s + 0.001f, 1);

                MyMatrix4x4 M = new MyMatrix4x4(-1, 3, -3, 1,
                                                 3, -6, 3, 0,
                                                -3, 3, 0, 0,
                                                 1, 0, 0, 0);

                Vector3 TMG_1 = (S * M * new MyMatrix3x4(p1, p5, p9, p13)).ToVector3();
                Vector3 TMG_2 = (S * M * new MyMatrix3x4(p2, p6, p10, p14)).ToVector3();
                Vector3 TMG_3 = (S * M * new MyMatrix3x4(p3, p7, p11, p15)).ToVector3();
                Vector3 TMG_4 = (S * M * new MyMatrix3x4(p4, p8, p12, p16)).ToVector3();

                Vector3 TMGFinal = (T * M * new MyMatrix3x4(TMG_1, TMG_2, TMG_3, TMG_4)).ToVector3();

                Vector3 TMG2_1 = (S2 * M * new MyMatrix3x4(p1, p5, p9, p13)).ToVector3();
                Vector3 TMG2_2 = (S2 * M * new MyMatrix3x4(p2, p6, p10, p14)).ToVector3();
                Vector3 TMG2_3 = (S2 * M * new MyMatrix3x4(p3, p7, p11, p15)).ToVector3();
                Vector3 TMG2_4 = (S2 * M * new MyMatrix3x4(p4, p8, p12, p16)).ToVector3();

                Vector3 TMGFinal2 = (T * M * new MyMatrix3x4(TMG2_1, TMG2_2, TMG2_3, TMG2_4)).ToVector3();

                vertices[k] = TMGFinal;
                k++;
            }

        }
        mesh.vertices = vertices;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }
}
