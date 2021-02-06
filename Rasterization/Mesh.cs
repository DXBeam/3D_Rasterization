using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Rasterization
{
    public class Mesh
    {
        public List<vector3> vertices = new List<vector3>();
        public List<vector3> normals = new List<vector3>();
        public List<int> indexes = new List<int>();
        public List<vector3> uvs = new List<vector3>();

        public void Draw(Rasterization raster, VertexProcessor vertex)
        {
            for (int i = 0; i < indexes.Count; i += 3)
            {
                raster.Triangle(vertex.tr(vertices[indexes[i]]), vertex.tr(vertices[indexes[i + 1]]), vertex.tr(vertices[indexes[i + 2]]),
                new vector3(255f, 0f, 0f), new vector3(0f, 255f, 0f), new vector3(0f, 0f, 255f));
            }
        }

        public void Draw(Rasterization raster, VertexProcessor vertex, ILight light)
        {
            for (int i = 0; i < indexes.Count; i += 3)
            {
                raster.Triangle(vertex.tr(vertices[indexes[i]]), vertex.tr(vertices[indexes[i + 1]]), vertex.tr(vertices[indexes[i + 2]]),
                light.Calculate(vertex, vertices[indexes[i]], normals[indexes[i]]),
                light.Calculate(vertex, vertices[indexes[i+1]], normals[indexes[i+1]]),
                light.Calculate(vertex, vertices[indexes[i+2]], normals[indexes[i+2]]));
            }
        }

        public void DrawPixelLight(Rasterization raster, VertexProcessor vertex, ILight light)
        {
            for (int i = 0; i < indexes.Count; i += 3)
            {
                raster.Triangle(vertex.tr(vertices[indexes[i]]), vertex.tr(vertices[indexes[i + 1]]), vertex.tr(vertices[indexes[i + 2]]),
                normals[indexes[i]],
                normals[indexes[i + 1]],
                normals[indexes[i + 2]],
                light,
                vertex);
            }
        }

        public void DrawPixelLight(Rasterization raster, VertexProcessor vertex, ILight light, Bitmap texture)
        {
            for (int i = 0; i < indexes.Count; i += 3)
            {
                raster.Triangle(vertex.tr(vertices[indexes[i]]), vertex.tr(vertices[indexes[i + 1]]), vertex.tr(vertices[indexes[i + 2]]),
                normals[indexes[i]],
                normals[indexes[i + 1]],
                normals[indexes[i + 2]],
                light,
                vertex,
                uvs[indexes[i]],
                uvs[indexes[i+1]],
                uvs[indexes[i+2]],
                texture);
            }
        }
    }
}
