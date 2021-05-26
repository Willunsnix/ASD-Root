using ASD.Forms.Base;
using ASD.Forms.Interfaces;
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

        public enum ButtonStyle
        {
            Circular = 0,
            Rectangular = 1,
            Elliptical = 2,
        }

        public enum ButtonState
        {
            Normal = 0,
            Pressed,
        }

        #endregion Enumerables

        #region Private Members

        private Timer _repeatTimer = null;

        private ButtonStyle _buttonStyle = ButtonStyle.Rectangular;
        private ButtonState _buttonState = ButtonState.Normal;
        private Color _buttonColor = Color.Red;
        private string _caption = String.Empty;
        private bool _repeatState = false;
        private int _startRepeatInterval = 500;
        private int _repeatInterval = 100;

        #endregion Private Members

        #region Constructors

        public ButtonControl()
        {
            InitializeComponent();

            //ButtonColor = Color.Red;
            Size = new Size(50, 50);

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

        [NotifyParentProperty(true)]
        [Category("Button")]
        [Description("Style of the button")]
        public ButtonStyle Style
        {
            set
            {
                _buttonStyle = value;
                CalculateDimensions();
            }
            get { return _buttonStyle; }
        }

        [Category("Button")]
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

        [Category("Button")]
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

        [Category("Button")]
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

        [Category("Button")]
        [Description("Enable/Disable the repetition of the event if the button is pressed")]
        public bool RepeatState
        {
            get { return _repeatState; }
            set { _repeatState = value; }
        }

        [Category("Button")]
        [Description("Interval to wait in ms for start the repetition")]
        public int StartRepeatInterval
        {
            get { return _startRepeatInterval; }
            set { _startRepeatInterval = value; }
        }

        [Category("Button")]
        [Description("Interva in ms for the repetition")]
        public int RepeatInterval
        {
            get { return _repeatInterval; }
            set { _repeatInterval = value; }
        }

        #endregion Properties

        #region Events

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            if (Style == ButtonControl.ButtonStyle.Circular)
            {
                Width = Math.Min(Width, Height);
                Height = Width;
            }
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