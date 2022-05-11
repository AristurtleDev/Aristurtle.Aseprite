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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Aristurtle.Aseprite.ThirdParty.Aseprite;
using Aristurtle.Aseprite.ThirdParty.Pixman;

namespace Aristurtle.Aseprite.IO
{
    /// <summary>
    ///     A data-model class that represents the data decoded from reading an
    ///     Aseprite .ase/.aseprite file.
    /// </summary>
    public partial class AsepriteFile
    {
        private List<Frame> _frames;                    //  Internal collection of all frame entries.
        private List<Layer> _layers;                    //  Internal collection of all layer entries.
        private Dictionary<int, string> _externalFiles; //  Internal collection of all external file entries.
        private List<Tag> _tags;                        //  Internal collection of all tag entries.
        private List<Slice> _slices;                    //  Internal collection of all slice entries.
        private List<Color> _palette;                   //  Internal collection of color palette.
        private Dictionary<int, Tileset> _tilesets;     //  Internal collection of all tilset entries.

        /// <summary>
        ///     Gets a <see cref="System.Version"/> class instance that
        ///     represents the detected version of the aseprite file that was
        ///     read.
        /// </summary>
        public Version Version { get; internal set; }

        // /// <summary>
        // ///     Gets the <see cref="Header"/> class instance that was read.
        // /// </summary>
        // public Header FileHeader { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates the total number of frames.
        /// </summary>
        public int FrameCount { get; internal set; }

        /// <summary>
        ///     Gets the a value that indicates the total width, in pixels,
        ///     of the canvas.
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates the total height, in pixels, of
        ///     the canvas.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates the color depth (bits per pixel)
        ///     used.
        /// </summary>
        public ColorDepth Depth { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates whether the opacity value of a
        ///     <see cref="Layer"/> class instance is a valid value to use.
        /// </summary>
        public bool LayerOpacityValid  { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates the index of the palette entry that
        ///     represents a transparent pixel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when the <see cref="ColorDepth"/> is
        ///     set to <see cref="ColorDepth.Index" />
        /// </remarks>
        public int TransparentIndex { get; internal set; }

        /// <summary>
        ///     Gets a value that indicates the total number of colors.
        /// </summary>
        public int ColorCount { get; internal set; }

        public ReadOnlyCollection<Frame> Frames { get; private set; }

        /// <summary>
        ///     Gets a read-only collection of all <see cref="Layer"/> class
        ///     instnaces that were read.
        /// </summary>
        public ReadOnlyCollection<Layer> Layers { get; private set; }

        /// <summary>
        ///     Gets a read-only dictionary collection of all external file
        ///     entries that were read, wher eeach dictionary entry the key
        ///     is the ID of the external file and the value is the path to
        ///     the external file.
        /// </summary>
        public ReadOnlyDictionary<int, string> ExternalFiles { get; private set; }

        /// <summary>
        ///     Gets a read-only collection of all <see cref="Tag"/> class
        ///     instances that were read.
        /// </summary>
        public ReadOnlyCollection<Tag> Tags { get; private set; }

        /// <summary>
        ///     Gets a read-only collection of all <see cref="Slice"/> class
        ///     instances that were read.
        /// </summary>
        public ReadOnlyCollection<Slice> Slices { get; private set; }

        /// <summary>
        ///     Gets a read-only collection of all <see cref="System.Drawing.Color"/>
        ///     values that were read that make up the palette.
        /// </summary>
        public ReadOnlyCollection<Color> Palette { get; private set; }

        /// <summary>
        ///     Gets a read-only dictionary collection of all
        ///     <see cref="Tileset"/> class instances that were read, where each
        ///     dictionary entry the key is the ID of the tileset and the
        ///     value is the <see cref="Tileset"/> class instance.
        /// </summary>
        public ReadOnlyDictionary<int, Tileset> Tilesets { get; private set; }

        /// <summary>
        ///     Creates a new <see cref="AsepriteFile"/> class
        ///     instance.
        /// </summary>
        internal AsepriteFile()
        {
            //  Instantiate the internal collections
            _frames = new List<Frame>();
            _layers = new List<Layer>();
            _externalFiles = new Dictionary<int, string>();
            _tags = new List<Tag>();
            _slices = new List<Slice>();
            _palette = new List<Color>();
            _tilesets = new Dictionary<int, Tileset>();

            //  Link the internal collections to the public facing read-only
            //  collections
            Frames = _frames.AsReadOnly();
            Layers = _layers.AsReadOnly();
            ExternalFiles = new ReadOnlyDictionary<int, string>(_externalFiles);
            Tags = _tags.AsReadOnly();
            Slices = _slices.AsReadOnly();
            Palette = _palette.AsReadOnly();
            Tilesets = new ReadOnlyDictionary<int, Tileset>(_tilesets);
        }

        public static AsepriteFile FromFile(string path)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("No file exists at the path given", path);
            }

            using(AsepriteBinaryReader reader = new AsepriteBinaryReader(path))
            {
                return reader.Read();
            }
        }

