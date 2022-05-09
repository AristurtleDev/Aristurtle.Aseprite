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

using System.Drawing;

namespace Aristurtle.Aseprite.IO
{
    public partial class AsepriteFile
    {
        /// <summary>
        ///     A data-model class that represents a key for a slice in an
        ///     Aseprite .ase/.aseprite file.
        /// </summary>
        public class SliceKey
        {
            /// <summary>
            ///     Gets a value that indicates the index of the frame that this key
            ///     is valid starting on for this slice.
            /// </summary>
            public int Frame { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="Rectangle"/> value that indicates the bounds
            ///     of the slice during this key.
            /// </summary>
            public Rectangle Bounds { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="Rectangle"/> value that indicates the bounds
            ///     of the center rectnagle of the slice during this key.
            /// </summary>
            /// <remarks>
            ///     This value is only valid when the <see cref="Slice.IsNinPatch"/>
            ///     value is <see langword="true" />
            /// </remarks>
            public Rectangle CenterBounds { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="Point"/> value that indicates the
            ///     xy-coordinate position of the pivot point of the slice during
            ///     this key.
            /// </summary>
            /// <remarks>
            ///     This value is only valid when the <see cref="Slice.HasPivot"/>
            ///     value is <see langword="true" />
            /// </remarks>
            public Point Pivot { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="SliceKey"/> class instance.
            /// </summary>
            internal SliceKey() { }
        }
    }
}
