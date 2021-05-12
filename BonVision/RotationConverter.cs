using Bonsai;
using OpenCV.Net;
using System;
using System.ComponentModel;
using System.Globalization;

namespace BonVision
{
    class RotationConverter : NumericRecordConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var rotation = (Point3d)base.ConvertFrom(context, culture, value);
            return new Point3d(
                DegreeConverter.DegreeToRadian(rotation.X),
                DegreeConverter.DegreeToRadian(rotation.Y),
                DegreeConverter.DegreeToRadian(rotation.Z));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var rotation = (Point3d)value;
            value = new Point3d(
                DegreeConverter.RadianToDegree(rotation.X),
                DegreeConverter.RadianToDegree(rotation.Y),
                DegreeConverter.RadianToDegree(rotation.Z));
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var baseProperties = base.GetProperties(context, value, attributes);

            var converterAttribute = new TypeConverterAttribute(typeof(DegreeConverter));
            var editorAttribute = new EditorAttribute(DesignTypes.SliderEditor, DesignTypes.UITypeEditor);
            var angleAttributes = new Attribute[] { new RangeAttribute(-Math.PI, Math.PI), converterAttribute, editorAttribute };

            var properties = new PropertyDescriptor[3];
            properties[0] = new PropertyDescriptorWrapper("X", baseProperties["X"], angleAttributes);
            properties[1] = new PropertyDescriptorWrapper("Y", baseProperties["Y"], angleAttributes);
            properties[2] = new PropertyDescriptorWrapper("Z", baseProperties["Z"], angleAttributes);
            return new PropertyDescriptorCollection(properties);
        }
    }
}
