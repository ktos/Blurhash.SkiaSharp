# Blurhash.SkiaSharp

[![NuGet](https://img.shields.io/nuget/v/Blurhash.SkiaSharp.svg)](https://www.nuget.org/packages/BlurHash.SkiaSharp/)

A [Blurhash](https://github.com/woltapp/blurhash) implementation based on
[blurhash.net](https://github.com/MarkusPalcer/blurhash.net) for SkiaSharp.

Currently allows to decode a blurhash into `SKBitmap`. Encoding of SKBitmap into
blurhash is not supported as of 1.0.

Several portions of the code are directly copy-pasted from the [System.Drawing
implementation](https://github.com/MarkusPalcer/blurhash.net/tree/master/Blurhash-System.Drawing).

Tested on UWP and Android.

## Example usage

### Displaying a placeholder for an image in Xamarin.Forms Image control

```csharp
using Blurhash.Skia;

// ...

var decoder = new Blurhash.SkiaSharp.Decoder();
var im = decoder.Decode("LEHV6nWB2yk8pyo0adR*.7kCMdnj", 219, 176);
var data = SKImage.FromBitmap(im).Encode();

image1.Source = ImageSource.FromStream(data.AsStream);
```