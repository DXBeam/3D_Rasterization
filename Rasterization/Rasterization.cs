using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rasterization
{
    public class Rasterization
    {
        Buffer buff;

        public Rasterization(Buffer buff)
        {
            this.buff = buff;
        }

        public void Triangle(vector3 v1, vector3 v2, vector3 v3, vector3 c1, vector3 c2, vector3 c3)
        {
            float x1 = (v1.X + 1) * buff.x * .5f;
            float y1 = (v1.Y + 1) * buff.y * .5f;
            float x2 = (v2.X + 1) * buff.x * .5f;
            float y2 = (v2.Y + 1) * buff.y * .5f;
            float x3 = (v3.X + 1) * buff.x * .5f;
            float y3 = (v3.Y + 1) * buff.y * .5f;

            int minx = Min(x1, x2, x3);
            int miny = Min(y1, y2, y3);
            int maxx = Max(x1, x2, x3);
            int maxy = Max(y1, y2, y3);

            minx = Math.Max(minx, 0);
            maxx = Math.Min(maxx, buff.colorBuffer.Width - 1);
            miny = Math.Max(miny, 0);
            maxy = Math.Min(maxy, buff.colorBuffer.Height - 1);

            float dy12 = y1 - y2;
            float dy23 = y2 - y3;
            float dy31 = y3 - y1;
            float dx12 = x1 - x2;
            float dx23 = x2 - x3;
            float dx31 = x3 - x1;

            bool tl1 = false;
            bool tl2 = false;
            bool tl3 = false;

            if (dy12 < 0 || (dy12 == 0 && dx12 > 0)) tl1 = true;
            if (dy23 < 0 || (dy23 == 0 && dx23 > 0)) tl2 = true;
            if (dy31 < 0 || (dy31 == 0 && dx31 > 0)) tl3 = true;

            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                {
                    float hs1 = dx12 * (y - y1) - dy12 * (x - x1);
                    float hs2 = dx23 * (y - y2) - dy23 * (x - x2);
                    float hs3 = dx31 * (y - y3) - dy31 * (x - x3);

                    if (((hs1 > 0 && !tl1) || (hs1 >= 0 && tl1)) &&
                        ((hs2 > 0 && !tl2) || (hs2 >= 0 && tl2)) &&
                        ((hs3 > 0 && !tl3) || (hs3 >= 0 && tl3)))
                    {
                        float l1 = (((y2 - y3) * (x - x3)) + ((x3 - x2) * (y - y3))) /
                                    (((y2 - y3) * (x1 - x3)) + ((x3 - x2) * (y1 - y3)));
                        float l2 = (((y3 - y1) * (x - x3)) + ((x1 - x3) * (y - y3))) /
                                    (((y3 - y1) * (x2 - x3)) + ((x1 - x3) * (y2 - y3)));
                        float l3 = 1 - l1 - l2;

                        float depth = l1 * v1.Z + l2 * v2.Z + l3 * v3.Z;

                        if (depth < buff.depthBuffer[x, y])
                        {
                            vector3 col = c1 * l1 + c2 * l2 + c3 * l3;
                            buff.SetPixel(x, y, Color.FromArgb((int)col.X, (int)col.Y, (int)col.Z));
                            buff.depthBuffer[x, y] = depth;
                        }
                    }
                }
        }

        public void Triangle(vector3 v1, vector3 v2, vector3 v3, vector3 n1, vector3 n2, vector3 n3, ILight light, VertexProcessor vert)
        {
            float x1 = (v1.X + 1) * buff.x * .5f;
            float y1 = (v1.Y + 1) * buff.y * .5f;
            float x2 = (v2.X + 1) * buff.x * .5f;
            float y2 = (v2.Y + 1) * buff.y * .5f;
            float x3 = (v3.X + 1) * buff.x * .5f;
            float y3 = (v3.Y + 1) * buff.y * .5f;

            int minx = Min(x1, x2, x3);
            int miny = Min(y1, y2, y3);
            int maxx = Max(x1, x2, x3);
            int maxy = Max(y1, y2, y3);

            minx = Math.Max(minx, 0);
            maxx = Math.Min(maxx, buff.x - 1);
            miny = Math.Max(miny, 0);
            maxy = Math.Min(maxy, buff.y - 1);

            float dy12 = y1 - y2;
            float dy23 = y2 - y3;
            float dy31 = y3 - y1;
            float dx12 = x1 - x2;
            float dx23 = x2 - x3;
            float dx31 = x3 - x1;

            bool tl1 = false;
            bool tl2 = false;
            bool tl3 = false;

            if (dy12 < 0 || (dy12 == 0 && dx12 > 0)) tl1 = true;
            if (dy23 < 0 || (dy23 == 0 && dx23 > 0)) tl2 = true;
            if (dy31 < 0 || (dy31 == 0 && dx31 > 0)) tl3 = true;

            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                {
                    float hs1 = dx12 * (y - y1) - dy12 * (x - x1);
                    float hs2 = dx23 * (y - y2) - dy23 * (x - x2);
                    float hs3 = dx31 * (y - y3) - dy31 * (x - x3);

                    if (((hs1 > 0 && !tl1) || (hs1 >= 0 && tl1)) &&
                        ((hs2 > 0 && !tl2) || (hs2 >= 0 && tl2)) &&
                        ((hs3 > 0 && !tl3) || (hs3 >= 0 && tl3)))
                    {
                        float l1 = (((y2 - y3) * (x - x3)) + ((x3 - x2) * (y - y3))) /
                                    (((y2 - y3) * (x1 - x3)) + ((x3 - x2) * (y1 - y3)));
                        float l2 = (((y3 - y1) * (x - x3)) + ((x1 - x3) * (y - y3))) /
                                    (((y3 - y1) * (x2 - x3)) + ((x1 - x3) * (y2 - y3)));
                        float l3 = 1 - l1 - l2;

                        float depth = l1 * v1.Z + l2 * v2.Z + l3 * v3.Z;

                        if (depth < buff.depthBuffer[x, y])
                        {
                            vector3 position = v1 * l1 + v2 * l2 + v3 * l3;
                            vector3 normal = n1 * l1 + n2 * l2 + n3 * l3;
                            vector3 col = light.Calculate(vert, position, normal);
                            buff.SetPixel(x, y, Color.FromArgb((int)col.X, (int)col.Y, (int)col.Z));
                            buff.depthBuffer[x, y] = depth;
                        }
                    }
                }
        }

        public void Triangle(vector3 v1, vector3 v2, vector3 v3, vector3 n1, vector3 n2, vector3 n3, ILight light, VertexProcessor vert, vector3 texCoord1,
            vector3 texCoord2, vector3 texCoord3, Bitmap texture)
        {
            float x1 = (v1.X + 1) * buff.x * .5f;
            float y1 = (v1.Y + 1) * buff.y * .5f;
            float x2 = (v2.X + 1) * buff.x * .5f;
            float y2 = (v2.Y + 1) * buff.y * .5f;
            float x3 = (v3.X + 1) * buff.x * .5f;
            float y3 = (v3.Y + 1) * buff.y * .5f;

            int minx = Min(x1, x2, x3);
            int miny = Min(y1, y2, y3);
            int maxx = Max(x1, x2, x3);
            int maxy = Max(y1, y2, y3);

            minx = Math.Max(minx, 0);
            maxx = Math.Min(maxx, buff.x - 1);
            miny = Math.Max(miny, 0);
            maxy = Math.Min(maxy, buff.y - 1);

            float dy12 = y1 - y2;
            float dy23 = y2 - y3;
            float dy31 = y3 - y1;
            float dx12 = x1 - x2;
            float dx23 = x2 - x3;
            float dx31 = x3 - x1;

            bool tl1 = false;
            bool tl2 = false;
            bool tl3 = false;

            if (dy12 < 0 || (dy12 == 0 && dx12 > 0)) tl1 = true;
            if (dy23 < 0 || (dy23 == 0 && dx23 > 0)) tl2 = true;
            if (dy31 < 0 || (dy31 == 0 && dx31 > 0)) tl3 = true;

            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                {
                    float hs1 = dx12 * (y - y1) - dy12 * (x - x1);
                    float hs2 = dx23 * (y - y2) - dy23 * (x - x2);
                    float hs3 = dx31 * (y - y3) - dy31 * (x - x3);

                    if (((hs1 > 0 && !tl1) || (hs1 >= 0 && tl1)) &&
                        ((hs2 > 0 && !tl2) || (hs2 >= 0 && tl2)) &&
                        ((hs3 > 0 && !tl3) || (hs3 >= 0 && tl3)))
                    {
                        float l1 = (((y2 - y3) * (x - x3)) + ((x3 - x2) * (y - y3))) /
                                    (((y2 - y3) * (x1 - x3)) + ((x3 - x2) * (y1 - y3)));
                        float l2 = (((y3 - y1) * (x - x3)) + ((x1 - x3) * (y - y3))) /
                                    (((y3 - y1) * (x2 - x3)) + ((x1 - x3) * (y2 - y3)));
                        float l3 = 1 - l1 - l2;

                        float depth = l1 * v1.Z + l2 * v2.Z + l3 * v3.Z;

                        if (depth < buff.depthBuffer[x, y])
                        {
                            vector3 position = v1 * l1 + v2 * l2 + v3 * l3;
                            vector3 normal = n1 * l1 + n2 * l2 + n3 * l3;
                            vector3 texCoords = texCoord1 * l1 + texCoord2 * l2 + texCoord3 * l3;

                            float texCoordX = Map(texCoords.X, 0f, texture.Width);
                            float texCoordY = Map(texCoords.Y, 0f, texture.Height);

                            Color texelC = (texture.GetPixel((int)texCoordX, (int)texCoordY));
                            vector3 texel = new vector3(texelC.R, texelC.G, texelC.B);
                            vector3 col = texel * (light.Calculate(vert, position, normal) / 255f);
                            buff.SetPixel(x, y, Color.FromArgb((int)col.X, (int)col.Y, (int)col.Z));
                            buff.depthBuffer[x, y] = depth;
                        }
                    }
                }
        }
        public static float Map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        public static float Map(float x, float out_min, float out_max)
        {
            return x * (out_max - out_min) + out_min;
        }
        public int Min(float a, float b, float c) => (int)Math.Min(a, Math.Min(b, c));
        public int Max(float a, float b, float c) => (int)Math.Max(a, Math.Max(b, c));
    }
}