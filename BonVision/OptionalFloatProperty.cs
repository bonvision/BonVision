using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace BonVision
{
    [Combinator]
    [DisplayName("OptionalFloat")]
    [Description("Represents a workflow property specifying an optional single-precision floating-point number.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class OptionalFloatProperty
    {
        float? value;
        event Action<float?> ValueChanged;

        [Range(0, 1)]
        [Description("The optional value of the property.")]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        public float? Value
        {
            get { return value; }
            set
            {
                this.value = value;
                ValueChanged?.Invoke(this.value);
            }
        }

        public bool HasValue
        {
            get { return value.HasValue; }
            set
            {
                if (value)
                {
                    if (!this.value.HasValue)
                    {
                        this.value = 0.5f;
                    }
                }
                else this.value = null;
            }
        }

        public IObservable<float?> Process()
        {
            return Observable
                .Defer(() => Observable.Return(value))
                .Concat(Observable.FromEvent<float?>(
                    handler => ValueChanged += handler,
                    handler => ValueChanged -= handler));
        }

        public IObservable<float?> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(x => value);
        }
    }
}
