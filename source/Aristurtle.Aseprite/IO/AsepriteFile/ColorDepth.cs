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
        ///     Used to specificy the color depth used to pack pixels. The value of the
        ///     color depth represents the total number of bits per pixel.
        /// </summary>
        public enum ColorDepth : int
        {
            /// <summary>
            ///     Specifies that the color depth is of Indexed type meaning that each
            ///     pixel is 8 bits.
            /// </summary>
            Index = 8,

            /// <summary>
            ///     Specifies that the color depth is of Grayscale type meaning that
            ///     each pixel is 16 bits.
            /// </summary>
            Grayscale = 16,

            /// <summary>
            ///     Specifies that the color depth is of RGBA type meaning that each
            ///     pixel is 32 bits.
            /// </summary>
            RGBA = 32
        }
    }
}
