using ASD.Forms.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASD.Forms.Renderers
{
    public class BaseRenderer : IRenderer
    {
        #region Private Members

        protected object _control = null;

        #endregion Private Members

        #region Constructors

        public BaseRenderer()
        {
        }

        #endregion Constructors

        #region Properties

        public object Control
        {
            set { _control = value; }
            get { return _control; }
        }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            OnDispose();
        }

        public virtual bool Update()
        {
            return false;
        }

        public virtual void Draw(Graphics graphics)
        {
            // Check the graphics
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            // Check the control
            if (!(Control is Control ctrl))
                throw new NullReferenceException("Associated control is not valid");

            // Default drawing
            Rectangle rc = new Rectangle(0, 0, ctrl.Bounds.Width - 1, ctrl.Bounds.Height - 1);

            graphics.FillRectangle(Brushes.White, rc);
            graphics.DrawRectangle(Pens.Black, rc);
            graphics.DrawLine(Pens.Red, rc.Left, rc.Top, rc.Right, rc.Bottom);
            graphics.DrawLine(Pens.Red, rc.Right, rc.Top, rc.Left, rc.Bottom);
        }

        #endregion Methods

        #region Events

        public virtual void OnDispose()
        {
        }

        #endregion Events
    }
}