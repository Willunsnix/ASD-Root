using ASD.Drawing.Enums;
using ASD.Drawing.Properties;
using ASD.Forms.Base;
using ASD.Forms.Interfaces;
using ASD.Global.Helpers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ASD.Forms.Controls
{
    [ToolboxItem(true)]
    public partial class ButtonControl : BaseControl
    {
        #region Enumerables

        public enum ButtonState
        {
            Normal = 0,
            Pressed = 1
        }

        #endregion Enumerables

        #region Private Members

        private ButtonState _buttonState = ButtonState.Normal;
        private Color _buttonColor = Color.Red;
        private string _caption = String.Empty;

        private Timer _repeatTimer = null;
        private bool _repeatState = false;
        private int _startRepeatInterval = 500;
        private int _repeatInterval = 100;

        //Shape properties
        private ShapenStyle _buttonStyle = ShapenStyle.Rectangular;

        private Corners _corners = new Corners();
        private ShapeOrientation _orientation = ShapeOrientation.Horizontal;

        private void SetPropertyAttributes()
        {
            switch (_buttonStyle)
            {
                case ShapenStyle.Circular:
                case ShapenStyle.Elliptical:
                    PropertyHandler.SetPropertyVisibility(this, "Corners", false);
                    PropertyHandler.SetPropertyVisibility(this, "Orientation", false);
                    break;

                case ShapenStyle.Parallellogram:
                    PropertyHandler.SetPropertyVisibility(this, "Corners", true);
                    PropertyHandler.SetPropertyVisibility(this, "Orientation", true);

                    PropertyHandler.SetPropertyVisibilityByPath(this, "Corners.TopLeft", false);
                    break;

                case ShapenStyle.Rectangular:
                default:
                    PropertyHandler.SetPropertyVisibility(this, "Corners", true);
                    PropertyHandler.SetPropertyVisibility(this, "Orientation", false);

                    PropertyHandler.SetPropertyVisibilityByPath(this, "Corners.TopLeft", true);
                    break;
            }
        }

        private void SetTheCorners(Corners value)
        {
            switch (_buttonStyle)
            {
                case ShapenStyle.Parallellogram:
                    _corners.CopyRadiuses(value);

                    if (_corners.TopRight.Angle != value.TopLeft.Angle)
                    {
                        _corners.BottomRight.Angle = _corners.TopLeft.Angle;
                        _corners.TopRight.Angle = 180 - _corners.TopLeft.Angle;
                        _corners.BottomLeft.Angle = _corners.TopRight.Angle;
                        Caption = "Par: TopLeft";
                    }
                    else if (_corners.TopRight.Angle != value.TopRight.Angle)
                    {
                        _corners.TopLeft.Angle = 180 - _corners.TopRight.Angle;
                        _corners.BottomRight.Angle = _corners.TopLeft.Angle;
                        _corners.BottomLeft.Angle = _corners.TopRight.Angle;
                        Caption = "Par: TopRight";
                    }
                    else if (_corners.BottomRight.Angle != value.BottomRight.Angle)
                    {
                        _corners.TopLeft.Angle = _corners.BottomRight.Angle;
                        _corners.TopRight.Angle = 180 - _corners.TopLeft.Angle;
                        _corners.BottomLeft.Angle = _corners.TopRight.Angle;
                        Caption = "Par: BottomRight";
                    }
                    else
                    {
                        _corners.TopLeft.Angle = 180 - _corners.BottomLeft.Angle;
                        _corners.BottomRight.Angle = _corners.TopLeft.Angle;
                        _corners.TopRight.Angle = _corners.BottomLeft.Angle;
                        Caption = "Par: BottomLeft";
                    }

                    break;

                case ShapenStyle.Rectangular:
                    _corners = value;
                    _corners.ResetAngles();
                    Caption = "Rectangle";
                    break;

                default:
                    _corners = value;
                    break;
            }
        }

        #endregion Private Members

        #region Constructors

        public ButtonControl()
        {
            InitializeComponent();

            //ButtonColor = Color.Red;
            Size = new Size(50, 50);
            SetPropertyAttributes();

            _repeatTimer = new Timer
            {
                Enabled = false,
                Interval = _startRepeatInterval
            };
            _repeatTimer.Tick += OnTimerTick;
        }

        #endregion Constructors

        #region Methods

        protected override IRenderer CreateDefaultRenderer()
        {
            return new Renderers.ButtonRenderer();
        }

        #endregion Methods

        #region Properties

        [RefreshProperties(RefreshProperties.All)]
        [Category("Appearance")]
        [Description("Style of the button")]
        public ShapenStyle Style
        {
            get { return _buttonStyle; }
            set
            {
                _buttonStyle = value;

                CalculateDimensions();

                SetPropertyAttributes();
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        //[ReadOnly(false)]
        [Category("Appearance")]
        [Description("Corners of the shape. For regular shapes the topleft angle is the key angle from which most calculations take place.")]
        public Corners Corners
        {
            get { return _corners; }
            set { SetTheCorners(value); Invalidate(); }
        }

        [Browsable(false)]
        [Category("Appearance")]
        [Description("Orientation of the button shape")]
        public ShapeOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Description("Color of the body of the button")]
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set
            {
                _buttonColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("Caption of the button")]
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("State of the button")]
        public ButtonState State
        {
            get { return _buttonState; }
            set
            {
                _buttonState = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("Enable/Disable the repetition of the event if the button is pressed")]
        public bool RepeatState
        {
            get { return _repeatState; }
            set { _repeatState = value; }
        }

        [Category("Behavior")]
        [Description("Interval to wait in ms for start the repetition")]
        public int StartRepeatInterval
        {
            get { return _startRepeatInterval; }
            set { _startRepeatInterval = value; }
        }

        [Category("Behavior")]
        [Description("Interva in ms for the repetition")]
        public int RepeatInterval
        {
            get { return _repeatInterval; }
            set { _repeatInterval = value; }
        }

        [Browsable(false)]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        [Browsable(false)]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout { get => base.BackgroundImageLayout; set => base.BackgroundImageLayout = value; }

        #endregion Properties

        #region Events

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            if (Style == ShapenStyle.Circular)
            {
                Width = Math.Min(Width, Height);
                Height = Width;
            }
            //PropertyHandler.SetPropertyVisibility(this, "BorderStyle", false);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            _repeatTimer.Enabled = false;

            // Update the interval
            if (_repeatTimer.Interval == _startRepeatInterval)
                _repeatTimer.Interval = _repeatInterval;

            // Call the delagate
            ButtonEventArgs ev = new ButtonEventArgs
            {
                State = State
            };
            OnButtonRepeatState(ev);

            _repeatTimer.Enabled = true;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Change the state
            State = ButtonState.Pressed;
            Invalidate();

            // Call the delagates
            ButtonEventArgs ev = new ButtonEventArgs
            {
                State = State
            };
            OnButtonChangeState(ev);

            // Enable the repeat timer
            if (RepeatState != false)
            {
                _repeatTimer.Interval = StartRepeatInterval;
                _repeatTimer.Enabled = true;
            }
        }

        private void OnMuoseUp(object sender, MouseEventArgs e)
        {
            // Change the state
            State = ButtonState.Normal;
            Invalidate();

            // Call the delagates
            ButtonEventArgs ev = new ButtonEventArgs
            {
                State = State
            };
            OnButtonChangeState(ev);

            // Disable the timer
            _repeatTimer.Enabled = false;
        }

        #endregion Events

        #region Delegate Events

        public event ButtonChangeState ButtonChangeState;

        public event ButtonRepeatState ButtonRepeatState;

        protected virtual void OnButtonChangeState(ButtonEventArgs e)
        {
            ButtonChangeState?.Invoke(this, e);
        }

        protected virtual void OnButtonRepeatState(ButtonEventArgs e)
        {
            ButtonRepeatState?.Invoke(this, e);
        }

        #endregion Delegate Events
    }

    #region Delegates

    public delegate void ButtonChangeState(object sender, ButtonEventArgs e);

    public delegate void ButtonRepeatState(object sender, ButtonEventArgs e);

    #endregion Delegates

    public class ButtonEventArgs : EventArgs
    {
        public ButtonEventArgs()
        {
        }

        public ButtonControl.ButtonState State { get; set; }
    }
}