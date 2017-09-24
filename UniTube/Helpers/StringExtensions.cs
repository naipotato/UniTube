using System;
using System.Globalization;

using Windows.UI;

namespace UniTube.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static Color ToColor(this string value)
        {
            if (value.StartsWith("#"))
            {
                value = value.Substring(1);
            }

            if (value.Length == 8)
            {
                return Color.FromArgb(
                    byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber),
                    byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber),
                    byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber),
                    byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber));
            }
            else if (value.Length == 6)
            {
                return Color.FromArgb(255,
                    byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber),
                    byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber),
                    byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber));
            }
            else
            {
                throw new Exception("Color not valid.");
            }
        }
    }
}
