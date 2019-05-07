using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MyMatrix
{
    public void Display()
    {
        foreach (float[] row in Matrix)
        {
            string str = "Row :";
            foreach (float value in row)
                str += " " + value;

            Debug.Log(str);
        }
    }

    private float[][] matrix;

    public float[][] Matrix
    {
        get
        {
            return matrix;
        }

        set
        {
            matrix = value;
        }
    }
}

class MyMatrix1x4 : MyMatrix
{
    public MyMatrix1x4(float m1, float m2, float m3, float m4)
    {
        Matrix = new float[][] { new float[] { m1, m2, m3, m4 } };
    }

    public MyMatrix1x4(Vector4 row)
    {
        Matrix = new float[][] { new float[] { row.x, row.y, row.z, row.w } };
    }

    public static MyMatrix1x4 operator *(MyMatrix1x4 lhs, MyMatrix4x4 rhs)
    {
        float[][] m1 = lhs.Matrix;
        float[][] m2 = rhs.Matrix;

        return new MyMatrix1x4(m1[0][0] * m2[0][0] + m1[0][1] * m2[1][0] + m1[0][2] * m2[2][0] + m1[0][3] * m2[3][0],
                               m1[0][0] * m2[0][1] + m1[0][1] * m2[1][1] + m1[0][2] * m2[2][1] + m1[0][3] * m2[3][1],
                               m1[0][0] * m2[0][2] + m1[0][1] * m2[1][2] + m1[0][2] * m2[2][2] + m1[0][3] * m2[3][2],
                               m1[0][0] * m2[0][3] + m1[0][1] * m2[1][3] + m1[0][2] * m2[2][3] + m1[0][3] * m2[3][3]);
    }

    public static MyMatrix1x3 operator *(MyMatrix1x4 lhs, MyMatrix3x4 rhs)
    {
        float[][] m1 = lhs.Matrix;
        float[][] m2 = rhs.Matrix;

        return new MyMatrix1x3(m1[0][0] * m2[0][0] + m1[0][1] * m2[1][0] + m1[0][2] * m2[2][0] + m1[0][3] * m2[3][0],
                               m1[0][0] * m2[0][1] + m1[0][1] * m2[1][1] + m1[0][2] * m2[2][1] + m1[0][3] * m2[3][1],
                               m1[0][0] * m2[0][2] + m1[0][1] * m2[1][2] + m1[0][2] * m2[2][2] + m1[0][3] * m2[3][2]);
    }
}

class MyMatrix1x3 : MyMatrix
{
    public MyMatrix1x3(float m1, float m2, float m3)
    {
        Matrix = new float[][] { new float[] { m1, m2, m3 } };
    }

    public MyMatrix1x3(Vector3 row)
    {
        Matrix = new float[][] { new float[] { row.x, row.y, row.z } };
    }

    public Vector3 ToVector3()
    {
        return new Vector3(Matrix[0][0], Matrix[0][1], Matrix[0][2]);
    }
}

class MyMatrix4x1 : MyMatrix
{
    public MyMatrix4x1(float m1, float m2, float m3, float m4)
    {
        Matrix = new float[][] { new float[] { m1 },
                                  new float[] { m2 } ,
                                  new float[] { m3 } ,
                                  new float[] { m4 }};

    }

    public MyMatrix4x1(Vector4 row)
    {
        Matrix = new float[][] { new float[] { row.x },
                                  new float[] { row.y },
                                  new float[] { row.z },
                                  new float[] { row.w } };
    }
}

class MyMatrix4x4 : MyMatrix
{
    public MyMatrix4x4(float m1, float m2, float m3, float m4,
                       float m5, float m6, float m7, float m8,
                       float m9, float m10, float m11, float m12,
                       float m13, float m14, float m15, float m16)
    {
        Matrix = new float[][] { new float[] { m1,m2,m3,m4 },
                                  new float[] { m5, m6, m7, m8 } ,
                                  new float[] { m9, m10, m11, m12 } ,
                                  new float[] { m13,m14,m15,m16 }};
    }

    public MyMatrix4x4(Vector4 row, Vector4 row2, Vector4 row3, Vector4 row4)
    {
        Matrix = new float[][] { new float[] { row.x,row.y,row.z,row.w },
                                  new float[] { row2.x, row2.y, row2.z, row2.w } ,
                                  new float[] { row3.x, row3.y, row3.z, row3.w } ,
                                  new float[] { row4.x,row4.y,row4.z,row4.w }};
    }

    public static MyMatrix4x4 operator *(float lhs, MyMatrix4x4 rhs)
    {
        float[][] m1 = rhs.Matrix;

        return new MyMatrix4x4( m1[0][0] * lhs, m1[0][1] * lhs, m1[0][2] * lhs, m1[0][3] * lhs,
                                m1[1][0] * lhs, m1[1][1] * lhs, m1[1][2] * lhs, m1[1][3] * lhs,
                                m1[2][0] * lhs, m1[2][1] * lhs, m1[2][2] * lhs, m1[2][3] * lhs,
                                m1[3][0] * lhs, m1[3][1] * lhs, m1[3][2] * lhs, m1[3][3] * lhs);
    }
}
class MyMatrix3x4 : MyMatrix
{
    public MyMatrix3x4(float m1, float m2, float m3,
                       float m4, float m5, float m6,
                       float m7, float m8, float m9,
                       float m10, float m11, float m12)
    {
        Matrix = new float[][] { new float[] { m1,m2,m3},
                                  new float[] { m4, m5, m6},
                                  new float[] { m7, m8, m9 },
                                  new float[] { m10,m11,m12 }};
    }

    public MyMatrix3x4(Vector3 row, Vector3 row2, Vector3 row3, Vector3 row4)
    {
        Matrix = new float[][] { new float[] { row.x,row.y,row.z },
                                  new float[] { row2.x, row2.y, row2.z } ,
                                  new float[] { row3.x, row3.y, row3.z} ,
                                  new float[] { row4.x,row4.y,row4.z }};
    }
}