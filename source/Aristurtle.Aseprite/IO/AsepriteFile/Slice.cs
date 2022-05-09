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
using System.Collections.ObjectModel;

namespace Aristurtle.Aseprite.IO
{
    public partial class AsepriteFile
    {
        /// <summary>
        ///     A data-model class that represetns the data for a slice in an
        ///     Aseprite .ase/.aseprite file.
        /// </summary>
        public class Slice : ChunkWithUserData
        {
            //  Internal collection of keys for this slice.
            private List<SliceKey> _keys;

            /// <summary>
            ///     Gets a <see cref="SliceFlags" /> bitmask value that contains
            ///     the bitmask flags set for this slice.
            /// </summary>
            public SliceFlags Flags { get; internal set; }

            /// <summary>
            ///     Gets a value that indicates whether this slice is a nine patch
            ///     and the keys contain valid center rect value.
            /// </summary>
            public bool IsNinePatch => (Flags & SliceFlags.IsNinePatch) != 0;

            /// <summary>
            ///     Gets a value that indicates whether the keys of this slice
            ///     contain valid pivot data.
            /// </summary>
            public bool HasPivot => (Flags & SliceFlags.HasPivot) != 0;

            /// <summary>
            ///     Gets a string value that contains the name of this slice.
            /// </summary>
            public string Name { get; internal set; }

            /// <summary>
            ///     Gets a read only collection of <see cref="SliceKey"/> class instances
            ///     that beling to this slice.
            /// </summary>
            public ReadOnlyCollection<SliceKey> Keys { get; internal set; }

            /// <summary>
            ///     Creates a new <see cref="Slice"/> class instance.
            /// </summary>
            internal Slice()
            {
                _keys = new List<SliceKey>();
                Keys = _keys.AsReadOnly();
            }

            /// <summary>
            ///     Adds the given <see cref="SliceKey"/> class instance to the internal
            ///     collection of keys for this slice.
            /// </summary>
            /// <param name="key">
            ///     The <see cref="SliceKey"/> class instance to add.
            /// </param>
            internal void AddKey(SliceKey key) => _keys.Add(key);
        }
    }
}
