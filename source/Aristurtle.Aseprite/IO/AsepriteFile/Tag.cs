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
        ///     A data-model class that represents a Tag chunk from an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public class Tag : ChunkWithUserData
        {
            /// <summary>
            ///     Gets a value that indicates the starting frame of animation in
            ///     the ga.
            /// </summary>
            public int From { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the last frame of animation in the
            ///     tag.
            /// </summary>
            public int To { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="Direction"/> value that indicates the
            ///     loop direction of the animation for this tag.
            /// </summary>
            public Direction Direction { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="Color"/> value that defines the
            ///     color of the tag.
            /// </summary>
            /// <remarks>
            ///     This value shoudl only be used for Aseprite 1.2.x files. For
            ///     Aseprite 1.3+ files, the color should derive from the user data
            ///     instead.
            /// </remarks>
            public Color Color { get; internal set; }

            /// <summary>
            ///     Gets a string containing the name of the tag.
            /// </summary>
            public string Name { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="Tag"/> class instance.
            /// </summary>
            internal Tag() { }
        }
    }
}
