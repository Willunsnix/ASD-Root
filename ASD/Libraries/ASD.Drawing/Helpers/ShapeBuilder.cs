using ASD.Drawing.Enums;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ASD.Drawing.Helpers
{
    public class ShapeBuilder
    {
        public static GraphicsPath RoundedRect(RectangleF bounds, int radius, float drawRatio)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius > 0.0F)
            {
                RectangleF baseRect = bounds;
                float diameter = radius * drawRatio * 2.0f;
                SizeF sizeF = new SizeF(diameter, diameter);
                RectangleF arc = new RectangleF(baseRect.Location, sizeF);

                // top left arc
                path.AddArc(arc, 180, 90);
                // top right arc
                arc.X = baseRect.Right - diameter;
                path.AddArc(arc, 270, 90);
                // bottom right  arc
                arc.Y = baseRect.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                // bottom left arc
                arc.X = baseRect.Left;
                path.AddArc(arc, 90, 90);

                path.CloseFigure();
            }
            else
            {
                path.AddRectangle(bounds);
            }

            return path;
        }

        public static GraphicsPath Parallellogram(RectangleF bounds, float angle, ShapeOrientation orientation)
        {
            GraphicsPath path = new GraphicsPath();

            if (angle == 90.0F)
            {
                return RoundedRect(bounds, 0, 1.0F);
            }
            else
            {
                double sharpAngle;
                float offset; 
                
                if (orientation == ShapeOrientation.Horizontal)
                {
                    if (angle < 90.0F)
                    {
                        sharpAngle = (double)(new Decimal(angle));
                        offset = bounds.Height * (float)(Math.Sin((90.0D - sharpAngle) * (Math.PI) / 180) / Math.Sin(sharpAngle * (Math.PI) / 180));

                        path.AddLine(bounds.Left, bounds.Top, bounds.Right - offset, bounds.Top);
                        path.AddLine(bounds.Right, bounds.Bottom, bounds.Left + offset, bounds.Bottom);
                    }
                    else
                    {
                        sharpAngle = (double)(new Decimal(180.0F - angle));
                        offset = bounds.Height * (float)(Math.Sin((90.0D - sharpAngle) * (Math.PI) / 180) / Math.Sin(sharpAngle * (Math.PI) / 180));

                        path.AddLine(bounds.Left + offset, bounds.Top, bounds.Right, bounds.Top);
                        path.AddLine(bounds.Right - offset, bounds.Bottom, bounds.Left, bounds.Bottom);
                    }
                }
                else
                {
                    if (angle < 90.0F)
                    {
                        sharpAngle = (double)(new Decimal(angle));
                        offset = bounds.Width * (float)(Math.Sin((90.0D - sharpAngle) * (Math.PI) / 180) / Math.Sin(sharpAngle * (Math.PI) / 180));

                        path.AddLine(bounds.Right, bounds.Top + offset, bounds.Right, bounds.Bottom);
                        path.AddLine(bounds.Left, bounds.Bottom - offset, bounds.Left, bounds.Top);
                    }
                    else
                    {
                        sharpAngle = (double)(new Decimal(180.0F - angle));
                        offset = bounds.Width * (float)(Math.Sin((90.0D - sharpAngle) * (Math.PI) / 180) / Math.Sin(sharpAngle * (Math.PI) / 180));

                        path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Bottom - offset);
                        path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Top + offset);
                    }
                }


                path.CloseFigure();
                //path.AddString(offset.ToString(),new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif), (int)FontStyle.Regular,20.0F, bounds,StringFormat.GenericDefault);
            }

            //double d = orientation == ShapeOrientation.Horizontal ? bounds.Height : bounds.Width;


            return path;
        }

        public static GraphicsPath Parallellogram2(RectangleF bounds, float angle, ShapeOrientation orientation)
        {
            GraphicsPath path = new GraphicsPath();

            double d = orientation == ShapeOrientation.Horizontal ? bounds.Height : bounds.Width;

            double sharpAngle = angle <= 90.0F ? (double)(new Decimal(angle)) : (double)(new Decimal(180.0F - angle));
            double s = d / Math.Sin(sharpAngle);
            float offset = (float)(s * Math.Sin(90.0D - sharpAngle));

            if (orientation == ShapeOrientation.Horizontal)
            {
                if (angle < 90.0F)
                {
                    path.AddLine(bounds.Left, bounds.Top, bounds.Right - offset, bounds.Top);
                    path.AddLine(bounds.Right, bounds.Bottom, bounds.Left + offset, bounds.Bottom);
                }
                else
                {
                    path.AddLine(bounds.Left + offset, bounds.Top, bounds.Right, bounds.Top);
                    path.AddLine(bounds.Right - offset, bounds.Bottom, bounds.Left, bounds.Bottom);
                }
            }
            else
            {
                if (angle < 90.0F)
                {
                    path.AddLine(bounds.Left, bounds.Top, bounds.Right, bounds.Top + offset);
                    path.AddLine(bounds.Right, bounds.Bottom, bounds.Left, bounds.Bottom - offset);
                }
                else
                {
                    path.AddLine(bounds.Left, bounds.Top + offset, bounds.Right, bounds.Top);
                    path.AddLine(bounds.Right, bounds.Bottom - offset, bounds.Left, bounds.Bottom);
                }
            }
            path.CloseFigure();

            return path;
        }
    }
}