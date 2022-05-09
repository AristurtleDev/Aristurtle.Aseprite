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

using System.Collections.Generic;
using System.Drawing;

namespace Aristurtle.Aseprite
{
    /// <summary>
    ///     A generated spritesheet from the Aseprite file.
    /// </summary>
    public class Spritesheet
    {
        /// <summary>
        ///     Gets a value indicating the total width, in pixels, of the
        ///     spritesheet.
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        ///     Gets a value indicating the total height, in pixels, of the
        ///     spritesheet.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        ///     Gets a <see cref="System.Drawing.Color"/> array of all pixels
        ///     for this spritesheet in scanline order row-by-row from
        ///     top-to-bottom read left-to-right.
        /// </summary>
        public Color[] Pixels { get; internal set; }

        /// <summary>
        ///     Gets a collection of <see cref="SpritesheetFrame"/> class
        ///     instnaces that contain the data for each frame for the
        ///     spritesheet.
        /// </summary>
        public List<SpritesheetFrame> Frames { get; internal set; }

        /// <summary>
        ///     Creates a new <see cref="Spritesheet"/> class instance.
        /// </summary>
        internal Spritesheet() => Frames = new List<SpritesheetFrame>();
    }
}
