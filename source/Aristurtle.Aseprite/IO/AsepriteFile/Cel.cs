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
        ///     A data-model class representing a Cel chunk in an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public abstract class Cel : ChunkWithUserData
        {
            /// <summary>
            ///     Gets a value that indicates the index of the layer this cel is
            ///     part of.
            /// </summary>
            public int LayerIndex { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the x-coordinate position of this
            ///     cel.
            /// </summary>
            public int X { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the y-coordinate position of this
            ///     cel.
            /// </summary>
            public int Y { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the opacity level of this cel.
            /// </summary>
            public int Opacity { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="CelType"/> value that indicates what type of
            ///     cel this is.
            /// </summary>
            public CelType Type { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the width, in pixels, of this cel.
            /// </summary>
            public int Width { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the height, in pixels, of this cel.
            /// </summary>
            public int Height { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="Cel"/> class instance.
            /// </summary>
            internal Cel() { }

            /// <summary>
            ///     Creates a new <see cref="Cel"/> class instnace from an existing
            ///     instance.
            /// </summary>
            /// <param name="existing">
            ///     An existing <see cref="Cel"/> class instnace to derive the
            ///     property values of this class instance from.
            /// </param>
            internal Cel(Cel existing)
            {
                LayerIndex = existing.LayerIndex;
                X = existing.X;
                Y = existing.Y;
                Opacity = existing.Opacity;
                Type = existing.Type;
                Width = existing.Width;
                Height = existing.Height;
                UserData = UserData;
            }
        }
    }
}
