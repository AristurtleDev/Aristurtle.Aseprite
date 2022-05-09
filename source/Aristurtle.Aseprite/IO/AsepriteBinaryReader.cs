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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Aristurtle.Aseprite.IO.Compression;

#pragma warning disable format

namespace Aristurtle.Aseprite.IO
{
    /// <summary>
    ///     A <see cref="BinaryReader" /> wrapper used to read the contents of
    ///     an Aseprite .ase/.aseprite file.
    /// </summary>
    public sealed class AsepriteBinaryReader : IDisposable
    {
        private bool _isDisposed;                               //  Whether this instance has been properly disposed of.
        private Stream _stream;                                 //  Cached reference to the underlying stream used to read the file.
        private BinaryReader _reader;                           //  Cached reference to the BinaryReader used to read the stream.
        private AsepriteFile _result;                           //  Cached reference to the result that is being built.
        private AsepriteFile.Frame _currentFrame;               //  Cached reference to the current frame being read.
        private int _chunksRead;                                //  Count of the total chunks that have been read.
        private AsepriteFile.ChunkWithUserData _lastReadChunk;  //  Cached reference to the last chunk read that can have user data.

        /// <summary>
        ///     Creates a new <see cref="AsepriteBinaryReader"/> class instance.
        /// </summary>
        /// <param name="path">
        ///     The fully-qualified absolute path to an Aseprite .ase/.aseprite
        ///     file to read.
        /// </param>
        public AsepriteBinaryReader(string path)
            : this(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)) { }

        /// <summary>
        ///     Creates a new <see cref="AsepriteBinaryReader"/> class instance.
        /// </summary>
        /// <param name="buffer">
        ///     A <see langword="byte" /> array containing the data from an
        ///     Aseprite .ase/.aseprite file.
        /// </param>
        public AsepriteBinaryReader(byte[] buffer)
            : this(new MemoryStream(buffer)) { }

        /// <summary>
        ///     Creates a new <see cref="AsepriteBinaryReader"/> class instance.
        /// </summary>
        /// <param name="stream">
        ///     An existing <see cref="System.IO.Stream"/> instance that can be
        ///     used to read the contents of an Aseprite .ase/.aseprite file.
        /// </param>
        public AsepriteBinaryReader(Stream stream)
        {
            _stream = stream;
            _reader = new BinaryReader(stream, Encoding.UTF8);
        }

