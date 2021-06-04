using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ASD.Drawing.Shapes
{
    public class QuadrilateralShape : IDisposable
    {
        #region Private Members

        private GraphicsPath _path = null;

        #endregion Private Members

        #region Constructors

        public QuadrilateralShape()
        {
            DrawRatio = 1.0F;
            Bounds = new RectangleF(0, 0, 0, 0);
            Radius = 0.0F;
        }

        public QuadrilateralShape(RectangleF bounds, float radius)
        {
            DrawRatio = 1.0F;
            Bounds = bounds;
            Radius = Math.Max(radius, 0.0F);
        }

        public QuadrilateralShape(RectangleF bounds, float radius, float drawRatio)
        {
            DrawRatio = drawRatio;
            Bounds = bounds;
            Radius = Math.Max(radius, 0.0F);
        }

        #endregion Constructors

        #region Properties

        public GraphicsPath Path
        {
            get
            {
                if (_path == null) Draw();
                return _path;
            }
            set { _path = value; }
        }

        public RectangleF Bounds { get; set; }
        public float DrawRatio { get; set; }

        public float Radius { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion Properties

        #region Methods

        public GraphicsPath Draw()
        {
            _path.Reset();

            //if (Radius > 0.0F)
            //{
            //    float diameter = Radius * DrawRatio * 2.0f;
            //    SizeF sizeF = new SizeF(diameter, diameter);
            //    RectangleF arc = new RectangleF(Bounds.Location, sizeF);

            //    // top left arc
            //    _path.AddArc(arc, 180, 90);
            //    // top right arc
            //    arc.X = Bounds.Right - diameter;
            //    _path.AddArc(arc, 270, 90);
            //    // bottom right  arc
            //    arc.Y = Bounds.Bottom - diameter;
            //    _path.AddArc(arc, 0, 90);
            //    // bottom left arc
            //    arc.X = Bounds.Left;
            //    _path.AddArc(arc, 90, 90);

            //    _path.CloseFigure();
            //}
            //else
            //{
            //    _path.AddRectangle(Bounds);
            //}

            _path = RoundedRect(Bounds, Radius, DrawRatio);
            return _path;
        }

        #endregion Methods

        public static GraphicsPath RoundedRect(RectangleF bounds, float radius, float drawRatio)
        {
            RectangleF baseRect = bounds;
            baseRect.X -= 20.0F;
            float diameter = radius * drawRatio * 2.0f;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new GraphicsPath();

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
            return path;
        }
    }
}