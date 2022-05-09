/* -----------------------------------------------------------------------------
    Copyright 2022 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to
    deal in the Software without restriction, including without limitation the
    rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
    sell copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
    IN THE SOFTWARE.
----------------------------------------------------------------------------- */

using System;
using System.Drawing;

namespace Aristurtle.Aseprite.IO
{
    public partial class AsepriteFile
    {
        /// <summary>
        ///     A data-model class that represents a cel chunk in an Aseprite
        ///     .ase/.aseprite file that contains image data.
        /// </summary>
        public class ImageCel : Cel
        {
            /// <summary>
            ///     Gets the array of all pixels that make up the image for
            ///     this cel.
            /// </summary>
            /// <remarks>
            ///     The array of pixels is row by row, from top to bottom, for each
            ///     scanline read pixels from left to right.
            /// </remarks>
            public Color[] Pixels { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="ImageCel"/> class instance.
            /// </summary>
            internal ImageCel() { }

            /// <summary>
            ///     Creates a new <see cref="ImageCel"/> class instnace from an
            ///     existing instance.
            /// </summary>
            /// <param name="existing">
            ///     An existing <see cref="ImageCel"/> class instnace to derive the
            ///     property values of this class instance from.
            /// </param>
            internal ImageCel(ImageCel existing) : base(existing)
            {
                Pixels = new Color[existing.Pixels.Length];
                Array.Copy(existing.Pixels, Pixels, Pixels.Length);
                // Buffer.BlockCopy(existing.Pixels, 0, Pixels, 0, Pixels.Length);
            }
        }
    }
}
