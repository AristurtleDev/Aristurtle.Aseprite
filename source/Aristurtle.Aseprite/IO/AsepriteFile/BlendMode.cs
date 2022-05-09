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
        ///     Specifies the type of blend mode used by a layer in an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public enum BlendMode : int
        {
            /// <summary>
            ///     Specifies that this layer uses normal blending.
            /// </summary>
            Normal = 0,

            /// <summary>
            ///     Specifies that this layer uses a Multiply blend mode.
            /// </summary>
            Multiply = 1,

            /// <summary>
            ///     Specifies that this layer uses a Screen blend mode.
            /// </summary>
            Screen = 2,

            /// <summary>
            ///     Specifies that this layer uses an Overlay blend mode.
            /// </summary>
            Overlay = 3,

            /// <summary>
            ///     Specifies that this layer uses a Darken blend mode.
            /// </summary>
            Darken = 4,

            /// <summary>
            ///     Specifies that this layer uses a Lighten blend mode.
            /// </summary>
            Lighten = 5,

            /// <summary>
            ///     Specifies that this layer uses a Color Dodge blend mode.
            /// </summary>
            ColorDodge = 6,

            /// <summary>
            ///     Specifies that this layer uses a Color Burn blend mode.
            /// </summary>
            ColorBurn = 7,

            /// <summary>
            ///     Specifies that this layer uses a Hard Light blend mode.
            /// </summary>
            HardLight = 8,

            /// <summary>
            ///     Specifies that this layer uses a Soft Light blend mode.
            /// </summary>
            SoftLight = 9,

            /// <summary>
            ///     Specifies that this layer uses a Difference blend mode.
            /// </summary>
            Difference = 10,

            /// <summary>
            ///     Specifies that this layer uses an Exclusion blend mode.
            /// </summary>
            Exclusion = 11,

            /// <summary>
            ///     Specifies that this layer uses a Hue blend mode.
            /// </summary>
            Hue = 12,

            /// <summary>
            ///     Specifies that this layer uses a Saturation blend mode.
            /// </summary>
            Saturation = 13,

            /// <summary>
            ///     Specifies that this layer uses a Color blend mode.
            /// </summary>
            Color = 14,

            /// <summary>
            ///     Specifies that this layer uses a Luminosity blend mode.
            /// </summary>
            Luminosity = 15,

            /// <summary>
            ///     Specifies that this layer uses an Addition blend mode.
            /// </summary>
            Addition = 16,

            /// <summary>
            ///     Specifies that this layer uses a Subtract blend mode.
            /// </summary>
            Subtract = 17,

            /// <summary>
            ///     Specifies that this layer uses a Divide blend mode.
            /// </summary>
            Divide = 18,
        }
    }
}
