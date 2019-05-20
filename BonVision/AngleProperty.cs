using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace BonVision
{
    [Combinator]
    [DisplayName("Angle")]
    [Description("Represents a workflow property specifying a single-precision angle.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class AngleProperty
    {
        float value;
        event Action<float> ValueChanged;

        [XmlIgnore]
        [Range(-Math.PI, Math.PI)]
        [TypeConverter(typeof(DegreeConverter))]
        [Description("The value of the angle.")]
        [Editor(DesignTypes.SliderEditor, "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public float Value
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
        public float ValueXml
        {
            get { return DegreeConverter.RadianToDegree(value); }
            set { this.value = DegreeConverter.DegreeToRadian(value); }
        }

        void OnValueChanged(float value)
        {
            var handler = ValueChanged;
            if (handler != null)
            {
                handler(value);
            }
        }

        public IObservable<float> Process()
        {
            return Observable
                .Defer(() => Observable.Return((float)value))
                .Concat(Observable.FromEvent<float>(
                    handler => ValueChanged += handler,
                    handler => ValueChanged -= handler));
        }

        public IObservable<float> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(x => (float)value);
        }
    }
}