        /// <summary>
        ///     Adds the given <see cref="Frame"/> class instnace to the
        ///     internal frame collection of this instance.
        /// </summary>
        /// <param name="frame">
        ///     The <see cref="Frame"/> class instance io tadd.
        /// </param>
        internal void AddFrame(Frame frame) => _frames.Add(frame);

        /// <summary>
        ///     Adds the given <see cref="Layer"/> class instnace to the
        ///     internal layer collection of this instance.
        /// </summary>
        /// <param name="layer">
        ///     The <see cref="Layer"/> class instance io tadd.
        /// </param>
        internal void AddLayer(Layer layer) => _layers.Add(layer);

        /// <summary>
        ///     Adds an external file path with a unique id to the internal
        ///     collection of external files of this instance.
        /// </summary>
        /// <param name="id">
        ///     The unique ID of the external file.
        /// </param>
        /// <param name="path">
        ///     The path to the external file.
        /// </param>
        internal void AddExternalFile(int id, string path) => _externalFiles.Add(id, path);

        /// <summary>
        ///     Adds the given <see cref="Tag"/> class instnace to the
        ///     internal tag collection of this instance.
        /// </summary>
        /// <param name="tag">
        ///     The <see cref="Tag"/> class instance io tadd.
        /// </param>
        internal void AddTag(Tag tag) => _tags.Add(tag);

        /// <summary>
        ///     Adds the given <see cref="Slice"/> class instnace to the
        ///     internal slice collection of this instance.
        /// </summary>
        /// <param name="slice">
        ///     The <see cref="Slice"/> class instance io tadd.
        /// </param>
        internal void AddSlice(Slice slice) => _slices.Add(slice);

        /// <summary>
        ///     Adds the given <see cref="System.Drawing.Color"/> value to the
        ///     internal palette collection of this instance.
        /// </summary>
        /// <param name="color">
        ///     The <see cref="System.Drawing.Color"/> value to add.
        /// </param>
        internal void AddPaletteEntry(Color color) => _palette.Add(color);

        /// <summary>
        ///     Adss the given <see cref="Tileset"/> class instance to the
        ///     internal collection of this instnace.
        /// </summary>
        /// <param name="tileset">
        ///     The <see cref="Tileset"/> class instance to add.
        /// </param>
        internal void AddTileset(Tileset tileset) => _tilesets.Add(tileset.ID, tileset);


