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

namespace Aristurtle.Aseprite.IO
{
    public partial class AsepriteFile
    {
        /// <summary>
        ///     A data-model class that represents a tilemap cel chunk in an
        ///     Aseprite .ase/.aseprite file.
        /// </summary>
        public class TilemapCel : Cel
        {

            /// <summary>
            ///     Gets a value that indicates the total number of bits used per
            ///     tile.
            /// </summary>
            public int BitsPerTile { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the bitmask used for teh tile ID.
            /// </summary>
            public int TileIDBitmask { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the bitmask used for x flip.
            /// </summary>
            public int XFlipBitmask { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the bitmask used for y flip.
            /// </summary>
            public int YFlipBitmask { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the bitmask used for 90CW rotation.
            /// </summary>
            public int RotationBitmask { get; internal set; }

            /// <summary>
            ///     Gets the array of tile data for this tilemap cel.
            /// </summary>
            public byte[] Tiles { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="TilemapCel"/> class instance.
            /// </summary>
            internal TilemapCel() { }

            /// <summary>
            ///     Creates a new <see cref="TilemapCel"/> class instnace from an
            ///     existing instance.
            /// </summary>
            /// <param name="existing">
            ///     An existing <see cref="TilemapCel"/> class instnace to derive the
            ///     property values of this class instance from.
            /// </param>
            internal TilemapCel(TilemapCel existing) : base(existing)
            {
                BitsPerTile = existing.BitsPerTile;
                TileIDBitmask = existing.TileIDBitmask;
                XFlipBitmask = existing.XFlipBitmask;
                YFlipBitmask = existing.YFlipBitmask;
                RotationBitmask = existing.RotationBitmask;

                Tiles = new byte[existing.Tiles.Length];
                Buffer.BlockCopy(existing.Tiles, 0, Tiles, 0, Tiles.Length);

            }
        }
    }
}
