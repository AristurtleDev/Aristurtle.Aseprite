<h1 align="center">
<img src="https://raw.githubusercontent.com/AristurtleDev/Branding/main/imgs/aristurtle-logo/aristurtle-logo-256-dark.png#gh-light-mode-only" alt="Aristurtle" width="256">
<img src="https://raw.githubusercontent.com/AristurtleDev/Branding/main/imgs/aristurtle-logo/aristurtle-logo-256-light.png#gh-dark-mode-only" alt="Aristurtle" width="256">
<br/>
Aristurtle.Aseprite
</h1>

**Aristurtle.Aseprite** is a cross-platform C# class library used for reading an [Aseprite](https://www.aseprite.org/) .ase/.aseprite file. Reading the file produces an `AsepriteFile` class instance which contains all the data from the file including the header, tags, slices, layers, and cel information.

The `AsepriteFile` class also features two utility methods for extracting the final image data either frame-by-frame or as a full spritesheet.

Aristurtle.Asprite is built against **.NET Standard 2.0** to be as compatible as possible.

## Usage

Add the `using` statements

```cs
using Aristurtle.Aseprite;
using Aristurtle.Aseprite.IO;
```

Use the `AsepriteBinaryReader` to read the Aseprite file

```cs
AsepriteFile asepriteFile;
using(AsepriteBinaryReader reader = new AsepriteBinaryReader("path/to/the/.aseprite/file"))
{
    asepriteFile = reader.Read();
}

//  asepriteFile now contians the contents of the file read.
```
### Properties
An `AsepriteFile` class instance contains the following properties used to access the information from the Aseprite file that was read.

| Property | Description |
| -------- | ----------- |
| `AsepriteFile.Version` | The detected verson of Aseprite used to generate the original file |
| `AsepriteFile.Header` | The header block from the file. This contains information such as the width and height of each frame. |
| `AsepriteFile.Frames` | A read-only collection containing the data for each frame.  Frame data includes information such as the frame's duration and the frames's cels (pixels). |
| `AsepriteFile.Layers` | A read-only collection containing the data for each layer. |
| `AsepriteFile.ExternalFiles` | A read-only dictionary containing all external file entries.  Each dictionary key is of type `int` and is the ID of the external file and  each dictionary value is a `string` containing the path to the external file. |
| `AsepriteFile.Slices` | A read-only collection of all slice data. |
| `AsepriteFile.Palette` | A read-only collection of `System.Drawing.Color` values that make up the palette. |
| `AsepriteFile.Tileset` | A read-only dictionary containing all tileset entries.  Each dictionary key is the ID of the tileset and each dictionary value is a `Tileset` instance containing the information about that tileset. |


### Getting Frame Pixels
The way information is presented in the `AsepriteFile` class is structured to be similar to how it is presented in the official [Aseprite File Spec](https://github.com/aseprite/aseprite/blob/main/docs/ase-file-specs.md) to keep it as generic as possible.  This means that data, such as all pixels that make up a single frame, are most likely not in the strucure you expect.

A single frame is made up of one cel (pixels) per layer.  A frame will need to have all cels combined to produce the final frame result.  You can write your own implementation to do this, or you can use the built-in `AsepriteFile.CreateFrame(int frameIndex, bool onlyVisible)` method, where `int frameIndex` is the zero-based index of the frame to create and `bool onlyVisible` specifies whether only cels within layers marked as visible in the Aseprite file should be included in the frame.

Example
```cs
AsepriteFile asepriteFile;
using(AsepriteBinaryReader reader = new AsepriteBinaryReader("path/to/the/.aseprite/file"))
{
    asepriteFile = reader.Read();
}

//  Create the color data for first frame (frame 0) using only layers that are marked as visible.
System.Drawing.Color[] frame = asepriteFile.CreateFrame(0, true);
```

The output from doing this is a `System.Drawing.Color[]` array where each index is the color of the pixel for that frame row-by-row from top-to-bottom, for each scanline read pixels from left-to-right.

### Generating Spritesheet
The `AsepriteFile` class also contains a utility method called `AsepriteFile.CreateSpritesheet(SpritesheetOptions options)`.  Using this method will produce a more friendly to ingest class instance of `Spritesheet` that contains all frames packed into a single spritesheet represented as a `System.Drawing.Color[]` array, and various properties for each frame used for animation such the bounds of the frame in the spritesheet as well as the frames duration.

The `SpritesheetOption` parameter is used to specify options to use when generating the spritesheet such as padding in and around each frame, frame spacing, if duplicate frames should be merged, and if only visible layers should be used.

Example
```cs
AsepriteFile asepriteFile;
using(AsepriteBinaryReader reader = new AsepriteBinaryReader("path/to/the/.aseprite/file"))
{
    asepriteFile = reader.Read();
}

//  Generate the spritesheet using the default options
Spritesheet spriteSheet = file.CreateSpritesheet(SpritesheetOptions.Default);
```

## Contributing
You can contribute to this project by submitting any bugs or feature requests on the [Issues] page.  You are also welcome to create pull requests to fix any issues or provide additional functionality.

Please refere to the [CONTRIBUTING.md](./CONTRIBUTING.md) documentation for more information.

## License
The source code in this repository, except where otherwise noted in the [Third Party License](#third-party-license) section, is licensed under the MIT license.  For full license text, please refer to the [LICENSE](./LICENSE) file.

## Third Party License
Portions of the source code uses code derived from the Aseprite source code as well as the Pixman library source code.  Refere to the [THIRD-PARTY-NOTICES.md](./THIRD-PARTY-NOTICES.md) file for more information.
