using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

namespace BonVision
{
    [Combinator]
    [Description("Creates a UV-mapped spherical vertex grid, with the specified degree boundaries.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class CreateSphereGrid
    {
        public CreateSphereGrid()
        {
            Left = -180;
            Right = 180;
            Bottom = -90;
            Top = 90;
        }

        [Description("The left edge of the sphere grid, in degrees.")]
        public float Left { get; set; }

        [Description("The right edge of the sphere grid, in degrees.")]
        public float Right { get; set; }

        [Description("The bottom edge of the sphere grid, in degrees.")]
        public float Bottom { get; set; }

        [Description("The top edge of the sphere grid, in degrees.")]
        public float Top { get; set; }

        Tuple<Matrix2x3[], int[]> CreateMeshData()
        {
            const int Rings = 180;
            const int Segments = 360;
            const float LatitudeStep = MathHelper.Pi / Rings;
            const float LongitudeStep = MathHelper.TwoPi / Segments;
            var latitudeMin = MathHelper.Clamp((int)Math.Min(Top, Bottom) + Rings / 2, 0, Rings);
            var latitudeMax = MathHelper.Clamp((int)Math.Max(Top, Bottom) + Rings / 2, 0, Rings);
            var longitudeMin = MathHelper.Clamp((int)Math.Min(Left, Right) + Segments / 2, 0, Segments);
            var longitudeMax = MathHelper.Clamp((int)Math.Max(Left, Right) + Segments / 2, 0, Segments);

            var vid = 0;
            var iid = 0;
            var vertices = new Matrix2x3[(Rings - 1) * (Segments + 1) + 2 * Segments];
            var indices = new int[3 * (2 * (Rings - 2) * Segments + 2 * Segments)];
            for (int i = 0; i <= Rings; i++)
            {
                var pole = i == 0 || i == Rings;
                var latitude = (Rings - i) * LatitudeStep;

                for (int j = 0; j <= Segments; j++)
                {
                    if (pole && j == Segments) break;
                    var longitude = j * LongitudeStep + MathHelper.PiOver2;

                    var x = (float)(Math.Sin(latitude) * Math.Cos(longitude));
                    var y = (float)(Math.Cos(latitude));
                    var z = (float)(Math.Sin(latitude) * Math.Sin(longitude));
                    var u = (j - longitudeMin) / (float)(longitudeMax - longitudeMin);
                    var v = (i - latitudeMin) / (float)(latitudeMax - latitudeMin);
                    vertices[vid++] = new Matrix2x3(x, y, z, u, v, 1);
                    if (i <= latitudeMin || i > latitudeMax || j < longitudeMin || j >= longitudeMax)
                    {
                        // clamp spherical grid
                        continue;
                    }

                    // generate north pole faces
                    if (i == 1 && j > 0)
                    {
                        indices[iid++] = j - 1;
                        indices[iid++] = Segments + (j - 1);
                        indices[iid++] = Segments + j;
                    }

                    // generate equatorial faces
                    if (i > 1 && !pole && j < Segments)
                    {
                        indices[iid++] = vid - Segments - 1;
                        indices[iid++] = vid - Segments - 2;
                        indices[iid++] = vid - 1;

                        indices[iid++] = vid - 1;
                        indices[iid++] = vid;
                        indices[iid++] = vid - Segments - 1;
                    }

                    // generate south pole faces
                    if (i == Rings - 1 && j > 0)
                    {
                        indices[iid++] = vertices.Length - 2 * Segments + (j - 2);
                        indices[iid++] = vertices.Length - Segments + (j - 1);
                        indices[iid++] = vertices.Length - 2 * Segments + (j - 1);
                    }
                }
            }

            return Tuple.Create(vertices, indices);
        }

        public IObservable<Tuple<Matrix2x3[], int[]>> Process()
        {
            return Observable.Defer(() => Observable.Return(CreateMeshData()));
        }

        public IObservable<Tuple<Matrix2x3[], int[]>> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select(input => CreateMeshData());
        }
    }
}
