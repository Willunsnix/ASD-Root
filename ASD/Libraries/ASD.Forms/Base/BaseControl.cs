using ASD.Forms.Interfaces;
using ASD.Forms.Renderers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ASD.Forms.Base
{
    [ToolboxItem(false)]
    public partial class BaseControl : UserControl
    {
        #region Private Members

        private IRenderer _defaultRenderer = null;
        private IRenderer _renderer = null;

        #endregion Private Members

        #region Constructors

        public BaseControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor,
                     true);

            BackColor = Color.Transparent;

            _defaultRenderer = CreateDefaultRenderer();
            if (_defaultRenderer != null)
                _defaultRenderer.Control = this;
        }

        #endregion Constructors

        #region Properties

        [Browsable(false)]
        public IRenderer DefaultRenderer
        {
            get { return _defaultRenderer; }
        }

        [Browsable(false)]
        public IRenderer Renderer
        {
            get { return _renderer; }
            set
            {
                _renderer = value;

                if (_renderer != null)
                {
                    _renderer.Control = this;
                    _renderer.Update();
                }

                Invalidate();
            }
        }

        #endregion Properties

        #region Methods

        protected virtual IRenderer CreateDefaultRenderer()
        {
            return new BaseRenderer();
        }

        protected virtual void CalculateDimensions()
        {
            DefaultRenderer.Update();

            if (Renderer != null) Renderer.Update();

            Invalidate();
        }

        #endregion Methods

        #region Events

        [EditorBrowsableAttribute()]
        protected override void OnFontChanged(EventArgs e)
        {
            CalculateDimensions();
        }

        [EditorBrowsableAttribute()]
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            CalculateDimensions();
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            CalculateDimensions();
            Invalidate();
        }

        [EditorBrowsableAttribute()]
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (Renderer == null)
            {
                DefaultRenderer.Draw(e.Graphics);
                return;
            }

            Renderer.Draw(e.Graphics);
        }

        #endregion Events
    }
}