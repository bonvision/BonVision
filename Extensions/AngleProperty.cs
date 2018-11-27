using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

[Combinator]
[DisplayName("Angle")]
[Description("Represents a workflow property specifying a single-precision angle, in degrees.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class AngleProperty
{
    double value;
    event Action<float> ValueChanged;
    const double DegreesToRadians = Math.PI / 180;
    const double RadiansToDegrees = 180 / Math.PI;


    [Range(-180, 180)]
    [Description("The value of the angle, in degrees.")]
    [Editor(DesignTypes.SliderEditor, "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public float Value
    {
        get { return (float)(value * RadiansToDegrees); }
        set
        {
            this.value = value * DegreesToRadians;
            OnValueChanged((float)this.value);
        }
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
