using System;
using System.Drawing;

namespace Rasterization
{
    class Program
    {
        static void Main(string[] args)
        {
            
            vector3 testv = new vector3(250, 1123, 895);
            vector3 testvv = new vector3(250, 1123, 895);
            vector3 test2v = new vector3(510, 932, 487);

            vector4 testV41 = new vector4(748, 741, 256, 201);
            vector4 testV42 = new vector4(348, 241, 656, 101);

            float test3f = 7.4f;
            var testing = (testv / test3f);
            matrix4 matTest = new matrix4(testV41, testV42, testV41, testV42);
            matrix4 matTest2 = new matrix4(testV42, testV41, testV42, testV41);

            // Vector by Vector
            Console.WriteLine("SSE Add Vector3 by Vector3: " + MathSSE.Add(testv, test2v));
            Console.WriteLine("FPU Add Vector3 by Vector3: " + (testv + test2v));
            Console.WriteLine("SSE Substract Vector3 by Vector3: " + MathSSE.Substract(testv, test2v));
            Console.WriteLine("FPU Substract Vector3 by Vector3: " + (testv - test2v));
            Console.WriteLine("SSE Multiply Vector3 by Vector3: " + MathSSE.Multiply(testv, test2v));
            Console.WriteLine("FPU Multiply Vector3 by Vector3: " + (testv * test2v));
            Console.WriteLine("SSE Divide Vector3 by Vector3: " + MathSSE.Divide(testv, test2v));
            Console.WriteLine("FPU Divide Vector3 by Vector3: " + (testv / test2v));

            // Vector by float
            Console.WriteLine("SSE Add Vector3 by float: " + MathSSE.Add(testv, test3f));
            Console.WriteLine("FPU Add Vector3 by float: " + (testv + test3f));
            Console.WriteLine("SSE Substract Vector3 by float: " + MathSSE.Substract(test2v, test3f));
            Console.WriteLine("FPU Substract Vector3 by float: " + (test2v - test3f));
            Console.WriteLine("SSE Multiply Vector3 by float: " + MathSSE.Multiply(testv, test3f));
            Console.WriteLine("FPU Multiply Vector3 by float: " + (testv * test3f));
            Console.WriteLine("SSE Divide Vector3 by float: " + MathSSE.Divide(testv, test3f));
            Console.WriteLine("FPU Divide Vector3 by float: " + (testv / test3f));

            //Vector by float by vector
            MathSSE.Divide(testv, test3f, testv);
            
            Console.WriteLine("SSE Divide Vector3 by float by Vector3:" + testv);
            Console.WriteLine("FPU Divide Vector3 by float by Vector3:" + testing);

            //Normalize
            Console.WriteLine("SSE Normalize:" + MathSSE.Normalize(testv));
            Console.WriteLine("FPU Normalize:" + (testv.Normalize()));

            //Dot
            Console.WriteLine("SSE Dot:" + MathSSE.Dot(testv, test2v));
            Console.WriteLine("FPU Dot:" + (testv.Dot(test2v)));

            //Cross
            Console.WriteLine("SSE Cross:" + MathSSE.CrossProduct(testv, test2v));
            Console.WriteLine("FPU Cross:" + (testv.Cross(test2v)));

            //Reflect
            Console.WriteLine("SSE Reflect:" + MathSSE.Reflect(testv, test2v));
            Console.WriteLine("FPU Reflect:" + vector3.Reflect(testv, test2v));

            //Saturate
            Console.WriteLine("SSE Saturate:" + MathSSE.Saturate(testv));
            Console.WriteLine("FPU Saturate:" + VertexProcessor.Saturate(testv));

            //Vector 4
            Console.WriteLine("SSE Add Vector4 by Vector4: " + MathSSE.Add(testV41, testV42));
            Console.WriteLine("FPU Add Vector4 by Vector4: " + (testV41 + testV42));
            Console.WriteLine("SSE Substract Vector4 by Vector4: " + MathSSE.Substract(testV41, testV42));
            Console.WriteLine("FPU Substract Vector4 by Vector4: " + (testV41 - testV42));
            Console.WriteLine("SSE Multiply Vector4 by Vector4: " + MathSSE.Multiply(testV41, testV42));
            Console.WriteLine("FPU Multiply Vector4 by Vector4: " + (testV41 * testV42));
            Console.WriteLine("SSE Divide Vector4 by Vector4: " + MathSSE.Divide(testV41, testV42));
            Console.WriteLine("FPU Divide Vector4 by Vector4: " + (testV41 / testV42));
            Console.WriteLine("SSE Normalize Vector4: " + MathSSE.Normalize(testV41));
            Console.WriteLine("FPU Normalize Vector4: " + testV41.Normalize2(testV41));

            //Matrix by Matrix mul
            Console.WriteLine("SSE Matrix by Matrix mul:\n" + MathSSE.Mul(matTest, matTest2));
            Console.WriteLine("FPU Matrix Matrix mul:\n" + (matTest.MultiplyMatrixByMatrix(matTest2)));

            // Matrix bu Vector
            Console.WriteLine("SSE Matrix by Vector4 mul:\n" + MathSSE.Mul(matTest, testV41));
            Console.WriteLine("FPU Matrix by Vector4 mul:\n" + (matTest.MultiplyMatrixByVector(testV41)));
        }
    }
}