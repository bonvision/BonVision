using Bonsai;
using OpenCV.Net;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace BonVision
{
    [Combinator]
    [DisplayName("Rotation")]
    [Description("Represents a workflow property specifying a rotation in euler angle format.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class RotationProperty
    {
        Point3f value;
        event Action<Point3f> ValueChanged;

        [XmlIgnore]
        [TypeConverter(typeof(RotationConverter))]
        [Description("The value of the rotation vector, in euler angle format.")]
        public Point3f Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnValueChanged(this.value);
            }
        }

        [Browsable(false)]
        [XmlElement("Value")]
        public Point3f ValueXml
        {
            get
            {
                var x = DegreeConverter.RadianToDegree((float)value.X);
                var y = DegreeConverter.RadianToDegree((float)value.Y);
                var z = DegreeConverter.RadianToDegree((float)value.Z);
                return new Point3f(x, y, z);
            }
            set
            {
                var x = DegreeConverter.DegreeToRadian((float)value.X);
                var y = DegreeConverter.DegreeToRadian((float)value.Y);
                var z = DegreeConverter.DegreeToRadian((float)value.Z);
                this.value = new Point3f(x, y, z);
            }
        }

        void OnValueChanged(Point3f value)
        {
            ValueChanged?.Invoke(value);
        }

        public IObservable<Point3f> Process()
        {
            return Observable
                .Defer(() => Observable.Return(value))
                .Concat(Observable.FromEvent<Point3f>(
                    handler => ValueChanged += handler,
                    handler => ValueChanged -= handler));
        }

        public IObservable<Point3f> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(x => value);
        }
    }
}
