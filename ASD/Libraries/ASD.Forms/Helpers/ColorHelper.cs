using System;
using System.Drawing;

namespace ASD.Forms.Helpers
{
    public class ColorHelper : object
    {

        public static double BlendColor(double foreColor, double backColor, double alpha)
        {
            return Math.Min(Math.Max(backColor + alpha * (foreColor - backColor), 0.0D), 255.0D);
        }

        /// <summary>
        /// Adjust the color by lighten or darken the color
        /// </summary>
        /// <param name="color">The base color</param>
        /// <param name="gradientPercentage">The percentage of gradient. Negative to darken the color and negative to lighten the color.</param>
        /// <returns></returns>
        public static Color GradientColor(Color color, int gradientPercentage)
        {
            if (gradientPercentage == 100) return color;

            //if positive then blend with white else blend with black
            float backColor = gradientPercentage > 0 ? 255.0F : 0.0F;

            // 0 = transparent foreColor; 1 = opaque foreColor
            double alpha = 1.0F - Math.Abs(Math.Max(Math.Min(gradientPercentage, 100), -100)) / 100.0;

            byte r = (byte)BlendColor(color.R, backColor, alpha);
            byte g = (byte)BlendColor(color.G, backColor, alpha);
            byte b = (byte)BlendColor(color.B, backColor, alpha);

            return Color.FromArgb(color.A, r, g, b);
        }
    };
}