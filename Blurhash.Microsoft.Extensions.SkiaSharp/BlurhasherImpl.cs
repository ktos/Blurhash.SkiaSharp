using Blurhash.Microsoft.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;

namespace Blurhash.Microsoft.Extensions.SkiaSharp
{
    /// <inheritdoc cref="Blurhash.Microsoft.Extensions.SkiaSharp.IBlurhasher" />
    public class BlurhasherImpl : Blurhasher<SKBitmap>, IBlurhasher
    {
        public BlurhasherImpl(IImageConverter<SKBitmap> imageConverter) : base(imageConverter)
        {
        }
    }

    /// <summary>
    /// The blurhash algorithm abstraction for <see cref="SKBitmap"/>
    /// </summary>
    public interface IBlurhasher : IBlurhasher<SKBitmap>
    {
    }

    public static class Extensions
    {
        /// <summary>
        /// Adds the blurhash core and the converter for <see cref="SKBitmap"/> to the given <see cref="IServiceCollection"/><br />
        /// Also enables you to request <see cref="IBlurhasher"/>
        /// </summary>
        public static IServiceCollection AddBlurhash(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddBlurhashCore()
                .AddSingleton<IImageConverter<SKBitmap>, ImageConverter>()
                .AddSingleton<IBlurhasher, BlurhasherImpl>();
        }
    }

    /// <inheritdoc />
    public class ImageConverter : IImageConverter<SKBitmap>
    {
        /// <inheritdoc />
        public Pixel[,] ImageToPixels(SKBitmap image) => Blurhash.SkiaSharp.Blurhasher.ConvertBitmap(image);

        /// <inheritdoc />
        public SKBitmap PixelsToImage(Pixel[,] pixels) => Blurhash.SkiaSharp.Blurhasher.ConvertToBitmap(pixels);
    }
}