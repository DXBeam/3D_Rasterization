using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Rasterization
{
    public static class MathSSE
    {
		public static vector3 Add(vector3 v1, vector3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);
			Vector128<float> result = Sse.Add(a, b);
			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Substract(vector3 v1, vector3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);
			Vector128<float> result = Sse.Subtract(a, b);
			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Multiply(vector3 v1, vector3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);
			Vector128<float> result = Sse.Multiply(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Divide(vector3 v1, vector3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static void Divide(vector3 res, vector3 v1, vector3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			res.X = result.GetElement(0);
			res.Y = result.GetElement(1);
			res.Z = result.GetElement(2);
		}

		public static vector3 Add(vector3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Add(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Substract(vector3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Subtract(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Multiply(vector3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Multiply(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector3 Divide(vector3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static void Divide(vector3 res, float f, vector3 v)
		{
			Vector128<float> a = Vector128.Create(v.X, v.Y, v.Z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			res.X = result.GetElement(0);
			res.Y = result.GetElement(1);
			res.Z = result.GetElement(2);
		}

		public static vector3 Normalize(vector3 v)
		{
			float len = v.GetLength();
			Vector128<float> a = Vector128.Create(v.X, v.Y, v.Z, 0f);
			Vector128<float> l = Vector128.Create(len, len, len, 0f);

			Vector128<float> result = Sse.Divide(a, l);

			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float Dot(vector3 v1, vector3 v2)
		{
			if (v1 == null || v2 == null)
				return 0;

			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, 0f);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, 0f);

			Vector128<float> result = Sse.Multiply(a, b);

			return result.GetElement(0) + result.GetElement(1) + result.GetElement(2);
		}

		public static vector3 CrossProduct(vector3 v1, vector3 v2)
		{
			Vector128<float> a1 = Vector128.Create(v1.Y, v1.Z, v1.Z, v1.X);
			Vector128<float> a2 = Vector128.Create(v1.X, v1.Y, 0f, 0f);
			Vector128<float> b1 = Vector128.Create(v2.Z, v2.Y, v2.X, v2.Z);
			Vector128<float> b2 = Vector128.Create(v2.Y, v2.X, 0f, 0f);

			Vector128<float> result1 = Sse.Multiply(a1, b1);
			Vector128<float> result2 = Sse.Multiply(a2, b2);

			return new vector3(result1.GetElement(0) - result1.GetElement(1),
							result1.GetElement(2) - result1.GetElement(3),
							result2.GetElement(0) - result2.GetElement(1));

		}

		public static vector3 Reflect(vector3 I, vector3 N)
		{
			vector3 Nn = N.Normalize();
			return I - Nn * Nn.Dot(I) * 2.0f;
		}


		public static vector3 Saturate(vector3 v)
		{
			Vector128<float> a = Vector128.Create(v.X, v.Y, v.Z, 0f);
			Vector128<float> min = Vector128.Create(1f, 1f, 1f, 0f);
			Vector128<float> max = Vector128.Create(0f, 0f, 0f, 0f);
			Vector128<float> result = Sse.Max(Sse.Min(a, min), max);
			return new vector3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static vector4 Mul(matrix4 matrix, vector4 v)
		{
			Vector128<float> a1 = Vector128.Create(matrix.mat[0, 0], matrix.mat[1, 0], matrix.mat[2, 0], matrix.mat[3, 0]);
			Vector128<float> a2 = Vector128.Create(matrix.mat[0, 1], matrix.mat[1, 1], matrix.mat[2, 1], matrix.mat[3, 1]);
			Vector128<float> a3 = Vector128.Create(matrix.mat[0, 2], matrix.mat[1, 2], matrix.mat[2, 2], matrix.mat[3, 2]);
			Vector128<float> a4 = Vector128.Create(matrix.mat[0, 3], matrix.mat[1, 3], matrix.mat[2, 3], matrix.mat[3, 3]);

			Vector128<float> b = Vector128.Create(v.X, v.Y, v.Z, v.W);

			Vector128<float> res1 = Sse.Multiply(a1, b);
			Vector128<float> res2 = Sse.Multiply(a2, b);
			Vector128<float> res3 = Sse.Multiply(a3, b);
			Vector128<float> res4 = Sse.Multiply(a4, b);

			vector4 result = new vector4(res1.GetElement(0) + res1.GetElement(1) + res1.GetElement(2) + res1.GetElement(3),
										res2.GetElement(0) + res2.GetElement(1) + res2.GetElement(2) + res2.GetElement(3),
										res3.GetElement(0) + res3.GetElement(1) + res3.GetElement(2) + res3.GetElement(3),
										res4.GetElement(0) + res4.GetElement(1) + res4.GetElement(2) + res4.GetElement(3));

			return result;
		}

		public static matrix4 Mul(matrix4 matrix1, matrix4 matrix2)
		{
			matrix4 result = new matrix4(Mul(matrix1, new vector4(matrix2.mat[0, 0], matrix2.mat[0, 1], matrix2.mat[0, 2], matrix2.mat[0, 3])), Mul(matrix1, new vector4(matrix2.mat[1, 0], matrix2.mat[1, 1], matrix2.mat[1, 2], matrix2.mat[1, 3])),
											Mul(matrix1, new vector4(matrix2.mat[2, 0], matrix2.mat[2, 1], matrix2.mat[2, 2], matrix2.mat[2, 3])), Mul(matrix1, new vector4(matrix2.mat[3, 0], matrix2.mat[3, 1], matrix2.mat[3, 2], matrix2.mat[3, 3])));

			return result;
		}

		public static vector4 Add(vector4 v1, vector4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, v1.W);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, v2.W);
			Vector128<float> result = Sse.Add(a, b);
			return new vector4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static vector4 Substract(vector4 v1, vector4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, v1.W);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, v2.W);
			Vector128<float> result = Sse.Subtract(a, b);
			return new vector4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static vector4 Multiply(vector4 v1, vector4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, v1.W);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, v2.W);
			Vector128<float> result = Sse.Multiply(a, b);

			return new vector4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static vector4 Divide(vector4 v1, vector4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.X, v1.Y, v1.Z, v1.W);
			Vector128<float> b = Vector128.Create(v2.X, v2.Y, v2.Z, v2.W);
			Vector128<float> result = Sse.Divide(a, b);

			return new vector4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static vector4 Normalize(vector4 v)
		{
			float len = v.GetLength();
			Vector128<float> a = Vector128.Create(v.X, v.Y, v.Z, v.W);
			Vector128<float> l = Vector128.Create(len, len, len, len);

			Vector128<float> result = Sse.Divide(a, l);

			return new vector4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}
	}
}
