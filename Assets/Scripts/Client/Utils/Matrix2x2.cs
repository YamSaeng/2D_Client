using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix2x2
{
    Vector2[] Cols = new Vector2[2];

    public Matrix2x2()
    {
        Cols[0] = Vector2.right;
        Cols[1] = Vector2.up;
    }

    public Matrix2x2(Vector2 Col0, Vector2 Col1)
    {
        Cols[0] = Col0;
        Cols[1] = Col1;
    }

    public Vector2 this[byte Index]
    {
        get
        {
            return Cols[Index];
        }
    }

    public static Vector2 operator *(Matrix2x2 Matrix, Vector2 Vector)
    {
        Matrix2x2 transposedMatrix = Matrix.Transpose();
        return new Vector2(Vector2.Dot(transposedMatrix[0], Vector), Vector2.Dot(transposedMatrix[1], Vector));
    }

    Matrix2x2 Transpose()
    {
        Vector2 AVector = new Vector2(Cols[0].x, Cols[1].x);
        Vector2 BVector = new Vector2(Cols[0].y, Cols[1].y);

        Matrix2x2 TransposeMatrix = new Matrix2x2(AVector, BVector);

        return TransposeMatrix;
    }
}