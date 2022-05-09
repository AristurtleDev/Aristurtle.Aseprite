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
        ///     Specifies the type of a cel.
        /// </summary>
        public enum CelType : int
        {
            /// <summary>
            ///     Specifies that this cel is a raw image cel.
            /// </summary>
            RawImage = 0,

            /// <summary>
            ///     Specifies that this cel is a linked cel.
            /// </summary>
            Linked = 1,

            /// <summary>
            ///     Specifies that this cel is a compressed image cel.
            /// </summary>
            CompressedImage = 2,

            /// <summary>
            ///     Specifies that this cel is a compressed tilemap cel.
            /// </summary>
            CompressedTilemap = 3
        }
    }
}
