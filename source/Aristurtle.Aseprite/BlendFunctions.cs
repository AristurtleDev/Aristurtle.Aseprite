using System;
using System.Drawing;
using Aristurtle.Aseprite.IO;
using Aristurtle.Aseprite.ThirdParty.Pixman;
using Aristurtle.Aseprite.ThirdParty.Aseprite;

namespace Aristurtle.Aseprite
{
    public static class BlendFunctions
    {
        public static Color Blend(Color backdrop,
                                  Color source,
                                  byte celOpacity,
                                  byte layerOpacity,
                                  AsepriteFile.BlendMode blendMode)
        {
            uint b = backdrop.ToUint32();
            uint c = source.ToUint32();

            uint blended = Blend(b, c, celOpacity, layerOpacity, blendMode);

            return Color.FromArgb(DocColor.rgba_geta(blended),
                                  DocColor.rgba_getr(blended),
                                  DocColor.rgba_getg(blended),
                                  DocColor.rgba_getb(blended));
        }

        public static uint Blend(uint backdrop,
                                  uint source,
                                  byte celOpacity,
                                  byte layerOpacity,
                                  AsepriteFile.BlendMode blendMode)
        {
            byte opacity = Combine32.MUL_UN8(celOpacity, layerOpacity);

            Func<uint, uint, int, uint> blender;
            switch (blendMode)
            {
                case AsepriteFile.BlendMode.Normal:
                    blender = BlendFuncs.rgba_blender_normal;
                    break;
                case AsepriteFile.BlendMode.Multiply:
                    blender = BlendFuncs.rgba_blender_multiply;
                    break;
                case AsepriteFile.BlendMode.Screen:
                    blender = BlendFuncs.rgba_blender_screen;
                    break;
                case AsepriteFile.BlendMode.Overlay:
                    blender = BlendFuncs.rgba_blender_overlay;
                    break;
                case AsepriteFile.BlendMode.Darken:
                    blender = BlendFuncs.rgba_blender_darken;
                    break;
                case AsepriteFile.BlendMode.Lighten:
                    blender = BlendFuncs.rgba_blender_lighten;
                    break;
                case AsepriteFile.BlendMode.ColorDodge:
                    blender = BlendFuncs.rgba_blender_color_dodge;
                    break;
                case AsepriteFile.BlendMode.ColorBurn:
                    blender = BlendFuncs.rgba_blender_color_burn;
                    break;
                case AsepriteFile.BlendMode.HardLight:
                    blender = BlendFuncs.rgba_blender_hard_light;
                    break;
                case AsepriteFile.BlendMode.SoftLight:
                    blender = BlendFuncs.rgba_blender_soft_light;
                    break;
                case AsepriteFile.BlendMode.Difference:
                    blender = BlendFuncs.rgba_blender_difference;
                    break;
                case AsepriteFile.BlendMode.Exclusion:
                    blender = BlendFuncs.rgba_blender_exclusion;
                    break;
                case AsepriteFile.BlendMode.Hue:
                    blender = BlendFuncs.rgba_blender_hsl_hue;
                    break;
                case AsepriteFile.BlendMode.Saturation:
                    blender = BlendFuncs.rgba_blender_hsl_saturation;
                    break;
                case AsepriteFile.BlendMode.Color:
                    blender = BlendFuncs.rgba_blender_hsl_color;
                    break;
                case AsepriteFile.BlendMode.Luminosity:
                    blender = BlendFuncs.rgba_blender_hsl_luminosity;
                    break;
                case AsepriteFile.BlendMode.Addition:
                    blender = BlendFuncs.rgba_blender_addition;
                    break;
                case AsepriteFile.BlendMode.Subtract:
                    blender = BlendFuncs.rgba_blender_subtract;
                    break;
                case AsepriteFile.BlendMode.Divide:
                    blender = BlendFuncs.rgba_blender_divide;
                    break;
                default:
                    throw new Exception("Unknown blend mode");
            }

            return blender.Invoke(backdrop, source, opacity);
        }
    }
}