        public Color[] GetFrameColorData(int id, bool onlyVisible)
        {
            if (id < 0 || id >= _frames.Count)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            uint[] packedResult = new uint[Width * Height];
            Frame frame = _frames[id];

            //  Process all cels in the frame from top layer to bottom layer
            for (int i = 0; i < frame.Cels.Count; i++)
            {
                if (frame.Cels[i] is ImageCel cel)
                {
                    Layer layer = _layers[cel.LayerIndex];

                    if (layer.IsVisible|| !onlyVisible)
                    {
                        byte opacity = Combine32.MUL_UN8((byte)cel.Opacity, (byte)layer.Opacity);

                        for (int p = 0; p < cel.Pixels.Length; p++)
                        {
                            int x = (p % cel.Width) + cel.X;
                            int y = (p / cel.Width) + cel.Y;
                            int index = y * Width + x;

                            //  Sometimes a cel can have a negative x and/or y
                            //  value.  This is caused by selecting an area
                            //  within Aseprite and then moving a portion of the
                            //  selected pixels outside the canvas.  We don't
                            //  care about these pixels, so if the index is
                            //  outside the range of the array to store them in
                            //  then we just ignore it.
                            if (index < 0 || index >= packedResult.Length) { continue; }

                            //  Get the current value of the pixel at the index
                            uint backdrop = packedResult[index];

                            //  Conver the color we'll be blending to into a
                            //  packed value
                            Color sourceColor = cel.Pixels[p];
                            uint source = DocColor.rgba(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A);

                            //  Get the blend funciton we'll be using
                            Func<uint, uint, int, uint> blendFunc = Utilities.GetBlendFunction(layer.BlendMode);

                            //  Get the blended color value
                            uint blended = blendFunc.Invoke(backdrop, source, opacity);

                            packedResult[index] = blended;
                        }
                    }
                }
            }

            //  Flattening the frame gave us an array of packed uint values.
            //  I could have done color-to-uint conversion, then blend, the
            //  uint-to-color conversion during the entire process, but that
            //  would mean uneccessary conversions happening every loop. Instead
            //  I felt it would be better to just do one last loop here to
            //  conver the packed uint values to colors in one swoop
            Color[] flattenedFrame = new Color[packedResult.Length];
            for (int i = 0; i < packedResult.Length; i++)
            {
                //  Get the uint value.
                uint value = packedResult[i];

                //  Get the rgba byte values from the uint value.
                byte r = DocColor.rgba_getr(value);
                byte g = DocColor.rgba_getg(value);
                byte b = DocColor.rgba_getb(value);
                byte a = DocColor.rgba_geta(value);

                //  Add the color to the final array
                flattenedFrame[i] = Color.FromArgb(a, r, g, b);
            }

            return flattenedFrame;
        }

        public Spritesheet CreateSpritesheet(SpritesheetOptions options)
        {
            //  Instantiate a new SpriteSheet class instnace that we'll bukld
            //  and return back.
            Spritesheet spriteSheet = new Spritesheet();

            //  Generate the color data for each frame, frame by frame.
            List<Color[]> frames = new List<Color[]>();
            for (int i = 0; i < FrameCount; i++)
            {
                frames.Add(GetFrameColorData(i, options.OnlyVisibleFrames));
            }

            //  A key-value dictionary that maps the link between a frame and another frame
            //  that is a duplicate of that frame.  The key for the dictionary is the index
            //  of the frame that is a duplicate of another frame, with the value being the index
            //  of the frame that it is a duplicate of.
            Dictionary<int, int> duplicateMap = new Dictionary<int, int>();

            //  A key-value dictionary that linkes the index of a frame with
            //  it's resulting spritesheet frame instance.
            Dictionary<int, SpritesheetFrame> duplicateLookup = new Dictionary<int, SpritesheetFrame>();

            //  Setup some variables that will be used for later
            int columns;        //  Total number of columns in resulting spritesheet.
            int rows;           //  Total number of rows in resulting spritesheet.
            int totalFrames;    //  Total number of frames to write to spritesheet.

            //  If we are mergining duplicates, we need to build a map of the
            //  duplicate frames.
            if (options.MergeDuplicateFrames)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    for (int d = 0; d < i; d++)
                    {
                        if (frames[i].SequenceEqual(frames[d]))
                        {
                            duplicateMap.Add(i, d);
                            break;
                        }
                    }
                }
            }

            //  Calculate the total number of frames that we'll output to the
            //  resulting spritesheet.
            totalFrames = frames.Count - duplicateMap.Count;

            //  Calculate the total number of columns and rows the resulting
            //  spritesheet will have based on the type of spritesheet being
            //  created
            if (options.Type == SpritesheetType.Horizontal)
            {
                //  Horizontal spritesheet has one column per frame and only
                //  one row.
                columns = totalFrames;
                rows = 1;
            }
            else if (options.Type == SpritesheetType.Vertical)
            {
                //  Vertical spritesheet has one row per frame and only one
                //  column
                columns = 1;
                rows = totalFrames;
            }
            else
            {
                //  Otherwise we assume a packed spritesheet.
                //  To get the total number of columns and rows, we'll use a
                //  super basic packing method.
                //
                //  https://en.wikipedia.org/wiki/Square_packing_in_a_square

                //  Attempt to square root the frame count to get the number of
                //  columns.  This will return back true or false depending on
                //  if the result ended in a perfect square root or not.  If it
                //  wasn't perfect, we add 1 to the result.
                double sqrt = Math.Sqrt(totalFrames);
                columns = (int)Math.Floor(sqrt);
                if (Math.Abs(sqrt % 1) >= double.Epsilon)
                {
                    columns += 1;
                }

                //  Attempt to divide the number of frames by the total number
                //  of columns.  Check to see if the result has no remainder. If
                //  there is a remainder, add one to the result.
                rows = totalFrames / columns;
                if ((totalFrames % columns) != 0)
                {
                    rows += 1;
                }
            }

