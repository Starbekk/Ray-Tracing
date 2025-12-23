using System;

namespace Ray_Tracing;

public class Image
{
    static void Main()
    {
        int image_Width = 256;
        int image_Height = 256;
        Console.WriteLine("P3");
        Console.WriteLine($"{image_Width} {image_Height}");
        Console.WriteLine("255");
        for (int j = 0; j < image_Height; j++)
        {
            Console.Error.Write($"\rScanlines remaining: {image_Height-j}  ");
            for (int i = 0; i < image_Width; i++)
            {
                var r = (double)i / (image_Width - 1);
                var g = (double)j / (image_Height - 1);

                var b = 0.0;
              
                Vec3 pixelColor = new Vec3(r, g, b);
                Color.WriteColor(Console.Out,  pixelColor);
            }
        }
        Console.Error.WriteLine();
        Console.Error.Write($"\rDone.       \n");
    }
}

