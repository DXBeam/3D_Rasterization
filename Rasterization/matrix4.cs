using System;
using System.Collections.Generic;
using System.Text;

namespace Rasterization
{
    public class matrix4
    {
        public float[,] mat;

        public matrix4()
        {
            mat = new float[4, 4];
            FillMatrix();
        }

        public matrix4(vector4 v1, vector4 v2, vector4 v3, vector4 v4)
        {
            mat = new float[4, 4];
            
            mat[0, 0] = v1.X;
            mat[1, 0] = v1.Y;
            mat[2, 0] = v1.Z;
            mat[3, 0] = v1.W;

            mat[0, 1] = v2.X;
            mat[1, 1] = v2.Y;
            mat[2, 1] = v2.Z;
            mat[3, 1] = v2.W;

            mat[0, 2] = v3.X;
            mat[1, 2] = v3.Y;
            mat[2, 2] = v3.Z;
            mat[3, 2] = v3.W;

            mat[0, 3] = v4.X;
            mat[1, 3] = v4.Y;
            mat[2, 3] = v4.Z;
            mat[3, 3] = v4.W;

        }

        public matrix4(float a1, float a2, float a3, float a4,
            float b1, float b2, float b3, float b4,
            float c1, float c2, float c3, float c4,
            float d1, float d2, float d3, float d4)
        {

            mat = new float[4, 4];

            mat[0, 0] = a1;
            mat[0, 1] = a2;
            mat[0, 2] = a3;
            mat[0, 3] = a4;

            mat[1, 0] = b1;
            mat[1, 1] = b2;
            mat[1, 2] = b3;
            mat[1, 3] = b4;

            mat[2, 0] = c1;
            mat[2, 1] = c2;
            mat[2, 2] = c3;
            mat[2, 3] = c4;

            mat[3, 0] = d1;
            mat[3, 1] = d2;
            mat[3, 2] = d3;
            mat[3, 3] = d4;
        }

        void FillMatrix()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    mat[i, j] = 0;
                }

