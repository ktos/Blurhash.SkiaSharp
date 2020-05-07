# Blurhash.SkiaSharp

[![NuGet](https://img.shields.io/nuget/v/Blurhash.SkiaSharp.svg)](https://www.nuget.org/packages/BlurHash.SkiaSharp/)

A [Blurhash](https://github.com/woltapp/blurhash) implementation based on
[blurhash.net](https://github.com/MarkusPalcer/blurhash.net) for SkiaSharp, thus
fully available for Xamarin and other .NET Standard 2.0 platforms.

Currently allows to decode a blurhash into `SKBitmap`. Creation of blurhash from
images is not supported as of 1.0.

Several portions of the code are directly copy-pasted from the [System.Drawing
implementation](https://github.com/MarkusPalcer/blurhash.net/tree/master/Blurhash-System.Drawing).

Tested on Xamarin.Forms on UWP, iOS and Android.

## Example usage

### Displaying a placeholder for an image in Xamarin.Forms Image control

```csharp
// ...

var decoder = new Blurhash.SkiaSharp.Decoder();
var im = decoder.Decode("LEHV6nWB2yk8pyo0adR*.7kCMdnj", 219, 176);
var data = SKImage.FromBitmap(im).Encode();

image1.Source = ImageSource.FromStream(data.AsStream);
```

![Example of Xamarin.Forms image with Blurhash switching to loaded image](https://raw.githubusercontent.com/ktos/Blurhash.SkiaSharp/master/ex1.gif)
