using System;
using System.ComponentModel;
using System.Globalization;

namespace BonVision
{
    class DegreeConverter : SingleConverter
    {
        public static double RadianToDegree(double value)
        {
            return value * (180.0 / Math.PI);
        }

        public static double DegreeToRadian(double value)
        {
            return value * (Math.PI / 180.0);
        }

        public static float RadianToDegree(float value)
        {
            return (float)RadianToDegree((double)value);
        }

        public static float DegreeToRadian(float value)
        {
            return (float)DegreeToRadian((double)value);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                value = ((string)value).TrimEnd('°');
            }

            value = base.ConvertFrom(context, culture, value);
            return DegreeToRadian((float)value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            value = RadianToDegree(Convert.ToSingle(value));
            var result = base.ConvertTo(context, culture, value, destinationType);
            if (destinationType == typeof(string))
            {
                return (string)result + "°";
            }

            return result;
        }
    }

    class NullableDegreeConverter : NullableConverter
    {
        readonly TypeConverter degreeConverter;

        public NullableDegreeConverter(Type type)
            : base(type)
        {
            degreeConverter = new DegreeConverter();
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (string.IsNullOrEmpty((string)value)) return base.ConvertFrom(context, culture, value);
            else return degreeConverter.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value != null) return degreeConverter.ConvertTo(context, culture, value, destinationType);
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
