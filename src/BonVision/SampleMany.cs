using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.Distributions;

namespace BonVision
{
    [Obsolete]
    [Combinator]
    [Description("Draws multiple random samples from the input distribution.")]
    [WorkflowElementCategory(ElementCategory.Combinator)]
    public class SampleMany
    {
        public SampleMany()
        {
            Count = 1;
        }

        [Description("The number of random samples to draw.")]
        public int Count { get; set; }

        public IObservable<int> Process(IObservable<IDiscreteDistribution> source)
        {
            return source.SelectMany(distribution => distribution.Samples().Take(Count));
        }

        public IObservable<double> Process(IObservable<IContinuousDistribution> source)
        {
            return source.SelectMany(distribution => distribution.Samples().Take(Count));
        }
    }
}
