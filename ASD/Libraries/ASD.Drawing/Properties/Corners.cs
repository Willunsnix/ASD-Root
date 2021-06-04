using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace ASD.Drawing.Properties
{
    [TypeConverter(typeof(Converter))]
    public class Corners
    {
        #region Private Members
        private Corner _topLeft = new Corner();
        private Corner _topRight = new Corner();
        private Corner _bottomLeft = new Corner();
        private Corner _bottomRight = new Corner();

        private Corner GetCornerFromString(string cornerString)
        {
            var cornerArray = cornerString.Trim().Replace("[", "").Replace("]", "").Split(':');

            if (cornerArray.Length >= 2)
            {
                if (int.TryParse(cornerArray[0], out int angle) && int.TryParse(cornerArray[1], out int radius))
                {
                    return new Corner(angle, radius);
                }
            }
            throw new FormatException();
        }

        private bool ShouldSerializeTopLeft()
        {
            return _topLeft != new Corner();
        }

        private void ResetTopLeft()
        {
            TopLeft = new Corner();
        }

        private bool ShouldSerializeTopRight()
        {
            return _topRight != new Corner();
        }

        private void ResetTopRight()
        {
            TopRight = new Corner();
        }

        private bool ShouldSerializeBottomRight()
        {
            return _bottomRight != new Corner();
        }

        private void ResetBottomRight()
        {
            BottomRight = new Corner();
        }

        private bool ShouldSerializeBottomLeft()
        {
            return _bottomLeft != new Corner();
        }

        private void ResetBottomLeft()
        {
            BottomLeft = new Corner();
        }

        #endregion Private Members

        #region Constructors

        public Corners()
        {
            TopLeft = new Corner();
            TopRight = new Corner();
            BottomLeft = new Corner();
            BottomRight = new Corner();
        }

        public Corners(Corner topLeft, Corner topRight, Corner bottomRight, Corner bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomRight;
            BottomRight = bottomLeft;
        }

        public Corners(string allCornersText)
        {
            string[] cornerTextArray = allCornersText.Split(':');
            if (cornerTextArray.Length >= 4)
            {
                Corner[] corners = new Corner[4];

                for (int i = 0; i < 4; i++) { corners[i] = GetCornerFromString(cornerTextArray[i]); }

                TopLeft = corners[0];
                TopRight = corners[1];
                BottomLeft = corners[2];
                BottomRight = corners[3];
            }
            else
            {
                throw new FormatException();
            }
        }

        public Corners(string topLeft, string topRight, string bottomRight, string bottomLeft)
        {
            TopLeft = GetCornerFromString(topLeft);
            TopRight = GetCornerFromString(topRight);
            BottomLeft = GetCornerFromString(bottomRight);
            BottomRight = GetCornerFromString(bottomLeft);
        }

        #endregion Constructors

        #region Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        [Description("Border settings for top left corner")]
        public Corner TopLeft
        {
            get { return _topLeft; }
            set { _topLeft = value; }
        }

        //[Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        [Description("Border settings for top right corner")]
        public Corner TopRight
        {
            get { return _topRight; }
            set { _topRight = value; }
        }

        //[Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        [Description("Border settings for bottom right corner")]
        public Corner BottomRight
        {
            get { return _bottomRight; }
            set { _bottomRight = value; }
        }

        //[Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        [Description("Border settings for bottom left corner")]
        public Corner BottomLeft
        {
            get { return _bottomLeft; }
            set { _bottomLeft = value; }
        }

        #endregion Properties

        #region Methods
        public void CopyAngles(Corners source)
        {
            _topLeft.Angle = source.TopLeft.Angle;
            _topRight.Angle = source.TopRight.Angle;
            _bottomLeft.Angle = source.BottomLeft.Angle;
            _bottomRight.Angle = source.BottomRight.Angle;
        }

        public void CopyRadiuses(Corners source)
        {
            _topLeft.Radius = source.TopLeft.Radius;
            _topRight.Radius = source.TopRight.Radius;
            _bottomLeft.Radius = source.BottomLeft.Radius;
            _bottomRight.Radius = source.BottomRight.Radius;
        }

        public void ResetAngles()
        {
            _topLeft.Angle = 90;
            _topRight.Angle = 90;
            _bottomLeft.Angle = 90;
            _bottomRight.Angle = 90;
        }

        public void ResetCorners()
        {
            _topLeft = new Corner();
            _topRight = new Corner();
            _bottomLeft = new Corner();
            _bottomRight = new Corner();
        }

        public void ResetRadiuses()
        {
            _topLeft.Radius = 0;
            _topRight.Radius = 0;
            _bottomLeft.Radius = 0;
            _bottomRight.Radius = 0;
        }

        public bool ValidAngles()
        {
            return (_topLeft.Angle + _topRight.Angle + _bottomLeft.Angle + _bottomRight.Angle) == 360;
        }

        public override string ToString()
        {
            return $"{TopLeft.ToString().Trim()};{TopRight.ToString().Trim()};{BottomLeft.ToString().Trim()};{BottomRight.ToString().Trim()}";
        }

        #endregion Methods

        public partial class Converter : ExpandableObjectConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (ReferenceEquals(sourceType, typeof(string)))
                {
                    return true;
                }
                // Allow conversion from string
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (ReferenceEquals(value.GetType(), typeof(string)))
                {
                    // Conversion from string
                    var stringArray = value.ToString().Split(';');
                    bool isValid = true;
                    string message = string.Empty;

                    if (stringArray.Length >= 4)
                    {
                        Corner[] corners = new Corner[4];
                        for (int i = 0; i < 4 && isValid; i++)
                        {
                            //var cornerString = stringArray[i].Replace("[", "").Replace("]", "").Split(':');
                            var cornerString = stringArray[i].Split(':');
                            if (int.TryParse(cornerString[0], out int angle) && int.TryParse(cornerString[1], out int radius))
                            {
                                corners[i] = new Corner(angle, radius);
                            }
                            else
                            {
                                isValid = false;
                                message = "Parsing of the corner[" + i.ToString() + "] failed!";
                            }
                        }

                        if (isValid) return new Corners(corners[0], corners[1], corners[2], corners[3]);
                    }
                    else message = "Not enough parameters in string value, or wrong separator used in splitting the value!";

                    throw new FormatException(message);
                }

                return base.ConvertFrom(context, culture, value);
            }

            public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                //Alway force a new instance
                return true;

                //Original default statement
                //return base.GetCreateInstanceSupported(context);
            }

            public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
            {
                //Use the dictionary to create a new instance
                //return new Corners((Corner)propertyValues["TopLeft"], (Corner)propertyValues["TopRight"], (Corner)propertyValues["BottomRight"], (Corner)propertyValues["BottomLeft"]);

                //Original default statement
                return base.CreateInstance(context, propertyValues);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(typeof(Corners));
            }
        }
    }
}