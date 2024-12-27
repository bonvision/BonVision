using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.Distributions;
using MathNet.Numerics;

namespace BonVision
{
    [Combinator]
    [Description("Generates a non-overlapping distribution of values in a grid.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class CreateSparseNoiseGrid
    {
        [Description("The number of rows in the sparse noise grid.")]
        public int Rows { get; set; }

        [Description("The number of columns in the sparse noise grid.")]
        public int Columns { get; set; }

        [Description("The number of active quads in the generated grid.")]
        public int ActiveQuads { get; set; }

        public IObservable<byte[]> Process<TSource>(IObservable<TSource> source)
        {
            return Process(source, Observable.Return(new Random()));
        }

        public IObservable<byte[]> Process<TSource>(IObservable<TSource> source, IObservable<Random> randomSource)
        {
            var distributionSource = randomSource.Select(random => new DiscreteUniform(0, 1, random));
            return source.CombineLatest(
                distributionSource,
                (input, distribution) =>
                {
                    var result = new byte[Rows * Columns];
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (i < ActiveQuads) result[i] = (byte)(distribution.Sample() * byte.MaxValue);
                        else result[i] = 128;
                    }

                    Combinatorics.SelectPermutationInplace(result, distribution.RandomSource);
                    return result;
                });
        }
    }
}