            //  Calculate the resulting spritesheet's widht and height,
            //  accounting for any padding and/or spacing specified.
            int width = (columns * Width) +
                            (options.BorderPadding * 2) +
                            (options.Spacing * (columns - 1)) +
                            (options.InnerPadding * 2 * columns);

            int height = (rows * Height) +
                         (options.BorderPadding * 2) +
                         (options.Spacing * (rows - 1)) +
                         (options.InnerPadding * 2 * rows);

            spriteSheet.Width = width;
            spriteSheet.Height = height;

            //  Instantiate the color array
            spriteSheet.Pixels = new Color[width * height];

            int frameOffset = 0;
            for (int f = 0; f < frames.Count; f++)
            {
                if (!options.MergeDuplicateFrames || !duplicateMap.ContainsKey(f))
                {
                    //  calcualte the x and y position of the frame's
                    //  top-left pixel relative to the top-left of the
                    //  final spritesheet
                    int frameColumn = (f - frameOffset) % columns;
                    int frameRow = (f - frameOffset) / columns;

                    //  Inject the pixel color data from the frame into the
                    //  final spritesheet color data array
                    Color[] pixels = frames[f];
                    for (int p = 0; p < pixels.Length; p++)
                    {
                        int x = (p % Width) + (frameColumn * Width);
                        int y = (p / Width) + (frameRow * Height);

                        if (options.BorderPadding > 0)
                        {
                            x += options.BorderPadding;
                            y += options.BorderPadding;
                        }

                        if (options.Spacing > 0)
                        {
                            if (frameColumn > 0)
                            {
                                x += options.Spacing * frameColumn;
                            }

                            if (frameRow > 0)
                            {
                                y += options.Spacing * frameRow;
                            }
                        }

                        if (options.InnerPadding > 0)
                        {
                            x += options.InnerPadding * (frameColumn + 1);
                            y += options.InnerPadding * (frameRow + 1);

                            if (frameColumn > 0)
                            {
                                x += options.InnerPadding * frameColumn;
                            }

                            if (frameRow > 0)
                            {
                                y += options.InnerPadding * frameRow;
                            }
                        }

                        int index = y * width + x;

                        spriteSheet.Pixels[index] = pixels[p];
                    }

                    //  Now create the frame data
                    SpritesheetFrame frame = new SpritesheetFrame();
                    frame.X = frameColumn * Width;
                    frame.Y = frameRow * Height;
                    frame.Width = Width;
                    frame.Height = Height;
                    frame.Duration = Frames[f].Duration;

                    if (options.BorderPadding > 0)
                    {
                        frame.X += options.BorderPadding;
                        frame.Y += options.BorderPadding;
                    }

                    if (options.Spacing > 0)
                    {
                        if (frameColumn > 0)
                        {
                            frame.X += options.Spacing * frameColumn;
                        }

                        if (frameRow > 0)
                        {
                            frame.Y += options.Spacing * frameRow;
                        }
                    }

                    if (options.InnerPadding > 0)
                    {
                        frame.X += options.InnerPadding * (frameColumn + 1);
                        frame.Y += options.InnerPadding * (frameRow + 1);

                        if (frameColumn > 0)
                        {
                            frame.X += options.InnerPadding * frameColumn;
                        }

                        if (frameRow > 0)
                        {
                            frame.Y += options.InnerPadding * frameColumn;
                        }
                    }

                    spriteSheet.Frames.Add(frame);
                    duplicateLookup.Add(f, frame);
                }
                else
                {
                    //  We are merging duplicates and it was detected that
                    //  the current frame to process is a duplicate.  So we
                    //  still add the Frame data but we need to make sure
                    //  the frame data added for this frame is the same as
                    //  it's duplicate frame.
                    SpritesheetFrame frame = duplicateLookup[duplicateMap[f]];
                    frame.Duration = Frames[f].Duration;
                    spriteSheet.Frames.Add(frame);
                    frameOffset++;
                }
            }
            return spriteSheet;
        }
    }
}