        /// <summary>
        ///     Finalizer implementation used to internally call the dispose
        ///     method.
        /// </summary>
        ~AsepriteBinaryReader()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Gracefully disposes of resources managed by this class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gracefully disposes of resources managed by this class instance.
        /// </summary>
        /// <param name="disposeManagedResources">
        ///     A value that indicates if managed resources should be disposed
        ///     of.
        /// </param>
        private void Dispose(bool disposeManagedResources)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposeManagedResources)
            {
                _reader.Dispose();
                _reader = null;
                _stream = null;
            }

            _isDisposed = true;
        }

        public AsepriteFile Read()
        {
            //  Instantiate a new AsepriteReadResult class instance and then
            //  immediatly cache it so we can use it throughout
            AsepriteFile result = new AsepriteFile();
            _result = result;

            //  Read the 128-byte header.
            ReadHeader();

            //  Read frame by frame until all frames are read
            for (int i = 0; i < _result.FrameCount; i++)
            {
                ReadFrame();
            }

            //  Return the result
            return result;
        }

        /// <summary>
        ///     Reads the next byte from the current stream and advances the
        ///     current position of the stream by one byte.
        /// </summary>
        /// <returns>
        ///     The next byte read from teh current stream.
        /// </returns>
        private byte ReadByte() => _reader.ReadByte();

        /// <summary>
        ///     Reads the specified number of bytes from the current stream into
        ///     a byte array and advances the current position of the stream
        ///     by that number of bytes.
        /// </summary>
        /// <param name="count">
        ///     The number of bytes to read.
        /// </param>
        /// <returns>
        ///     A byte array contining the data read from the stream.  This
        ///     might be less than the number of bytes requested if the end of
        ///     the stream is reached.
        /// </returns>
        private byte[] ReadBytes(int count) => _reader.ReadBytes(count);

        /// <summary>
        ///     Reads a 2-byte unsigned integer from the current stream and
        ///     advances the current stream by two bytes.
        /// </summary>
        /// <returns>
        ///     A 2-byte unsigned integer read from the current stream.
        /// </returns>
        private ushort ReadWord() => _reader.ReadUInt16();

        /// <summary>
        ///     Reads a 2-byte signed integer from the current stream and
        ///     advances the current stream by two bytes.
        /// </summary>
        /// <returns>
        ///     A 2-byte signed integer read from the current stream.
        /// </returns>
        private short ReadShort() => _reader.ReadInt16();

        /// <summary>
        ///     Reads a 4-byte unsigned integer from the current stream and
        ///     advances the current stream by four bytes.
        /// </summary>
        /// <returns>
        ///     A 4-byte unsigned integer read from the current stream.
        /// </returns>
        private uint ReadDword() => _reader.ReadUInt32();

        /// <summary>
        ///     Reads a 4-byte signed integer from the current stream and
        ///     advances the current stream by four bytes.
        /// </summary>
        /// <returns>
        ///     A 4-byte signed integer read from the current stream.
        /// </returns>
        private int ReadLong() => _reader.ReadInt32();

        /// <summary>
        ///     Reads a string from the current stream.  String are prepended by
        ///     a 2-byte unsigned integer that tells the length of the stream to
        ///     read.  The current stream is advanced by 2+len bytes.
        /// </summary>
        /// <returns>
        ///     A string.
        /// </returns>
        private string ReadString() => Encoding.UTF8.GetString(ReadBytes(ReadWord()));

        /// <summary>
        ///     Reads an Aseprite header from the current stream and advances
        ///     the stream to the end of the header.
        /// </summary>
        private void ReadHeader()
        {
            //  Instantiate a new Header class instance
            // Header header = new Header();
            _                        = ReadDword();              //  File Size (discarded)
            ushort magic             = ReadWord();               //  Magic Number
            _result.FrameCount       = ReadWord();               //  Total number of frames
            _result.Width            = ReadWord();               //  Width of canvas, in pixels
            _result.Height           = ReadWord();               //  Height of canvas, in pixels
            _result.Depth            = (AsepriteFile.ColorDepth)ReadWord();   //  Color Depth (bits per pixels)
            uint flags               = ReadDword();              //  Flags
            _                        = ReadWord();               //  Milliseconds between frames (discarded)
            _                        = ReadDword();              //  Set to 0 (discarded)
            _                        = ReadDword();              //  Set to 0 (discarded)
            _result.TransparentIndex = ReadByte();               //  Index of transparent color in palette
            _                        = ReadBytes(3);             //  3-bytes of padding (discarded)
            _result.ColorCount       = ReadWord();               //  Number of Colors
            _                        = ReadByte();               //  Pixel Width (discarded)
            _                        = ReadByte();               //  Pixel Height (discarded)
            _                        = ReadShort();              //  X position of the grid (discarded)
            _                        = ReadShort();              //  Y position of the grid (discarded)
            _                        = ReadWord();               //  Grid Width (discarded)
            _                        = ReadWord();               //  Grid Height (discarded)
            _                        = ReadBytes(84);            //  Reserved 84 bytes (discarded)

            //  Validate the header magic number.
            if (magic != 0xA5E0)
            {
                string message = "Invalid file format. This does not appear to be a valid Aseprite " +
                                 ".ase/.aseprite file.  Header magic number was invalid, expected 0xA5E0, " +
                                 $"instead got 0x{magic:X2}";
                throw new Exception(message);
            }

            //  Transparent index is only valid for Indexed images
            if (_result.Depth != AsepriteFile.ColorDepth.Index)
            {
                _result.TransparentIndex = 0;
            }

            //  If color count is 0, this actually means 256 (for older .ase)
            if (_result.ColorCount == 0)
            {
                _result.ColorCount = 256;
            }

            //  Set properties  based on header flags
            _result.LayerOpacityValid = ((flags & 1) != 0);

            // //  Store the header in the reasult
            // _result.Header = header;
        }

        /// <summary>
        ///     Reads a Asperite frame from the current stream and advances the
        ///     stream to the end of the frame.
        /// </summary>
        private void ReadFrame()
        {
            //  Instantiate a new frame class instnace and immediatly cache
            //  it for internal use
            AsepriteFile.Frame frame = new AsepriteFile.Frame();
            _currentFrame = frame;

            //  Get the position of the base stream at the start of the frame
            long framePos = _stream.Position;

            //  Read the frame header
            uint frameLen  = ReadDword();       //  Size of frame, in bytes.
            ushort magic   = ReadWord();        //  Frame header magic number.
            int chunks     = ReadWord();        //  Old field that specifies the number of chunks.
            frame.Duration = ReadWord();        //  The duration of the frame, in milliseconds.
            _              = ReadBytes(2);      //  2-bytes reserved for future use (discarded).
            int newChunks  = (int)ReadDword();  //  New field that specifies the number of chunks.

            //  Validate the frame header magic numbeer
            if (magic != 0xF1FA)
            {
                string message = "Invalid file format. This does not appear to be a valid Aseprite " +
                                 ".ase/.aseprite file.  Frame magic number was invalid, expected 0xF1FA, " +
                                 $"instead got 0x{magic:X2}";
                throw new Exception(message);
            }

            //  Determine the total number of chunks that need to be read by
            //  comparing the old and new chunk count fields.
            chunks = chunks == 0xFFFF && chunks < newChunks ? newChunks : chunks;

            //  Read chunk by chunk until all chunks have been read for this
            //  frame.
            for (_chunksRead = 0; _chunksRead < chunks; _chunksRead++)
            {
                ReadChunk();
            }

            //  Validate that the current stream position matches where we
            //  expect it should be
            if (_stream.Position != framePos + frameLen)
            {
                string message = "Stream position misaligned after reading frame.";
                throw new Exception(message);
            }

            //  Add the frame to the result collection and remove the cached
            //  reference to the frame
            _result.AddFrame(frame);
            _currentFrame = null;
        }

        private void ReadChunk()
        {
            //  Cache the position of the current stream at the start of the
            //  chunk
            long chunkPos = _stream.Position;

            //  Read the chunk header.
            uint chunkLen       = ReadDword();              //  The total size of the chunk in bytes.
            AsepriteFile.ChunkType chunkType = (AsepriteFile.ChunkType)ReadWord();    //  The type of chunk to be read.

            //  Read the chunk data based on the type of chunk to read
            switch (chunkType)
            {
                case AsepriteFile.ChunkType.Layer:
                    ReadLayerChunk();
                    break;
                case AsepriteFile.ChunkType.Cel:
                    //  We subtract 6 here becuase reading the len and type
                    //  above was a total of 6 bytes. The pixel data in a cel
                    //  doesn't have a len value so we need to know the exact
                    //  size of the data portion when reading.
                    ReadCelChunk((int)(chunkLen - 6));
                    break;
                case AsepriteFile.ChunkType.ExternalFiles:
                    ReadExternalFilesChunk();
                    break;
                case AsepriteFile.ChunkType.Tags:
                    ReadTagsChunk();
                    break;
                case AsepriteFile.ChunkType.Palette:
                    ReadPaletteChunk();
                    break;
                case AsepriteFile.ChunkType.UserData:
                    ReadUserDataChunk();
                    break;
                case AsepriteFile.ChunkType.Slice:
                    ReadSliceChunk();
                    break;
                case AsepriteFile.ChunkType.Tileset:
                    ReadTilesetChunk();
                    break;
                //  The following chunk types are ignored either because the
                //  data is not needed for our purposes, or becuase they are
                //  only for backwards compatibility for older Aseprite versions
                //  which are out of scope for this library.
                case AsepriteFile.ChunkType.OldPaletteA:
                case AsepriteFile.ChunkType.OldPaletteB:
                case AsepriteFile.ChunkType.CelExtra:
                case AsepriteFile.ChunkType.ColorProfile:
                case AsepriteFile.ChunkType.Mask:
                case AsepriteFile.ChunkType.Path:
                    //  Since we are ignoring these, just advance the stream to
                    //  the end of the chunk
                    _stream.Position = chunkPos + chunkLen;
                    break;
                default:
                    throw new Exception($"Unknown chunk type 0x{chunkType:X2} detected");
            }
        }

        /// <summary>
        ///     Reads a Layer chunk from the current stream and adavnaces the
        ///     stream to the end of the chunk.
        /// </summary>
        private void ReadLayerChunk()
        {
            AsepriteFile.Layer layer = new AsepriteFile.Layer();

            ushort flags        = ReadWord();   //  Layer flags
            layer.Type          = (AsepriteFile.LayerType)ReadWord();    //  Type of layer.
            layer.ChildLevel    = ReadWord();               //  Child level of layer.
            _                   = ReadWord();               //  Layer Width (discarded).
            _                   = ReadWord();               //  Layer Height (discarded).
            layer.BlendMode     = (AsepriteFile.BlendMode)ReadWord();    //  Blend mode used by the layer.
            layer.Opacity       = ReadByte();               //  Layer opacity.
            _                   = ReadBytes(3);             //  3-bytes of padding (discarded).
            layer.Name          = ReadString();             //  Name of layer

            if(layer.Type == AsepriteFile.LayerType.Tilemap)
            {
                layer.TilesetIndex = (int)ReadDword();  //  Tileset index.
            }
            else
            {
                layer.TilesetIndex = -1;
            }

            //  Set values based on layer flags
            layer.IsVisible          = (flags & 1) != 0;
            layer.IsEditable         = (flags & 2) != 0;
            layer.LockMovement       = (flags & 4) != 0;
            layer.IsBackground       = (flags & 8) != 0;
            layer.PrefersLinkedCels  = (flags & 16) != 0;
            layer.DisplayedCollapsed = (flags & 32) != 0;
            layer.IsReferenceLayer   = (flags & 64) != 0;

            //  Add the layer to the internal collection and set it as the last
            //  chunk read.
            _result.AddLayer(layer);
            _lastReadChunk = layer;
        }

        /// <summary>
        ///     Reads a cel chunk from the current stream and advances the
        ///         stream to the end of the chunk.
        /// </summary>
        /// <param name="len">
        ///     The length, in bytes, of the cel chunk's data.
        /// </param>
        private void ReadCelChunk(int len)
        {
            //  Cache the current position of the stream at the start of reading
            //  the cel chunk
            long celPos = _stream.Position;

            //  Read the initial cel data
            int layer            = ReadWord();          //  Index of the layer this cel is on.
            int x                = ReadShort();         //  X-coordinate position of cel.
            int y                = ReadShort();         //  Y-coordinate position of cel.
            int opacity          = ReadByte();          //  Opacity level of the cel.
            AsepriteFile.CelType type         = (AsepriteFile.CelType)ReadWord(); //  The type of cel
            _                    = ReadBytes(7);        //  7-bites padding (discarded)

            //  The rest of the data being read is based on the type of cel
            if (type == AsepriteFile.CelType.RawImage || type == AsepriteFile.CelType.CompressedImage)
            {
                //  Instantiate a new ImageCel class instance and copy the data
                //  that we've already read
                AsepriteFile.ImageCel cel = new AsepriteFile.ImageCel();
                cel.LayerIndex = layer;
                cel.X = x;
                cel.Y = y;
                cel.Opacity = opacity;
                cel.Type = type;

                //  Read the width and height
                cel.Width   = ReadWord();   //  Width of the cel
                cel.Height  = ReadWord();   //  Height of the cel

                //  Calcualte the remaining bytes to read in this chunk
                long bytesToRead = len - (_stream.Position - celPos);

                //  Read the remaining bytes
                byte[] buffer = ReadBytes((int)bytesToRead);

                //  If this cel is a Raw Image type, then we can just copy
                //  the buffer, otherwise, we'll need to decompress the data
                byte[] pixels;
                if (cel.Type == AsepriteFile.CelType.RawImage)
                {
                    pixels = new byte[buffer.Length];
                    Buffer.BlockCopy(buffer, 0, pixels, 0, buffer.Length);
                }
                else
                {
                    //  Compressed image data needs to be deflated first.
                    pixels = ZlibDeflater.Deflate(buffer);
                }

                //  Now that we have the decompressed pixel data, we can convert
                //  it to Color values.  How we do this is based on the color
                //  depth of the image.
                cel.Pixels = new Color[cel.Width * cel.Height];
                if (_result.Depth == AsepriteFile.ColorDepth.RGBA)
                {
                    //  RGBA is 4-bytes (32-bits) per pixel
                    for (int i = 0, b = 0; i < cel.Pixels.Length; i++, b += 4)
                    {
                        byte red = pixels[b];
                        byte green = pixels[b + 1];
                        byte blue = pixels[b + 2];
                        byte alpha = pixels[b + 3];
                        cel.Pixels[i] = Color.FromArgb(alpha, red, green, blue);
                    }
                }
                else if (_result.Depth == AsepriteFile.ColorDepth.Grayscale)
                {
                    //  Grayscale is 2-bytes (16-bits) per pixel
                    for (int i = 0, b = 0; i < cel.Pixels.Length; i++, b += 2)
                    {
                        byte red = pixels[b];
                        byte green = pixels[b];
                        byte blue = pixels[b];
                        byte alpha = pixels[b + 1];
                        cel.Pixels[i] = Color.FromArgb(alpha, red, green, blue);
                    }
                }
                else if (_result.Depth == AsepriteFile.ColorDepth.Index)
                {
                    //  Indexed is 1-byte (8-bits) per pixel. The actual value
                    //  given is the index of the color from the palette
                    for (int i = 0; i < cel.Pixels.Length; i++)
                    {
                        int paletteIndex = pixels[i];

                        //  If the palette index is the same as the transparent
                        //  color index, then we just a fully transparent
                        //  pixel
                        if (paletteIndex == _result.TransparentIndex)
                        {
                            cel.Pixels[i] = Color.FromArgb(0, 0, 0, 0);
                        }
                        else
                        {
                            //  Get the color from the palette
                            cel.Pixels[i] = _result.Palette[paletteIndex];
                        }
                    }
                }
                else
                {
                    throw new Exception("Unrecognized colord depth");
                }

                //  Add the cel to the frame
                _currentFrame.AddCel(cel);
            }
            else if (type == AsepriteFile.CelType.Linked)
            {
                //  Linked cels are 1:1 with the cel they are linked with.
                //
                //  Read the frame position the cel is linked to.
                ushort linkedFrame = ReadWord();

                //  Get a reference to the linked cel from that frame.
                AsepriteFile.Cel linkedCel = _result.Frames[linkedFrame]
                                .Cels
                                .FirstOrDefault(c => c.LayerIndex == layer);

                if (linkedCel == null)
                {
                    throw new Exception("Unable to locate linked cel");
                }

                //  Clone the linked cel
                if (linkedCel is AsepriteFile.ImageCel linkedImagedCel)
                {
                    AsepriteFile.ImageCel cel = new AsepriteFile.ImageCel(linkedImagedCel);
                    _currentFrame.AddCel(cel);
                }
                else if (linkedCel is AsepriteFile.TilemapCel linkedTileCel)
                {
                    AsepriteFile.TilemapCel cel = new AsepriteFile.TilemapCel(linkedTileCel);
                    _currentFrame.AddCel(cel);
                }
                else
                {
                    throw new Exception("Linked cel is an unknown type");
                }
            }
            else if (type == AsepriteFile.CelType.CompressedTilemap)
            {
                AsepriteFile.TilemapCel cel = new AsepriteFile.TilemapCel();
                cel.LayerIndex = layer;
                cel.X = x;
                cel.Y = y;
                cel.Opacity = opacity;
                cel.Type = type;

                cel.Width           = ReadWord();       // Width of the cel.
                cel.Height          = ReadWord();       //  Height of the cel.
                cel.BitsPerTile     = ReadWord();       //  Bits per tile
                cel.TileIDBitmask   = (int)ReadDword(); //  Bitmask for Tile ID.
                cel.XFlipBitmask    = (int)ReadDword(); //  Bitmask for X Flip.
                cel.YFlipBitmask    = (int)ReadDword(); //  Bitmask for Y Flip.
                cel.RotationBitmask = (int)ReadDword(); //  Bitmask for 90CW rotation.
                _                   = ReadBytes(10);    //  10-bytes of padding (discarded).

                //  The remaining tile data is compressed and needs to be
                //  deflated first
                //
                //  Determine the number of bytes to read
                long bytesToRead = len - (_stream.Position - celPos);

                //  Read the remaining data
                byte[] buffer = ReadBytes((int)bytesToRead);

                //  Deflate the buffer.
                cel.Tiles = ZlibDeflater.Deflate(buffer);

                //  Add the cel to the frame
                _currentFrame.AddCel(cel);
            }
        }


        /// <summary>
        ///     Reads an external files chunk from the current stream and
        ///     advances the stream to the end of the chunk.
        /// </summary>
        private void ReadExternalFilesChunk()
        {
            int count = (int)ReadDword();   //  Number of entries.
            _ = ReadBytes(8);   //  8-bytes of padding (discarded).

            //  Read entry by entry until all entries are read.
            for (int i = 0; i < count; i++)
            {
                int id = (int)ReadDword();   //  Entry ID.
                _ = ReadBytes(8);   //  8-bytes of padding (discarded).
                string path = ReadString(); //  External file name.

                //  Add to the external files collection
                _result.AddExternalFile(id, path);
            }
        }

        /// <summary>
        ///     Reads a tag chunk from the current stream and adavnes the stream
        ///     to the end of the chunk.
        /// </summary>
        private void ReadTagsChunk()
        {
            int count = ReadWord(); //  Number of tags.
            _ = ReadBytes(8);   //  8-bytes of padding (discarded).

            //  Read tag by tag until all tags are read
            for (int i = 0; i < count; i++)
            {
                AsepriteFile.Tag tag = new AsepriteFile.Tag();

                tag.From      = ReadWord();             //  From frame.
                tag.To        = ReadWord();             //  To frame.
                tag.Direction = (Direction)ReadByte();  //  Loop animation direction.
                _             = ReadBytes(8);           //  8-bytes of padding (discarded).
                byte[] color  = ReadBytes(3);           //  Tag color.
                _             = ReadByte();             //  1-byte padding (discarded).
                tag.Name      = ReadString();           //  Tag Name.

                _result.AddTag(tag);
            }

            //  Starting with Aseprite 1.3, there may exist user data chunks
            //  directly after the tags chunk.  If this is the case, then we
            //  need to read each user data chunk after the tags and apply them
            //  to the tags just created.
            //
            //  If the next chunk is not a user data chunk, then we instead can
            //  assume the file version is 1.2.x and can reset the stream
            //  position back.
            for (int i = 0; i < count; i++)
            {
                //  Cache the stream position
                long pos = _stream.Position;

                _ = ReadDword();           //  Chunk Size.
                AsepriteFile.ChunkType type = (AsepriteFile.ChunkType)ReadWord(); //  Chunk type

                if (type == AsepriteFile.ChunkType.UserData)
                {
                    //  This is a v1.3 file
                    _result.Version = new Version(1, 3);

                    //  Set the tag as the last chunk, then read the user data
                    _lastReadChunk = _result.Tags[i];
                    ReadUserDataChunk();

                    //  Note that we increment the total number of chunks read
                    //  here since we just read a chunk
                    _chunksRead++;
                }
                else
                {
                    _result.Version = new Version(1, 2);
                    //  No user data, reset stream
                    _stream.Position = pos;

                    //  Break out of loop, we don't need to continue
                    break;
                }
            }
        }

        /// <summary>
        ///     Reads a Palette chunk from the current stream and adavnaces the
        ///     stream to the end of the chunk.
        /// </summary>
        private void ReadPaletteChunk()
        {
            //  Read the palette header
            int size      = (int)ReadDword();   //  New palette size (total number of entries).
            int firstIndx = (int)ReadDword();   //  First color index to change.
            int lastIndex = (int)ReadDword();   //  Last color index to change.
            _             = ReadBytes(8);       //  8-bytes padding (ignored).

            //  TODO: Remove unused code.
            // //  Initialize the palette array
            // _palette = new Color[size];

            //  Read each color entry and add to the palette array
            for (int i = 0; i < lastIndex - firstIndx + 1; i++)
            {
                ushort flag = ReadWord();   //  Entry flag
                byte r      = ReadByte();   //  Red (0 - 255)
                byte g      = ReadByte();   //  Green (0 - 255)
                byte b      = ReadByte();   //  Blue (0 - 255)
                byte a      = ReadByte();   //  Alpha (0 - 255)

                //  If flag bitmask 1 is set, then we need to read the name
                if ((flag & 1) != 0)
                {
                    _       = ReadString(); //  Color name (discarded).
                }

                //  Create the palette entry color
                _result.AddPaletteEntry(Color.FromArgb(a, r, g, b));
            }
        }

        /// <summary>
        ///     Reads a user data chunk from the current stream, applies it to
        ///     the last read chunk, and advances the stream to the end of the
        ///     user data chunk.
        /// </summary>
        private void ReadUserDataChunk()
        {
            AsepriteFile.UserData data = new AsepriteFile.UserData();

            data.Flags = (AsepriteFile.AsepriteUserDataFlags)ReadDword();    //  Flags

            if ((data.Flags & AsepriteFile.AsepriteUserDataFlags.HasText) != 0)
            {
                data.Text = ReadString();   //  User data text
            }

            if ((data.Flags & AsepriteFile.AsepriteUserDataFlags.HasColor) != 0)
            {
                byte r = ReadByte();    //  Red (0 - 255).
                byte g = ReadByte();    //  Green (0 - 255).
                byte b = ReadByte();    //  Blue (0 - 255).
                byte a = ReadByte();    //  Alpha (0 - 255).

                data.Color = Color.FromArgb(a, r, g, b);
            }

            _lastReadChunk.UserData = data;
        }

        /// <summary>
        ///     Reads a slice chunk from the current stream and advances the
        ///     stream to the end of the chunk.
        /// </summary>
        private void ReadSliceChunk()
        {
            AsepriteFile.Slice slice = new AsepriteFile.Slice();

            int count   = (int)ReadDword();         //  Number of keys for this slice
            slice.Flags = (AsepriteFile.SliceFlags)ReadDword();  //  Slice flags.
            _           = ReadDword();              //  Reserved (discarded).
            slice.Name  = ReadString();             //  The name of the slice.

            //  Read key by key until all keys for this slice have been read.
            for (int i = 0; i < count; i++)
            {
                AsepriteFile.SliceKey key = new AsepriteFile.SliceKey();

                key.Frame = (int)ReadDword();   //  Key frame
                int x     = ReadLong();         //  x-coordinate position of slice.
                int y     = ReadLong();         //  y-coordiante position of slice.
                int w     = (int)ReadDword();   //  Width of the slice.
                int h     = (int)ReadDword();   //  Height of the slice.

                key.Bounds = new Rectangle(x, y, w, h);

                if (slice.IsNinePatch)
                {
                    int cx = ReadLong();        //  Center rect x-coordinate position.
                    int cy = ReadLong();        //  Center rect y-coordiante position.
                    int cw = (int)ReadDword();  //  Center rect width.
                    int ch = (int)ReadDword();  //  Center rect height.

                    key.CenterBounds = new Rectangle(cx, cy, cw, ch);
                }
                else
                {
                    key.CenterBounds = Rectangle.Empty;
                }

                if (slice.HasPivot)
                {
                    int px = ReadLong();    //  Pivot x-coordinate position;
                    int py = ReadLong();    //  Pivot y-coordinate position.

                    key.Pivot = new Point(px, py);
                }
                else
                {
                    key.Pivot = Point.Empty;
                }

                slice.AddKey(key);
            }

            _result.AddSlice(slice);
            _lastReadChunk = slice;
        }

        private void ReadTilesetChunk()
        {
            AsepriteFile.Tileset tileset = new AsepriteFile.Tileset();

            tileset.ID        = (int)ReadDword();           //  Tileset ID.
            tileset.Flags     = (AsepriteFile.TilesetFlags)ReadDword();  //  Flags.
            tileset.TileCount = (int)ReadDword();           //  Total number of tiles.
            int w             = ReadWord();                 //  Width of a tile
            int h             = ReadWord();                 //  Height of a tile
            tileset.Size      = new Size(w, h);
            tileset.BaseIndex = ReadShort();                //  Base index number to show from tile with id 1 and on.
            _                 = ReadBytes(14);              //  14-bytes of padding (discarded).
            tileset.Name      = ReadString();               //  Name of tileset.

            if (tileset.IncludesExternalFile)
            {
                tileset.ExternalFileID = (int)ReadDword();  //  ID of external file.
                tileset.TilesetID      = (int)ReadDword();  //  ID of tileset in external file.
            }

            if (tileset.IncludesTiles)
            {
                int len       = (int)ReadDword();   //  Length of compressed data
                byte[] buffer = ReadBytes(len);     //  Compressed pixel data

                byte[] pixels = ZlibDeflater.Deflate(buffer);

                tileset.Pixels = new Color[tileset.Size.Width * (tileset.Size.Height * tileset.TileCount)];

                if (_result.Depth == AsepriteFile.ColorDepth.RGBA)
                {
                    //  RGBA is 4-bytes (32-bits) per pixel
                    for (int i = 0, b = 0; i < tileset.Pixels.Length; i++, b += 4)
                    {
                        byte red = pixels[b];
                        byte green = pixels[b + 1];
                        byte blue = pixels[b + 2];
                        byte alpha = pixels[b + 3];
                        tileset.Pixels[i] = Color.FromArgb(alpha, red, green, blue);
                    }
                }
                else if (_result.Depth == AsepriteFile.ColorDepth.Grayscale)
                {
                    //  Grayscale is 2-bytes (16-bits) per pixel
                    for (int i = 0, b = 0; i < tileset.Pixels.Length; i++, b += 2)
                    {
                        byte red = pixels[b];
                        byte green = pixels[b];
                        byte blue = pixels[b];
                        byte alpha = pixels[b + 1];
                        tileset.Pixels[i] = Color.FromArgb(alpha, red, green, blue);
                    }
                }
                else if (_result.Depth == AsepriteFile.ColorDepth.Index)
                {
                    //  Indexed is 1-byte (8-bits) per pixel. The actual value
                    //  given is the index of the color from the palette
                    for (int i = 0; i < tileset.Pixels.Length; i++)
                    {
                        int paletteIndex = pixels[i];

                        //  If the palette index is the same as the transparent
                        //  color index, then we just a fully transparent
                        //  pixel
                        if (paletteIndex == _result.TransparentIndex)
                        {
                            tileset.Pixels[i] = Color.FromArgb(0, 0, 0, 0);
                        }
                        else
                        {
                            //  Get the color from the palette
                            tileset.Pixels[i] = _result.Palette[i];
                        }
                    }
                }
                else
                {
                    throw new Exception("Unrecognized colord depth");
                }

            }

            _result.AddTileset(tileset);
        }
    }
}
#pragma warning restore format
