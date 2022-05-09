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

using System.IO;
using System.IO.Compression;

namespace Aristurtle.Aseprite.IO.Compression
{
    /// <summary>
    ///     Utility clas used to deflate ZLIB compressed data
    ///     Provides utility functions for dealing with compression.
    /// </summary>
    internal static class ZlibDeflater
    {
        /// <summary>
        ///     Deflates an array of ZLIB compresed bytes.
        /// </summary>
        /// <remarks>
        ///     The method of decompression used here is specifically for
        ///     compressed data from Aseprite which uses ZLIB.
        /// </remarks>
        /// <param name="buffer">
        ///     The array of compressed bytes.
        /// </param>
        /// <returns>
        ///     A byte array
        /// </returns>
        public static byte[] Deflate(byte[] buffer)
        {
            //  Put the buffer into a memory stream we can work with
            using (MemoryStream compressedStream = new MemoryStream(buffer))
            {
                //  The first two bytes of the compressed stream is zlib header
                //  information that we don't need and we need to ignore and
                //  advance the stream past.
                _ = compressedStream.ReadByte();
                _ = compressedStream.ReadByte();

                //  Now we can deflate
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                    {
                        deflateStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }
    }
}
