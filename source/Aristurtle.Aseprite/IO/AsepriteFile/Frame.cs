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
        ///     A data-model class that represents a single frame of an Aseprite
        ///     .ase/.aseprite file.
        /// </summary>
        public class Frame
        {
            //  The collection of all cel chunks that belong to this frame.
            private List<Cel> _cels;

            /// <summary>
            ///     Gets a value that indicates the duration, in milliseconds, of
            ///     this frame.
            /// </summary>
            public int Duration { get; internal set; }

            /// <summary>
            ///     Gets a read only collection of all <see cref="Cel"/> class
            ///     instnaces that belong to this frame.
            /// </summary>
            public ReadOnlyCollection<Cel> Cels { get; private set; }

            /// <summary>
            ///     Creates a new <see cref="Frame"/> class instance.
            /// </summary>
            internal Frame()
            {
                _cels = new List<Cel>();
                Cels = _cels.AsReadOnly();
            }

            /// <summary>
            ///     Adds the given <see cref="Cel"/> class instance to the
            ///     internal colelction of cels for this frame.
            /// </summary>
            /// <param name="cel">
            ///     The <see cref="Cel"/> class instance to add.
            /// </param>
            internal void AddCel(Cel cel) => _cels.Add(cel);
        }
    }
}
