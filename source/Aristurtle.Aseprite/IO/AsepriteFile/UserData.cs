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
        ///     A data-model class that represents the data from a user data chunk
        ///     in an Aseprite .ase/.aseprite file.
        /// </summary>
        public class UserData
        {
            /// <summary>
            ///     Gets a <see cref="AsepriteUserDataFlags"/> value that specifies the
            ///     bitmask flags set for this user data.
            /// </summary>
            public AsepriteUserDataFlags Flags { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this user data has a valid
            ///     <see cref="Text"/> value.
            /// </summary>
            public bool HasText => (Flags & AsepriteUserDataFlags.HasText) != 0;

            /// <summary>
            ///     Gets a value that indicates whether this user data has a valid
            ///     <see cref="UserData.Color"/> value.
            /// </summary>
            public bool HasColor => (Flags & AsepriteUserDataFlags.HasColor) != 0;

            /// <summary>
            ///     Gets a string containing the text value of this user data.
            /// </summary>
            /// <remarks>
            ///     This value is only valid if the <see cref="Flags"/> bitmask
            ///     <see cref="AsepriteUserDataFlags.HasText"/> is set.
            /// </remarks>
            public string Text { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="System.Drawing.Color"/> value that specifies the color set
            ///     for this user data.
            /// </summary>
            /// <remarks>
            ///     This value is only valid if the <see cref="Flags"/> bitmask
            ///     <see cref="AsepriteUserDataFlags.HasColor"/> is set.
            /// </remarks>
            public Color Color { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="UserData"/> class instance.
            /// </summary>
            internal UserData() { }
        }
    }
}
