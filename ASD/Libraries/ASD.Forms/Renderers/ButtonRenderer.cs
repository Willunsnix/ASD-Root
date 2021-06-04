using ASD.Drawing.Enums;
using ASD.Drawing.Helpers;
using ASD.Forms.Controls;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ASD.Forms.Renderers
{
    /// <summary>
    /// Base class for the button renderers
    /// </summary>
    public class ButtonRenderer : BaseRenderer
    {
        #region Protected Memebrs

        protected RectangleF rectCtrl;
        protected RectangleF rectBody;
        protected RectangleF rectText;
        protected float drawRatio = 1.0F;

        #endregion Protected Memebrs

        #region Constructors

        public ButtonRenderer()
        {
            rectCtrl = new RectangleF(0, 0, 0, 0);
        }

        #endregion Constructors

        #region Properies

        /// <summary>
        /// Get the associated button object
        /// </summary>
        public ButtonControl Button
        {
            get { return Control as ButtonControl; }
        }

        #endregion Properies

        #region Overrided Methods

        /// <summary>
        /// Update the rectangles for drawing
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            // Check Button object
            if (Button == null)
                throw new NullReferenceException("Invalid 'Button' object");

            // Control rectangle
            rectCtrl.X = 0;
            rectCtrl.Y = 0;
            rectCtrl.Width = Button.Width;
            rectCtrl.Height = Button.Height;

            if (Button.Style == ShapenStyle.Circular)
            {
                if (rectCtrl.Width < rectCtrl.Height)
                    rectCtrl.Height = rectCtrl.Width;
                else if (rectCtrl.Width > rectCtrl.Height)
                    rectCtrl.Width = rectCtrl.Height;

                if (rectCtrl.Width < 10)
                    rectCtrl.Width = 10;
                if (rectCtrl.Height < 10)
                    rectCtrl.Height = 10;
            }

            rectBody = rectCtrl;
            rectBody.Width -= 1;
            rectBody.Height -= 1;

            rectText = rectCtrl;
            rectText.Width -= 2;
            rectText.Height -= 2;

            // Calculate ratio
            drawRatio = Math.Min(rectCtrl.Width, rectCtrl.Height) / 200;
            if (drawRatio == 0.0)
                drawRatio = 1;

            return true;
        }

        /// <summary>
        /// Draw the button object
        /// </summary>
        /// <param name="Gr"></param>
        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
                throw new ArgumentNullException("Gr", "Invalid Graphics object");

            if (Button == null)
                throw new NullReferenceException("Invalid 'Button' object");

            DrawBackground(Gr, rectCtrl);
            DrawBody(Gr, rectBody);
            DrawText(Gr, rectText);
        }

        #endregion Overrided Methods

        #region Virtual Methods

        /// <summary>
        /// Draw the background of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBackground(Graphics Gr, RectangleF rc)
        {
            if (Button == null)
                return false;

            Color c = Button.BackColor;
            SolidBrush br = new SolidBrush(c);
            Pen pen = new Pen(c);

            Rectangle _rcTmp = new Rectangle(0, 0, Button.Width, Button.Height);
            Gr.DrawRectangle(pen, _rcTmp);
            Gr.FillRectangle(br, rc);

            br.Dispose();
            pen.Dispose();

            return true;
        }

        /// <summary>
        /// Draw the body of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawBody(Graphics Gr, RectangleF rc)
        {
            if (Button == null)
                return false;

            Color bodyColor = Button.ButtonColor;
            Color cDark = ColorHelper.GradientColor(bodyColor, -80);

            LinearGradientBrush br1 = new LinearGradientBrush(rc,
                                                               bodyColor,
                                                               cDark,
                                                               45);
            GraphicsPath path = new GraphicsPath();
            switch (Button.Style)
            {
                case ShapenStyle.Circular:
                case ShapenStyle.Elliptical:
                    Gr.FillEllipse(br1, rc);
                    break;

                case ShapenStyle.Parallellogram:
                    if (Button.Corners.ValidAngles())
                    {
                        path = ShapeBuilder.Parallellogram(rc, Button.Corners.TopLeft.Angle, Button.Orientation);
                        Gr.FillPath(br1, path);
                    }
                    else
                        throw new ArgumentOutOfRangeException("Corners", "The sum of all angles should be exactly 360 degrees!");
                    break;

                case ShapenStyle.Rectangular:
                default:
                    if (Button.Corners.ValidAngles())
                    {
                        //path = ShapeBuilder.RoundedRect(rc, 15.0F, drawRatio);
                        path = ShapeBuilder.RoundedRect(rc, Button.Corners.TopLeft.Radius, drawRatio);
                        Gr.FillPath(br1, path);
                    }
                    else
                        throw new ArgumentOutOfRangeException("Corners", "The sum of all angles should be exactly 360 degrees!");
                    break;
            }

            if (Button.State == ButtonControl.ButtonState.Pressed)
            {
                RectangleF _rc = rc;
                _rc.Inflate(-15F * drawRatio, -15F * drawRatio);
                LinearGradientBrush br2 = new LinearGradientBrush(_rc,
                                                                   cDark,
                                                                   bodyColor,
                                                                   45);

                switch (Button.Style)
                {
                    case ShapenStyle.Circular:
                    case ShapenStyle.Elliptical:
                        Gr.FillEllipse(br2, _rc);
                        break;

                    case ShapenStyle.Parallellogram:
                        path.Reset();
                        path = ShapeBuilder.Parallellogram(_rc, Button.Corners.TopLeft.Angle, Button.Orientation);
                        Gr.FillPath(br2, path);
                        break;

                    case ShapenStyle.Rectangular:
                    default:
                        path.Reset();
                        path = ShapeBuilder.RoundedRect(_rc, Button.Corners.TopLeft.Radius - 5, drawRatio);
                        Gr.FillPath(br2, path);
                        break;
                }
                br2.Dispose();
            }

            path.Dispose();
            br1.Dispose();
            return true;
        }

        /// <summary>
        /// Draw the text of the control
        /// </summary>
        /// <param name="Gr"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public virtual bool DrawText(Graphics Gr, RectangleF rc)
        {
            if (Button == null)
                return false;

            //Draw Strings
            //Font font = new Font(Button.Font.FontFamily, Button.Font.Size * drawRatio, Button.Font.Style);
            Font font = new Font(Button.Font.FontFamily, Button.Font.Size, Button.Font.Style);

            string str = Button.Caption;

            Color bodyColor = Button.ButtonColor;
            Color cDark = ColorHelper.GradientColor(bodyColor, -80);

            SizeF size = Gr.MeasureString(str, font);

            SolidBrush br1 = new SolidBrush(bodyColor);
            SolidBrush br2 = new SolidBrush(cDark);

            Gr.DrawString(str,
                            font,
                            br1,
                            rc.Left + (rc.Width * 0.5F - (float)(size.Width * 0.5F)) + (float)(1 * drawRatio),
                            rc.Top + (rc.Height * 0.5F - (float)(size.Height * 0.5)) + (float)(1 * drawRatio));

            Gr.DrawString(str,
                            font,
                            br2,
                            rc.Left + (rc.Width * 0.5F - (float)(size.Width * 0.5F)),
                            rc.Top + (rc.Height * 0.5F - (float)(size.Height * 0.5)));

            br1.Dispose();
            br2.Dispose();
            font.Dispose();

            return false;
        }

        #endregion Virtual Methods
    }
}