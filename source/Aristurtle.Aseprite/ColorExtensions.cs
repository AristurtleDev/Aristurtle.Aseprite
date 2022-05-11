using System;
using System.Drawing;
using Aristurtle.Aseprite.ThirdParty.Aseprite;

namespace Aristurtle.Aseprite
{
    public static class ColorExtensions
    {
        public static uint ToUint32(this Color color)
        {
            return DocColor.rgba(color.R, color.G, color.B, color.A);
        }

        public static Color ToArgbColor(this uint value)
        {
            byte r = DocColor.rgba_getr(value);
            byte g = DocColor.rgba_getg(value);
            byte b = DocColor.rgba_getb(value);
            byte a = DocColor.rgba_geta(value);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
