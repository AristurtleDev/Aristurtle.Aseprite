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
        ///     A data-model class that represents a Layer Chunk in an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public class Layer : ChunkWithUserData
        {
            /// <summary>
            ///     Gets a value that indicates whether this layer is visible.
            /// </summary>
            public bool IsVisible { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this layer is
            ///     editable.
            /// </summary>
            /// <remarks>
            ///     This is a value that is used by the Aseprite software. This
            ///     does not indicate that the property data of the Layer within
            ///     Aristurlt.Aseprite is editable.
            /// </remarks>
            public bool IsEditable { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether the movement of this
            ///     layer has been locked.
            /// </summary>
            public bool LockMovement { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this layer is a
            ///     Background Layer.
            /// </summary>
            public bool IsBackground { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this layer prefers to
            ///     have linked cels.
            /// </summary>
            public bool PrefersLinkedCels { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this layer should
            ///     be displaed as collapsed.
            /// </summary>
            public bool DisplayedCollapsed { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this layer is a
            ///     Reference Layer.
            /// </summary>
            public bool IsReferenceLayer { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="LayerType"/> value that indicates the type
            ///     of layer this is.
            /// </summary>
            public LayerType Type { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates this layer's child level.
            /// </summary>
            /// <remarks>
            ///     The child level is used to show the relationship of this layar
            ///     with the previous layer
            /// </remarks>
            public int ChildLevel { get; internal set; }

            /// <summary>
            ///     Gets a <see cref="AsepriteDecoder.IO.Types.BlendMode"/> value
            ///     that specifies the type of blend mode used by this layer.
            /// </summary>
            public BlendMode BlendMode { get; internal set; }

            /// <summary>
            ///     Gets a value that represents the opacity of this layer.
            /// </summary>
            /// <remarks>
            ///     This value is only valid if
            ///     <see cref="AsepriteFile.LayerOpacityValid"/> is set to
            ///     <see langword="true"/>
            /// </remarks>
            public int Opacity { get; internal set; }

            /// <summary>
            ///     Gets a string that represents the name of this layer.
            /// </summary>
            public string Name { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates the index of the tileset used for
            ///     this layer.
            /// </summary>
            /// <remarks>
            ///     This value is only valid when <see cref="Type"/> is
            ///     <see cref="LayerType.Tilemap"/>
            /// </remarks>
            public int TilesetIndex { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="Layer"/> class instance
            /// </summary>
            internal Layer() { }
        }
    }
}
