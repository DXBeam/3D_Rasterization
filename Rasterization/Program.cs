using System;
using System.Drawing;

namespace Rasterization
{
    class Program
    {
        static void Main(string[] args)
        {
            Buffer buffer = new Buffer(500, 500, Color.Black);
            Rasterization raster = new Rasterization(buffer);
            VertexProcessor vertex = new VertexProcessor(); // Sphere
            VertexProcessor vertex2 = new VertexProcessor(); // Cylinder
            VertexProcessor vertex3 = new VertexProcessor(); // Cone
            Sphere sphere = new Sphere();
            Cylinder cylinder = new Cylinder();
            Cone cone = new Cone();

            DirectionalLight light = new DirectionalLight(new vector3(0,0,1), new vector3(100,100,100), new vector3(200,0,0), new vector3(0,200,00), 10f); 
            PointLight light2 = new PointLight(new vector3(-1, 0, 0), new vector3(100, 100, 100), new vector3(200, 0, 0), new vector3(0, 200, 00), 10f);
            ReflectLight light3 = new ReflectLight(new vector3(0, 0, 0), new vector3(0,0,1), 40f, new vector3(100, 100, 100), new vector3(200, 0, 0), new vector3(0, 200, 00), 10f);
            DirectionalLight lightOff = new DirectionalLight(new vector3(0,0,1), new vector3(255,255,255), new vector3(0, 0, 0), new vector3(0, 0, 0), 10f);

            Bitmap texture1 = new Bitmap("brick.jpg");
            Bitmap texture2 = new Bitmap("checker.jpg");

            vertex.MultiplyByTranslation(new vector3(0.5f, 0f, 0f)); // Move
            vertex.MultiplyByRotation(0f, new vector3(0f, 0f, 0f)); //Rotation
            vertex.MultiplyByScale(new vector3(0.25f, 0.25f, 0.25f)); //Scale
            vertex.GetMatrix(); //Building

            vertex2.MultiplyByTranslation(new vector3(0f, 0f, 0f)); // Move
            vertex2.MultiplyByRotation(140f, new vector3(140f, 0f, 0f)); //Rotation
            vertex2.MultiplyByScale(new vector3(0.3f, 0.3f, 0.3f)); //Scale
            vertex2.GetMatrix(); //Building

            vertex3.MultiplyByTranslation(new vector3(-0.5f, 0f, 0f)); // Move
            vertex3.MultiplyByRotation(140f, new vector3(-140f, 0f, 0f)); //Rotation
            vertex3.MultiplyByScale(new vector3(0.3f, 0.3f, 0.3f)); //Scale
            vertex3.GetMatrix(); //Building


            sphere.DrawPixelLight(raster, vertex, lightOff, texture1); //Sphere Rasterization
            cylinder.DrawPixelLight(raster, vertex2, light, texture1); //Cylinder Rasterization
            cone.DrawPixelLight(raster, vertex3, light, texture2); //Cone Rasterization

            /*
            raster.Triangle(
                vertex.tr(new vector3(-0.5f, -0.5f, 0f)),
                vertex.tr(new vector3(0f, 0.5f, 0f)),
                vertex.tr(new vector3(0.5f, -0.5f, 0f)),
                new vector3(255, 0, 0), new vector3(0, 255, 0), new vector3(0, 0, 255));
            raster.Triangle(
                vertex.tr(new vector3(-1.1f, 0.5f, 0.9f)),
                vertex.tr(new vector3(0.6f, 0f, -0.1f)),
                vertex.tr(new vector3(-0.9f, -1.1f, 0.5f)),
                new vector3(255, 0, 0), new vector3(0, 255, 0), new vector3(0, 0, 255));

            raster.Triangle(
                vertex.tr(new vector3(0f, 0.5f, 0f)),
                vertex.tr(new vector3(1.3f, 0.5f, 0f)),
                vertex.tr(new vector3(0.5f, -0.5f, 0f)),
                new vector3(255, 0, 0), new vector3(0, 255, 0), new vector3(0, 0, 255));
            */
            buffer.SaveImage();
        }
    }
}