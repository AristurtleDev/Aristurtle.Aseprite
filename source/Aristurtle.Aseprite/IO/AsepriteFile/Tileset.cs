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
        ///     A data-model class that represents a tileset chunk in an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public class Tileset
        {
            /// <summary>
            ///     Gets a value that indicates the id of this tileset.
            /// </summary>
            public int ID { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="TilesetFlags"/> bitmask value that indicates
            ///     the bitmask flags set for this tileset.
            /// </summary>
            public TilesetFlags Flags { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this <see cref="Tileset"/>
            ///     class instnace contains the tilemap data in an external file.
            /// </summary>
            public bool IncludesExternalFile => (Flags & TilesetFlags.IncludesLink) != 0;

            /// <summary>
            ///     Gets a value that indicates whether this <see cref="Tileset"/>
            ///     class instnace contans valid <see cref="Pixels"/> data.
            /// </summary>
            public bool IncludesTiles => (Flags & TilesetFlags.IncludesTiles) != 0;

            /// <summary>
            ///     Gets a value that indicats whether tilemaps using this tileset
            ///     use tile ID=0 as the empty tile.
            /// </summary>
            public bool TileIDZero => (Flags & TilesetFlags.TileIDZero) != 0;

            /// <summary>
            ///     Gets a value that indicates the total number of tiles contained
            ///     in this tileset.
            /// </summary>
            public int TileCount { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="System.Drawing.Size"/> value that indicates
            ///     width and height size of a single tile.
            /// </summary>
            public Size Size { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the number to show on screen from
            ///     the tile with index 1 and so on.
            /// </summary>
            public int BaseIndex { get; internal set; }

            /// <summary>
            ///     Gets a string that contains the name of this tileset.
            /// </summary>
            public string Name { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the external file ID that contains
            ///     the tileset data.
            /// </summary>
            /// <remarks>
            ///     This value is only valid when <see cref="IncludesExternalFile"/>
            ///     is <see langword="true"/>
            /// </remarks>
            public int ExternalFileID { get; internal set; }

            /// <summary>
            ///     Ges a value that indicates the ID of the tileset in the external
            ///     file.
            /// </summary>
            /// <remarks>
            ///     This value is only valid when <see cref="IncludesExternalFile"/>
            ///     is <see langword="true"/>
            /// </remarks>
            public int TilesetID { get; internal set; }

            /// <summary>
            ///     Gets an array of <see cref="System.Drawing.Color"/> values that
            ///     represent the image for this tileset.
            /// </summary>
            /// <remarks>
            ///     This is only valid when <see cref="IncludesTiles"/> is
            ///     <see langword="true" />
            /// </remarks>
            public Color[] Pixels { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="Tileset"/> class instnace.
            /// </summary>
            internal Tileset() { }
        }
    }
}

