using Bonsai;
using OpenTK;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace BonVision
{
    [Description("Creates a texture scale vector.")]
    public class CreateTextureScale : Source<Vector2>
    {
        public CreateTextureScale()
        {
            X = Y = 1;
        }

        [Range(0, 2)]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("The texture scale factor on the x-axis.")]
        public float X { get; set; }

        [Range(0, 2)]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("The texture scale factor on the y-axis.")]
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