            mat[0, 0] = 1;
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
        }

        public vector4 MultiplyMatrixByVector(vector4 vec) //Column major order
        {
            return new vector4
            {
                X = mat[0, 0] * vec.X + mat[0, 1] * vec.Y + mat[0, 2] * vec.Z + mat[0, 3] * vec.W,
                Y = mat[1, 0] * vec.X + mat[1, 1] * vec.Y + mat[1, 2] * vec.Z + mat[1, 3] * vec.W,
                Z = mat[2, 0] * vec.X + mat[2, 1] * vec.Y + mat[2, 2] * vec.Z + mat[2, 3] * vec.W,
                W = mat[3, 0] * vec.X + mat[3, 1] * vec.Y + mat[3, 2] * vec.Z + mat[3, 3] * vec.W
            };
        }

        public matrix4 MultiplyMatrixByMatrix(matrix4 matrix2)
        {
            matrix4 m = new matrix4();

            // First row
            m.mat[0,0] = mat[0,0] * matrix2.mat[0,0] + mat[0,1] * matrix2.mat[1,0] + mat[0,2] * matrix2.mat[2,0] + mat[0,3] * matrix2.mat[3,0];
            m.mat[0,1] = mat[0,0] * matrix2.mat[0,1] + mat[0,1] * matrix2.mat[1,1] + mat[0,2] * matrix2.mat[2,1] + mat[0,3] * matrix2.mat[3,1];
            m.mat[0,2] = mat[0,0] * matrix2.mat[0,2] + mat[0,1] * matrix2.mat[1,2] + mat[0,2] * matrix2.mat[2,2] + mat[0,3] * matrix2.mat[3,2];
            m.mat[0,3] = mat[0,0] * matrix2.mat[0,3] + mat[0,1] * matrix2.mat[1,3] + mat[0,2] * matrix2.mat[2,3] + mat[0,3] * matrix2.mat[3,3];

            // Second row
            m.mat[1,0] = mat[1,0] * matrix2.mat[0,0] + mat[1,1] * matrix2.mat[1,0] + mat[1,2] * matrix2.mat[2,0] + mat[1,3] * matrix2.mat[3,0];
            m.mat[1,1] = mat[1,0] * matrix2.mat[0,1] + mat[1,1] * matrix2.mat[1,1] + mat[1,2] * matrix2.mat[2,1] + mat[1,3] * matrix2.mat[3,1];
            m.mat[1,2] = mat[1,0] * matrix2.mat[0,2] + mat[1,1] * matrix2.mat[1,2] + mat[1,2] * matrix2.mat[2,2] + mat[1,3] * matrix2.mat[3,2];
            m.mat[1,3] = mat[1,0] * matrix2.mat[0,3] + mat[1,1] * matrix2.mat[1,3] + mat[1,2] * matrix2.mat[2,3] + mat[1,3] * matrix2.mat[3,3];

            // Third row
            m.mat[2,0] = mat[2,0] * matrix2.mat[0,0] + mat[2,1] * matrix2.mat[1,0] + mat[2,2] * matrix2.mat[2,0] + mat[2,3] * matrix2.mat[3,0];
            m.mat[2,1] = mat[2,0] * matrix2.mat[0,1] + mat[2,1] * matrix2.mat[1,1] + mat[2,2] * matrix2.mat[2,1] + mat[2,3] * matrix2.mat[3,1];
            m.mat[2,2] = mat[2,0] * matrix2.mat[0,2] + mat[2,1] * matrix2.mat[1,2] + mat[2,2] * matrix2.mat[2,2] + mat[2,3] * matrix2.mat[3,2];
            m.mat[2,3] = mat[2,0] * matrix2.mat[0,3] + mat[2,1] * matrix2.mat[1,3] + mat[2,2] * matrix2.mat[2,3] + mat[2,3] * matrix2.mat[3,3];

            // Fourth row
            m.mat[3,0] = mat[3,0] * matrix2.mat[0,0] + mat[3,1] * matrix2.mat[1,0] + mat[3,2] * matrix2.mat[2,0] + mat[3,3] * matrix2.mat[3,0];
            m.mat[3,1] = mat[3,0] * matrix2.mat[0,1] + mat[3,1] * matrix2.mat[1,1] + mat[3,2] * matrix2.mat[2,1] + mat[3,3] * matrix2.mat[3,1];
            m.mat[3,2] = mat[3,0] * matrix2.mat[0,2] + mat[3,1] * matrix2.mat[1,2] + mat[3,2] * matrix2.mat[2,2] + mat[3,3] * matrix2.mat[3,2];
            m.mat[3,3] = mat[3,0] * matrix2.mat[0,3] + mat[3,1] * matrix2.mat[1,3] + mat[3,2] * matrix2.mat[2,3] + mat[3,3] * matrix2.mat[3,3];

            return new matrix4(
                m.mat[0,0], m.mat[0,1], m.mat[0,2], m.mat[0,3],
                m.mat[1,0], m.mat[1,1], m.mat[1,2], m.mat[1,3],
                m.mat[2,0], m.mat[2,1], m.mat[2,2], m.mat[2,3],
                m.mat[3,0], m.mat[3,1], m.mat[3,2], m.mat[3,3]);
        }

        public matrix4 Transpose()
        {
            matrix4 transposedMatrix = new matrix4();

            for (int x = 0; x < mat.GetLength(0); x++)
                for (int y = 0; y < mat.GetLength(1); y++)
                {
                    transposedMatrix.mat[y, x] = this.mat[x, y];
                }

            return transposedMatrix;
        }

        public void ShowMatrix()
        {
            Console.WriteLine("\nMacierz: ");
            Console.WriteLine(mat[0, 0] + "\t" + mat[0, 1] + "\t" + mat[0, 2] + "\t" + mat[0, 3]);
            Console.WriteLine(mat[1, 0] + "\t" + mat[1, 1] + "\t" + mat[1, 2] + "\t" + mat[1, 3]);
            Console.WriteLine(mat[2, 0] + "\t" + mat[2, 1] + "\t" + mat[2, 2] + "\t" + mat[2, 3]);
            Console.WriteLine(mat[3, 0] + "\t" + mat[3, 1] + "\t" + mat[3, 2] + "\t" + mat[3, 3]);
        }

        public override string ToString()
        {
            return $"[{mat[0, 0]}, {mat[0, 1]}, {mat[0, 2]}, {mat[0, 3]}]\n[{mat[1, 0]}, {mat[1, 1]}, {mat[1, 2]}, {mat[1, 3]}]\n[{mat[2, 0]}, {mat[2, 1]}, {mat[2, 2]}, {mat[2, 3]}]\n[{mat[3, 0]}, {mat[3, 1]}, {mat[3, 2]}, {mat[3, 3]}]";
        }
    }
}
