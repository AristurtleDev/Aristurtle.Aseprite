// /* -----------------------------------------------------------------------------
//     Copyright 2022 Christopher Whitley

//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to
//     deal in the Software without restriction, including without limitation the
//     rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
//     sell copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:

//     The above copyright notice and this permission notice shall be included in
//     all copies or substantial portions of the Software.

//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//     IN THE SOFTWARE.
// ----------------------------------------------------------------------------- */

// using System;

// namespace Aristurtle.Aseprite.IO
// {
//     public partial class AsepriteFile
//     {
//         public class Header
//         {
//             /// <summary>
//             ///     Gets a value that indicates the total number of frames.
//             /// </summary>
//             public int FrameCount { get; internal set; }

//             /// <summary>
//             ///     Gets the a value that indicates the total width, in pixels,
//             ///     of the canvas.
//             /// </summary>
//             public int Width { get; internal set; }

//             /// <summary>
//             ///     Gets a value that indicates the total height, in pixels, of
//             ///     the canvas.
//             /// </summary>
//             public int Height { get; internal set; }

//             /// <summary>
//             ///     Gets a value that indicates the color depth (bits per pixel)
//             ///     used.
//             /// </summary>
//             public ColorDepth ColorDepth { get; internal set; }

//             /// <summary>
//             ///     Gets a value that indicates the flags set for the header.
//             /// </summary>
//             public HeaderFlags Flags { get; internal set; }

//             /// <summary>
//             ///     Gets a value that indicates whether the opacity value of a
//             ///     <see cref="Layer"/> class instance is a valid value to use.
//             /// </summary>
//             public bool LayerOpacityValid => (Flags & HeaderFlags.LayerOpacityValid) != 0;

//             /// <summary>
//             ///     Gets a value that indicates the index of the palette entry that
//             ///     represents a transparent pixel.
//             /// </summary>
//             /// <remarks>
//             ///     This value is only valid when the <see cref="ColorDepth"/> is
//             ///     set to <see cref="ColorDepth.Index" />
//             /// </remarks>
//             public int TransparentIndex { get; internal set; }

//             /// <summary>
//             ///     Gets a value that indicates the total number of colors.
//             /// </summary>
//             public int ColorCount { get; internal set; }

//             /// <summary>
//             ///     Creates a new <see cref="Header"/> class instance.
//             /// </summary>
//             internal Header() { }
//         }
//     }
// }
