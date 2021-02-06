using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterization
{
    public class VertexProcessor
    {
        public matrix4 view2proj = new matrix4(); //
        public matrix4 world2view = new matrix4(); //
        public matrix4 obj2world = new matrix4(); //

        public matrix4 obj2proj = new matrix4();
        public matrix4 obj2view = new matrix4();

        public VertexProcessor()
        {
            obj2world = new matrix4();

            //skalowanie sceny
            vector3 scale = new vector3(1f, 1f, 1f);
            //MultiplyByScale(scale);

            //obrot sceny
            vector3 rotation = new vector3(0, 0, 1);
            //MultiplyByRotation(0, rotation);

            SetPerspective(45, 1.0f, 0.1f, 10000.0f);

            //kamera
            vector3 eye = new vector3(.2f, 0f, 2f);
            vector3 center = new vector3(0, 0, 0);
            vector3 up = new vector3(0, 1, 0);
            SetLookAt(eye, center, up);

            obj2view = world2view.MultiplyMatrixByMatrix(obj2world);
            obj2proj = view2proj.MultiplyMatrixByMatrix(obj2view);
        }

        //tworzy macierz rzutowania view2proj
        public void SetPerspective(double fovy, float aspect, float near, float far)
        {
            fovy *= Math.PI / 360;
            float f = (float)(Math.Cos(fovy) / Math.Sin(fovy));

            vector4 v1 = new vector4(f / aspect, 0, 0, 0);
            vector4 v2 = new vector4(0, f, 0, 0);
            vector4 v3 = new vector4(0, 0, (far + near) / (near - far), -1);
            vector4 v4 = new vector4(0, 0, (2 * near * far) / (near - far), 0);

            view2proj = new matrix4(v1, v2, v3, v4);
        }

        //tworzy macierz rzutowanie world2view
        public void SetLookAt(vector3 eye, vector3 center, vector3 up)
        {
            vector3 f = center - eye;
            f = f.Normalize();
            up = up.Normalize();
            vector3 s = f.Cross(up);
            vector3 u = s.Cross(f);

            vector4 v1 = new vector4(s.X, u.X, -f.X, 0);
            vector4 v2 = new vector4(s.Y, u.Y, -f.Y, 0);
            vector4 v3 = new vector4(s.Z, u.Z, -f.Z, 0);
            vector4 v4 = new vector4(0, 0, 0, 1);
            world2view = new matrix4(v1, v2, v3, v4);

            matrix4 m = new matrix4();
            m.mat[0, 3] = -eye.X;
            m.mat[1, 3] = -eye.Y;
            m.mat[2, 3] = -eye.Z;
            m.mat[3, 3] = 1.0f;

            world2view = world2view.MultiplyMatrixByMatrix(m);
        }

        //macierz translacji
        public void MultiplyByTranslation(vector3 v)
        {
            vector4 v1 = new vector4(1, 0, 0, 0);
            vector4 v2 = new vector4(0, 1, 0, 0);
            vector4 v3 = new vector4(0, 0, 1, 0);
            vector4 v4 = new vector4(v.X, v.Y, v.Z, 1);

            matrix4 m = new matrix4(v1, v2, v3, v4);

            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        public void MultiplyByScale(vector3 v)
        {
            vector4 v1 = new vector4(v.X, 0, 0, 0);
            vector4 v2 = new vector4(0, v.Y, 0, 0);
            vector4 v3 = new vector4(0, 0, v.Z, 0);
            vector4 v4 = new vector4(0, 0, 0, 1);

            matrix4 m = new matrix4(v1, v2, v3, v4);

            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        //macierz rotacji
        public void MultiplyByRotation(double a, vector3 v)
        {
            float s = (float)Math.Sin(a * (Math.PI / 180));
            float c = (float)Math.Cos(a * (Math.PI / 180));
            v = v.Normalize();

            vector4 v1 = new vector4(v.X * v.X * (1f - c) + c, v.Y * v.X * (1f - c) + v.Z * s, v.X * v.Z * (1f - c) - v.Y * s, 0f);
            vector4 v2 = new vector4(v.X * v.Y * (1f - c) - v.Z * s, v.Y * v.Y * (1f - c) + c, v.Y * v.Z * (1f - c) + v.X * s, 0f);
            vector4 v3 = new vector4(v.X * v.Z * (1f - c) + v.Y * s, v.Y * v.Z * (1f - c) - v.X * s, v.Z * v.Z * (1f - c) + c, 0f);
            vector4 v4 = new vector4(0, 0, 0, 1);

            matrix4 m = new matrix4(v1, v2, v3, v4);
            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        //przemnozenie wierzcholka przez macierze
        public vector3 tr(vector3 vertexPosition)
        {
            vector4 verPos = new vector4(vertexPosition);
            verPos = obj2proj.MultiplyMatrixByVector(verPos);
            vector3 finalPos = new vector3(verPos.X / verPos.W, verPos.Y / verPos.W, verPos.Z / verPos.W);
            return finalPos;
        }

        public vector3 tr_obj2view4(vector3 vertexPosition)
        {
            vector4 verPos = new vector4(vertexPosition);
            verPos = obj2view.MultiplyMatrixByVector(verPos);
            vector3 finalPos = new vector3(verPos.X / verPos.W, verPos.Y / verPos.W, verPos.Z / verPos.W);
            return finalPos;
        }
        public vector3 tr_obj2view3(vector3 vertexPosition)
        {
            vector4 verPos = new vector4(vertexPosition.X, vertexPosition.Y, vertexPosition.Z, 0f);
            verPos = obj2view.MultiplyMatrixByVector(verPos);
            vector3 finalPos = new vector3(verPos.X, verPos.Y, verPos.Z);
            return finalPos;
        }

        public void GetMatrix()
        {
            obj2view = world2view.MultiplyMatrixByMatrix(obj2world);
            obj2proj = view2proj.MultiplyMatrixByMatrix(obj2view);
        }

        public static float Saturate(float value)
        {
            return Math.Clamp(value, 0.0f, 1.0f);
        }

        public static vector3 Saturate(vector3 value)
        {
            return new vector3(Math.Clamp(value.X, 0.0f, 255.0f),
                Math.Clamp(value.Y, 0.0f, 255.0f),
                Math.Clamp(value.Z, 0.0f, 255.0f));
        }
    }
}
