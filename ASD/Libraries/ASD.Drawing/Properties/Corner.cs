using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace ASD.Drawing.Properties
{
    [TypeConverter(typeof(CornerConverter))]
    public class Corner : IEquatable<Corner>
    {
        #region Private Members

        private const int DEFAULT_ANGLE = 90;
        private const int DEFAULT_RADIUS = 0;

        private int _angle = DEFAULT_ANGLE;
        private int _radius = DEFAULT_RADIUS;

        private bool ShouldSerializeAngle()
        {
            return _angle != DEFAULT_ANGLE;
        }

        private void ResetAngle()
        {
            Angle = DEFAULT_ANGLE;
        }

        private bool ShouldSerializeRadius()
        {
            return _radius != DEFAULT_RADIUS;
        }

        private void ResetRadius()
        {
            Radius = DEFAULT_RADIUS;
        }

        #endregion Private Members

        #region Constructors

        public Corner()
        {
            Angle = DEFAULT_RADIUS;
            Radius = 0;
        }

        public Corner(int angle, int radius)
        {
            Angle = angle > 0 ? angle : DEFAULT_RADIUS;
            Radius = radius > 0 ? radius : 0;
        }

        #endregion Constructors

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        //[DefaultValue(90)]
        [Description("Angle of the corner.")]
        public int Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [NotifyParentProperty(true)]
        //[DefaultValue(0)]
        [Description("Radius of the corner")]
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        #endregion Properties

        #region Methods

        public override bool Equals(object obj) => this.Equals(obj as Corner);

        public bool Equals(Corner p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (Angle == p.Angle) && (Radius == p.Radius);
        }

        public override int GetHashCode() => (Angle, Radius).GetHashCode();

        public static bool operator ==(Corner lhs, Corner rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Corner lhs, Corner rhs) => !(lhs == rhs);

        public override string ToString()
        {
            return $"[{Angle.ToString().Trim()}:{Radius.ToString().Trim()}]";
        }

        #endregion Methods

        public partial class CornerConverter : ExpandableObjectConverter
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
                    // Conversion from
                    var stringArray = value.ToString().Replace("[", "").Replace("]", "").Split(':');
                    //var stringArray = value.ToString().Split(':');

                    if (stringArray.Length >= 2)
                    {
                        if (int.TryParse(stringArray[0], out int angle) && int.TryParse(stringArray[1], out int radius))
                        {
                            return new Corner(angle, radius);
                        }
                    }

                    throw new FormatException();
                }

                return base.ConvertFrom(context, culture, value);
            }

            // Overrides the ConvertTo method of TypeConverter.
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    //return ((Corner)value).Angle + ":" + ((Corner)value).Radius;
                    return $"[{((Corner)value).Angle}:{((Corner)value).Radius}]";
                }
                return base.ConvertTo(context, culture, value, destinationType);
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
                return new Corner((int)propertyValues["Angle"], (int)propertyValues["Radius"]);

                //Original default statement
                //return base.CreateInstance(context, propertyValues);
            }

            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                return TypeDescriptor.GetProperties(typeof(Corner));
            }
        }
    }
}