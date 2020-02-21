using Blurhash.Core;
using SkiaSharp;
using System;
using System.Linq;

namespace Blurhash.Skia
{
    /// <summary>
    /// The Blurhash decoder for SkiaSharp
    /// Creates a bitmap placeholder from a Blurhash
    /// </summary>
    public class Decoder : CoreDecoder
    {
        /// <summary>
        /// Decodes a Blurhash string into a <c>SKBitmap</c>
        /// </summary>
        /// <param name="blurhash">The blurhash string to decode</param>
        /// <param name="outputWidth">The desired width of the output in pixels</param>
        /// <param name="outputHeight">The desired height of the output in pixels</param>
        /// <param name="punch">A value that affects the contrast of the decoded image. 1 means normal, smaller values will make the effect more subtle, and larger values will make it stronger.</param>
        /// <returns>The decoded preview</returns>
        public SKBitmap Decode(string blurhash, int outputWidth, int outputHeight, double punch = 1.0)
        {
            var pixelData = CoreDecode(blurhash, outputWidth, outputHeight, punch);
            return ToSKBitmap(pixelData);
        }

        /// <summary>
        /// Converts the library-independent representation of pixels into a bitmap
        /// </summary>
        /// <param name="pixelData">The library-independent representation of the image</param>
        /// <returns>A <c>SKBitmap</c> in Bgra8888 representation</returns>
        private static SKBitmap ToSKBitmap(Blurhash.Core.Pixel[,] pixelData)
        {
            var width = pixelData.GetLength(0);
            var height = pixelData.GetLength(1);

            var data = Enumerable.Range(0, height)
                .SelectMany(y => Enumerable.Range(0, width).Select(x => (x, y)))
                .Select(tuple => pixelData[tuple.x, tuple.y])
                .SelectMany(pixel => new byte[]
                {
                    (byte) MathUtils.LinearTosRgb(pixel.Blue), (byte) MathUtils.LinearTosRgb(pixel.Green),
                    (byte) MathUtils.LinearTosRgb(pixel.Red), 255
                })
                .ToArray();

            SKBitmap bitmap = new SKBitmap(width, height, SKColorType.Bgra8888, SKAlphaType.Opaque);

            unsafe
            {
                fixed (byte* ptr = data)
                {
                    bitmap.SetPixels((IntPtr)ptr);
                }
            }

            return bitmap;
        }
    }
}