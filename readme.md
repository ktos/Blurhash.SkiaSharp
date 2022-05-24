# Blurhash.SkiaSharp

[![NuGet](https://img.shields.io/nuget/v/Blurhash.SkiaSharp.svg)](https://www.nuget.org/packages/BlurHash.SkiaSharp/)

![Build Status](https://github.com/ktos/Blurhash.SkiaSharp/actions/workflows/tag.yml/badge.svg)

A [Blurhash](https://github.com/woltapp/blurhash) implementation based on
[blurhash.net](https://github.com/MarkusPalcer/blurhash.net) for SkiaSharp, thus
fully available for Xamarin and other .NET Standard 2.0 platforms.

Currently allows to decode a blurhash into `SKBitmap` or encode a `SKBitmap`
into Blurhash string.

Several portions of the code are almost directly copy-pasted from the
[System.Drawing
implementation](https://github.com/MarkusPalcer/blurhash.net/tree/master/Blurhash-System.Drawing).

Tested on Xamarin.Forms on UWP, iOS and Android.

## Example usage

### Displaying a placeholder for an image in Xamarin.Forms Image control

```csharp
// decode blurhash instring into a SKBitmap
var im = Blurhash.SkiaSharp.Blurhasher.Decode("LEHV6nWB2yk8pyo0adR*.7kCMdnj", 219, 176);

// load it into a SKImage
var data = SKImage.FromBitmap(im).Encode();

// use as a source for Image control
image1.Source = ImageSource.FromStream(data.AsStream);
```

### Encoding an resource image into a Blurhash string

```csharp
// get the image from resources
var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestMobileApp.noah.jpg");

// load to SKImage and to Image control
var img = SKImage.FromEncodedData(stream);
image1.Source = ImageSource.FromFile("noah.jpg");

// create SKBitmap from Image and create a blurhash for it
var hash = Blurhash.SkiaSharp.Blurhasher.Encode(SKBitmap.FromImage(img), 4, 3);

// and show it in a Editor control
editor1.Text = hash;
```

![Example of Xamarin.Forms image with Blurhash switching to loaded image](https://raw.githubusercontent.com/ktos/Blurhash.SkiaSharp/master/ex1.gif)
