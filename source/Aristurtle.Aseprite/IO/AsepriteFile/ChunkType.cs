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

namespace Aristurtle.Aseprite.IO
{
    public partial class AsepriteFile
    {
        /// <summary>
        ///     Specifies the type of a chunk.
        /// </summary>
        internal enum ChunkType : int
        {
            /// <summary>
            ///     Specifies that this is an Old Palette chunk where the RGB values
            ///     are 0-255
            /// </summary>
            OldPaletteA = 0x0004,

            /// <summary>
            ///     Specifies that this is an Old Palette chunk where the RGB values
            ///     are 0-255
            /// </summary>
            OldPaletteB = 0x0011,

            /// <summary>
            ///     Specifies that this is a Layer chunk.
            /// </summary>
            Layer = 0x2004,

            /// <summary>
            ///     Specifies that this is a Cel chunk.
            /// </summary>
            Cel = 0x2005,

            /// <summary>
            ///     Specifies that this is a Cel Extra chunk.
            /// </summary>
            CelExtra = 0x2006,

            /// <summary>
            ///     Specifies that this is a Color Profile chunk.
            /// </summary>
            ColorProfile = 0x2007,

            /// <summary>
            ///     Specifies that this is an External Files chunk.
            /// </summary>
            ExternalFiles = 0x2008,

            /// <summary>
            ///     Specifies that this is a Mask chunk.
            /// </summary>
            Mask = 0x2016,

            /// <summary>
            ///     Specifies that this is a Path chunk.
            /// </summary>
            Path = 0x2017,

            /// <summary>
            ///     Specifies that this is a Tags chunk.
            /// </summary>
            Tags = 0x2018,

            /// <summary>
            ///     Specifies that this is a Palette chunk.
            /// </summary>
            Palette = 0x2019,

            /// <summary>
            ///     Specifies that this is a User Data chunk.
            /// </summary>
            UserData = 0x2020,

            /// <summary>
            ///     Specifies that this is a Slice chunk.
            /// </summary>
            Slice = 0x2022,

            /// <summary>
            ///     Specifies that this is a Tileset chunk.
            /// </summary>
            Tileset = 0x2023
        }
    }
}
