using Bonsai;
using OpenTK;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace BonVision
{
    [Description("Creates a texture shift vector.")]
    public class CreateTextureShift : Source<Vector2>
    {
        [Range(-1, 1)]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("The texture shift on the x-axis.")]
        public float X { get; set; }

        [Range(-1, 1)]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("The texture shift on the y-axis.")]
        public float Y { get; set; }

        public override IObservable<Vector2> Generate()
        {
            return Observable.Defer(() => Observable.Return(new Vector2(X, Y)));
        }

        public IObservable<Vector2> Generate<TSource>(IObservable<TSource> source)
        {
            return source.Select(input => new Vector2(X, Y));
        }
    }
}
