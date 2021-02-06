using System;
using System.Collections.Generic;
using System.Text;

namespace Rasterization
{
    public class Sphere: Mesh
    {
        float radius, widthSegments, heightSegments, phiStart, thetaStart;
        double phiLength, thetaLength;
        public Sphere(float radius = 1f, float widthSegments = 8f, float heightSegments = 6f, float phiStart = 0f, double phiLength = Math.PI * 2,
			float thetaStart = 0, double thetaLength = Math.PI)
        {
            this.radius = radius;
            this.widthSegments = widthSegments;
            this.heightSegments = heightSegments;
            this.phiStart = phiStart;
            this.phiLength = phiLength;
            this.thetaStart = thetaStart;
            this.thetaLength = thetaLength;

			Create();
        }

        public void Create()
        {
			widthSegments = (float)Math.Max(3, Math.Floor(widthSegments));
			heightSegments = (float)Math.Max(2, Math.Floor(heightSegments));

			float thetaEnd = (float)Math.Min(thetaStart + thetaLength, Math.PI);

			int index = 0;
			List<List<int>> grid = new List<List<int>>();

			vector3 vertex = new vector3(0,0,0);
			vector3 normal = new vector3(0,0,0);

			// generate vertices, normals and uvs

			for (int iy = 0; iy <= heightSegments; iy++)
			{

				List<int> verticesRow = new List<int>();

				float v = (float)iy / heightSegments;

				// special case for the poles

				float uOffset = 0f;

				if (iy == 0 && thetaStart == 0)
				{

					uOffset = 0.5f / widthSegments;

				}
				else if (iy == heightSegments && thetaEnd == Math.PI)
				{

					uOffset = -0.5f / widthSegments;

				}

				for (int ix = 0; ix <= widthSegments; ix++)
				{

					float u = (float)ix / widthSegments;

					// vertex

					vertex.X = (float)(-radius * Math.Cos(phiStart + u * phiLength) * Math.Sin(thetaStart + v * thetaLength));
					vertex.Y = (float)(radius * Math.Cos(thetaStart + v * thetaLength));
					vertex.Z = (float)(radius * Math.Sin(phiStart + u * phiLength) * Math.Sin(thetaStart + v * thetaLength));

					vertices.Add(vertex.Normalize());

					// normal

					normal = new vector3(vertex.X, vertex.Y, vertex.Z).Normalize();
					normals.Add(normal.Normalize());

					// uv

					uvs.Add(new vector3(u + uOffset, 1 - v, 0));

					verticesRow.Add(index++);

				}

				grid.Add(verticesRow);

			}

			// indices

			for (int iy = 0; iy < heightSegments; iy++)
			{

				for (int ix = 0; ix < widthSegments; ix++)
				{

					int a = grid[iy][ix + 1];
					int b = grid[iy][ix];
					int c = grid[iy + 1][ix];
					int d = grid[iy + 1][ix + 1];

					if (iy != 0 || thetaStart > 0)
					{
						indexes.Add(d);
						indexes.Add(b);
						indexes.Add(a);
					}

					if (iy != heightSegments - 1 || thetaEnd < Math.PI)
					{
						indexes.Add(d);
						indexes.Add(c);
						indexes.Add(b);
					}

				}

			}
		}
    }
}
