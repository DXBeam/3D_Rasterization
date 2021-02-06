using System;
using System.Collections.Generic;
using System.Text;

namespace Rasterization
{
    public class ReflectLight: PointLight
    {
        private float angle;
        private float cutOff;

        public vector3 Direction { get; set; }
        public float Angle
        {
            get => angle;
            set
            {
                angle = value;
                cutOff = 1f - (value / 180f);
            }
        }

        public ReflectLight(vector3 position, vector3 direction, float angle, vector3 ambient, vector3 diffuse, vector3 specular, float shininess)
            : base(position, ambient, diffuse, specular, shininess)
        {
            Direction = direction;
            Angle = angle;
        }

        public override vector3 Calculate(VertexProcessor vert, vector3 position, vector3 normal)
        {
            vector3 N = vert.tr_obj2view3(normal).Normalize();
            vector3 V = vert.tr_obj2view4(position * -1);
            vector3 L = (Position - V).Normalize();
            V = V.Normalize();

            vector3 D = Position - position;
            D *= -1;
            float theta = Direction.Normalize().Dot(D.Normalize());

            if (theta > cutOff)
            {
                vector3 R = vector3.Reflect(L, N).Normalize();

                float diff = VertexProcessor.Saturate(L.Dot(N));
                float spec = (float)Math.Pow(VertexProcessor.Saturate(R.Dot(V)), Shininess);

                return VertexProcessor.Saturate(Ambient + Diffuse * diff + Specular * spec);
            }
            else
                return VertexProcessor.Saturate(Ambient);

        }
    }
}
