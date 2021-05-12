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
        Point3d value;
        event Action<Point3d> ValueChanged;

        [XmlIgnore]
        [TypeConverter(typeof(RotationConverter))]
        [Description("The value of the rotation vector, in euler angle format.")]
        public Point3d Value
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
        public Point3d ValueXml
        {
            get
            {
                var x = DegreeConverter.RadianToDegree(value.X);
                var y = DegreeConverter.RadianToDegree(value.Y);
                var z = DegreeConverter.RadianToDegree(value.Z);
                return new Point3d(x, y, z);
            }
            set
            {
                var x = DegreeConverter.DegreeToRadian(value.X);
                var y = DegreeConverter.DegreeToRadian(value.Y);
                var z = DegreeConverter.DegreeToRadian(value.Z);
                this.value = new Point3d(x, y, z);
            }
        }

        void OnValueChanged(Point3d value)
        {
            ValueChanged?.Invoke(value);
        }

        public IObservable<Point3d> Process()
        {
            return Observable
                .Defer(() => Observable.Return(value))
                .Concat(Observable.FromEvent<Point3d>(
                    handler => ValueChanged += handler,
                    handler => ValueChanged -= handler));
        }

        public IObservable<Point3d> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(x => value);
        }
    }
}
