# Blurhash.Skia

[![NuGet](https://img.shields.io/nuget/v/Blurhash.Skia.svg)](https://www.nuget.org/packages/BlurHash.Skia/)

A [Blurhash](https://github.com/woltapp/blurhash) implementation based on
[blurhash.net](https://github.com/MarkusPalcer/blurhash.net) for SkiaSharp.

Allows to encode a `SoftwareBitmap` into a blurhash and decode a blurhash into
`SoftwareBitmap` again. Several portions of the code are directly copy-pasted
from the [System.Drawing implementation](https://github.com/MarkusPalcer/blurhash.net/tree/master/Blurhash-System.Drawing).

## Example usage

### Displaying a placeholder for an image in Xamarin Forms Image control

```csharp
using Blurhash.Skia;

// ...

var decoder = new Blurhash.Skia.Decoder();
var im = decoder.Decode("LEHV6nWB2yk8pyo0adR*.7kCMdnj", 219, 176);
var data = SKImage.FromBitmap(im).Encode();

image1.Source = ImageSource.FromStream(data.AsStream);
```

### Calculating the blurhash for an image loaded from file

```csharp
var fileOpenPicker = new FileOpenPicker();
fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
fileOpenPicker.FileTypeFilter.Add(".jpg");
fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

var inputFile = await fileOpenPicker.PickSingleFileAsync();

if (inputFile == null)
{
    return;
}
else
{
    SoftwareBitmap softwareBitmap;

    using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
    {
        var decoder = await BitmapDecoder.CreateAsync(stream);

        softwareBitmap = await decoder.GetSoftwareBitmapAsync();

        var encoder = new Encoder();
        var blurhash = encoder.Encode(softwareBitmap, 4, 3);
    }
}
```
