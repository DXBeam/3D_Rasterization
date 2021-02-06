using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterization
{
    public class vector3
    {
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Z { get; set; } = 0;

        public vector3 (float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static vector3 operator *(vector3 a, float b) => new vector3(a.X * b, a.Y * b, a.Z * b); //Оператор умножения.
        public static vector3 operator *(vector3 a, vector3 b) => new vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z); //Оператор умножения.
        public static vector3 operator /(vector3 a, float b) => new vector3(a.X / b, a.Y / b, a.Z / b); //Оператор умножения.

        //public static vector3 operator +(vector3 a, vector3 b) => new vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z); //Оператор суммирования.
        public static vector3 operator +(vector3 v, vector3 v2)
        {
            vector3 vect;
            return vect = new vector3(v.X + v2.X, v.Y + v2.Y, v.Z + v2.Z);
        }

        //roznica dwoch wektorow
        public static vector3 operator -(vector3 v, vector3 v2)
        {
            vector3 vect;
            return vect = new vector3(v.X - v2.X, v.Y - v2.Y, v.Z - v2.Z);
        }


        //iloczyn skalarny dwoch wektorow np. do obliczenia cosinusa 
        public float Dot(vector3 v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
        }

        //długość wektora
        public float GetLength()
        {
            return (float)Math.Sqrt(Dot(this));
        }

        const float eps = 0.0001f;

        //normalizacja wektora
        public vector3 Normalize()
        {
            float len = GetLength();
            if (len > eps)
            {
                return (this) * (1 / len);
            }
            else
            {
                return new vector3(.0f, .0f, .0f);
            }
        }
        public vector3 Cross(vector3 v)
        {
            vector3 vect = new vector3
                (this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X);
            return vect;
        }

        //Otbicie wektora i od normalnej n
        public static vector3 Reflect(vector3 I, vector3 N)
        {
            vector3 Nn = N.Normalize();

            return I - Nn * Nn.Dot(I) * 2.0f;
        }
    }
}
