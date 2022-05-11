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
using Aristurtle.Aseprite.IO;
using Aristurtle.Aseprite.ThirdParty.Aseprite;

namespace Aristurtle.Aseprite
{
    public static class Utilities
    {
        public static uint ColorToUint(Color color) => DocColor.rgba(color.R, color.G, color.B, color.A);
        public static Color UintToColor(uint value) => Color.FromArgb(DocColor.rgba_geta(value),
                                                                      DocColor.rgba_getr(value),
                                                                      DocColor.rgba_getg(value),
                                                                      DocColor.rgba_getb(value));

        public static Func<uint, uint, int, uint> GetBlendFunction(AsepriteFile.BlendMode mode)
        {
            switch (mode)
            {
                case AsepriteFile.BlendMode.Normal:
                    return BlendFuncs.rgba_blender_normal;
                case AsepriteFile.BlendMode.Multiply:
                    return BlendFuncs.rgba_blender_multiply;
                case AsepriteFile.BlendMode.Screen:
                    return BlendFuncs.rgba_blender_screen;
                case AsepriteFile.BlendMode.Overlay:
                    return BlendFuncs.rgba_blender_overlay;
                case AsepriteFile.BlendMode.Darken:
                    return BlendFuncs.rgba_blender_darken;
                case AsepriteFile.BlendMode.Lighten:
                    return BlendFuncs.rgba_blender_lighten;
                case AsepriteFile.BlendMode.ColorDodge:
                    return BlendFuncs.rgba_blender_color_dodge;
                case AsepriteFile.BlendMode.ColorBurn:
                    return BlendFuncs.rgba_blender_color_burn;
                case AsepriteFile.BlendMode.HardLight:
                    return BlendFuncs.rgba_blender_hard_light;
                case AsepriteFile.BlendMode.SoftLight:
                    return BlendFuncs.rgba_blender_soft_light;
                case AsepriteFile.BlendMode.Difference:
                    return BlendFuncs.rgba_blender_difference;
                case AsepriteFile.BlendMode.Exclusion:
                    return BlendFuncs.rgba_blender_exclusion;
                case AsepriteFile.BlendMode.Hue:
                    return BlendFuncs.rgba_blender_hsl_hue;
                case AsepriteFile.BlendMode.Saturation:
                    return BlendFuncs.rgba_blender_hsl_saturation;
                case AsepriteFile.BlendMode.Color:
                    return BlendFuncs.rgba_blender_hsl_color;
                case AsepriteFile.BlendMode.Luminosity:
                    return BlendFuncs.rgba_blender_hsl_luminosity;
                case AsepriteFile.BlendMode.Addition:
                    return BlendFuncs.rgba_blender_addition;
                case AsepriteFile.BlendMode.Subtract:
                    return BlendFuncs.rgba_blender_subtract;
                case AsepriteFile.BlendMode.Divide:
                    return BlendFuncs.rgba_blender_divide;
                default:
                    throw new Exception("Unknown blend mode");
            }
        }
    }
}
