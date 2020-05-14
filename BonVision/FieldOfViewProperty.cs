using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace BonVision
{
    [Combinator]
    [DisplayName("FieldOfView")]
    [Description("Represents a workflow property specifying a camera field of view angle.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class FieldOfViewProperty
    {
        float value;
        event Action<float> ValueChanged;

        public FieldOfViewProperty()
        {
            Value = 90;
        }

        [XmlIgnore]
        [Range(0.00174532924, 3.13984728)]
        [TypeConverter(typeof(DegreeConverter))]
        [Description("The value of the camera field of view.")]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
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
