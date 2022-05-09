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

namespace Aristurtle.Aseprite
{
    /// <summary>
    ///     Used to specify the direction of an animation loop.
    /// </summary>
    public enum Direction : int
    {
        /// <summary>
        ///     Specifies that the animation should play in a forward direction
        ///     from the first frame to the last frame.
        /// </summary>
        Forward = 0,

        /// <summary>
        ///     Specifies that the animation should play in a reverse direction
        ///     from the last frame to the first frame.
        /// </summary>
        Reverse = 1,

        /// <summary>
        ///     Specifies that the animation should ping pong the direction by
        ///     fist animating in a forward direction from the first frame to
        ///     the last frame, then play in a reverse direction from teh last
        ///     frame to the first frame.
        /// </summary>
        PingPong = 2
    }

}
