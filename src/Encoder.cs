using Blurhash.Core;
using SkiaSharp;
using System;

namespace Blurhash.SkiaSharp
{
    /// <summary>
    /// The Blurhash encoder for SkiaSharp
    /// Creates a very compact hash from an image to use as a blurred image placeholder
    /// </summary>
    public class Encoder : CoreEncoder
    {
        /// <summary>
        /// Encodes a <c>SKBitmap</c> into a Blurhash string
        /// </summary>
        /// <param name="image">The bitmap to encode</param>
        /// <param name="componentsX">The number of components used on the X-Axis for the DCT</param>
        /// <param name="componentsY">The number of components used on the Y-Axis for the DCT</param>
        /// <returns>The resulting Blurhash string</returns>
        public string Encode(SKBitmap image, int componentsX, int componentsY)
        {
            return CoreEncode(ConvertBitmap(image), componentsX, componentsY);
        }

        /// <summary>
        /// Converts the given bitmap to the library-independent representation used within the Blurhash-core
        /// </summary>
        /// <param name="sourceBitmap">The bitmap to encode</param>
        public unsafe static Pixel[,] ConvertBitmap(SKBitmap sourceBitmap)
        {
            SKPixmap pixmap = sourceBitmap.PeekPixels();
            byte* bmpPtr = (byte*)pixmap.GetPixels().ToPointer();
            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height;

            var result = new Pixel[width, height];

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    byte red, green, blue, alpha;
                    if (sourceBitmap.ColorType == SKColorType.Rgba8888)
                    {
                        // SKColorType.Rgba8888 is used by iOS and Android                                       
                        red = *bmpPtr++;
                        green = *bmpPtr++;
                        blue = *bmpPtr++;
                        alpha = *bmpPtr++;

                        result[col, row].Blue = MathUtils.SRgbToLinear(blue);
                        result[col, row].Green = MathUtils.SRgbToLinear(green);
                        result[col, row].Red = MathUtils.SRgbToLinear(red);
                    }
                    else if (sourceBitmap.ColorType == SKColorType.Bgra8888)
                    {
                        // UWP uses SKColorType.Bgra8888
                        blue = *bmpPtr++;
                        green = *bmpPtr++;
                        red = *bmpPtr++;
                        alpha = *bmpPtr++;

                        result[col, row].Blue = MathUtils.SRgbToLinear(blue);
                        result[col, row].Green = MathUtils.SRgbToLinear(green);
                        result[col, row].Red = MathUtils.SRgbToLinear(red);
                    }
                    else
                    {
                        throw new ArgumentException($"ColorType {sourceBitmap.ColorType} is not supported");
                    }
                }
            }

            return result;
        }
    }
}