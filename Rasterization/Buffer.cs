using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Rasterization
{
    //Создаю битмап, передавая ссылку на массив интов. У меня массив, который привязан к битмапу, и я напрямую работаю с паматью битмапа.
    public class Buffer
    {
        public Bitmap colorBuffer;
        public int[] intBuffer;
        public GCHandle intBufferHandle;
        public float[,] depthBuffer;

        public int x = 500;
        public int y = 500;

        public Buffer()
        {
            intBuffer = new int[x * y];
            intBufferHandle = GCHandle.Alloc(intBuffer, GCHandleType.Pinned);
            colorBuffer = new Bitmap(x, y, x * 4, PixelFormat.Format32bppPArgb, intBufferHandle.AddrOfPinnedObject());
            FillColor(Color.White);
            depthBuffer = new float[500, 500];
        }

        public Buffer(int size_X, int size_Y, Color col)
        {
            x = size_X;
            y = size_Y;
            intBuffer = new int[x * y];
            intBufferHandle = GCHandle.Alloc(intBuffer, GCHandleType.Pinned);
            colorBuffer = new Bitmap(x, y, x * 4, PixelFormat.Format32bppPArgb, intBufferHandle.AddrOfPinnedObject());
            FillColor(col);
            depthBuffer = new float[x, y];
            FillDepth();
        }

        public void SetPixel(int width, int height, Color col)
        {
            int index = width + (height * x);
            int c = col.ToArgb();
            intBuffer[index] = c;
        }

        private void FillColor(Color col)
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    SetPixel(i, j, col);
                }
        }

        public void ClearColor()
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    SetPixel(i, j, Color.White);
                }
        }

        public void FillDepth()
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    depthBuffer[i, j] = 100;
                }
        }

        public void ClearDepth()
        {
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    depthBuffer[i, j] = 0.0f;
                }
        }

        public void SaveImage()
        {
            colorBuffer.Save("Render.png", ImageFormat.Png);
        }
    }
}
