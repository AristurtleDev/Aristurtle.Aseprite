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

namespace Aristurtle.Aseprite
{
    /// <summary>
    ///     A data-model class that represents a single frame within a
    ///     spritesheet.
    /// </summary>
    public class SpritesheetFrame
    {
        /// <summary>
        ///     Gets a value indicating the ID of the frame.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     Gets a value that indicates the top-left x-coordinate position
        ///     of this frame within the spritesheet.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Gets a value that indicates the top-left y-coordinate position
        ///     of this frame within teh spritesheet.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///     Gets a value that indicates the width, in pixels, of this frame
        ///     within the spritesheet.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Gets a value that indicates the height, in pixels, of this frame
        ///     within the spritesheet.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Gets a <see cref="System.Drawing.Rectangle"/> value that
        ///     indicates the bounds of this frame within the spritesheet.
        /// </summary>
        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);

        /// <summary>
        ///     Gets a value that indicates the duration of this frame, in
        ///     milliseconds, when used in an animation.
        /// </summary>
        public int Duration { get; set; }
    }
}
