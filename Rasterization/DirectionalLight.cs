using System;
using System.Collections.Generic;
using System.Text;

namespace Rasterization
{
    public class DirectionalLight: ILight
    {
        public vector3 Direction { get; set; }
        public vector3 Ambient { get; set; }
        public vector3 Diffuse { get; set; }
        public vector3 Specular { get; set; }
        public float Shininess { get; set; }

        public DirectionalLight(vector3 direction, vector3 ambient, vector3 diffuse, vector3 specular, float shininess)
        {
            Direction = direction;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        public vector3 Calculate(VertexProcessor vert, vector3 position, vector3 normal)
        {
            vector3 L = Direction;
            vector3 N = vert.tr_obj2view3(normal).Normalize();
            vector3 V = vert.tr_obj2view4(position * -1).Normalize();
            vector3 R = vector3.Reflect(L, N).Normalize();

            float diff = VertexProcessor.Saturate(L.Dot(N));
            float spec = (float)Math.Pow(VertexProcessor.Saturate(R.Dot(V)), Shininess);

            return VertexProcessor.Saturate(Ambient + Diffuse * diff + Specular * spec);
        }
    }
}
