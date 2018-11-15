using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

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
        var rings = 180;
        var segments = 360;
        var latitudeStep = MathHelper.Pi / rings;
        var longitudeStep = MathHelper.TwoPi / segments;
        var latitudeMin = MathHelper.Clamp((int)Math.Min(Top, Bottom) + rings / 2, 0, rings);
        var latitudeMax = MathHelper.Clamp((int)Math.Max(Top, Bottom) + rings / 2, 0, rings);
        var longitudeMin = MathHelper.Clamp((int)Math.Min(Left, Right) + segments / 2, 0, segments);
        var longitudeMax = MathHelper.Clamp((int)Math.Max(Left, Right) + segments / 2, 0, segments);

        var vid = 0;
        var iid = 0;
        var vertices = new Matrix2x3[(rings - 1) * (segments + 1) + 2 * segments];
        var indices = new int[3 * (2 * (rings - 2) * segments + 2 * segments)];
        for (int i = 0; i <= rings; i++)
        {
            var pole = i == 0 || i == rings;
            var latitude = i * latitudeStep;

            for (int j = 0; j <= segments; j++)
            {
                if (pole && j == segments) break;
                var longitude = j * longitudeStep;

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
                    indices[iid++] = segments + (j - 1);
                    indices[iid++] = segments + j;
                }

                // generate equatorial faces
                if (i > 1 && !pole && j < segments)
                {
                    indices[iid++] = vid - segments - 1;
                    indices[iid++] = vid - segments - 2;
                    indices[iid++] = vid - 1;

                    indices[iid++] = vid - 1;
                    indices[iid++] = vid;
                    indices[iid++] = vid - segments - 1;
                }

                // generate south pole faces
                if (i == rings - 1 && j > 0)
                {
                    indices[iid++] = vertices.Length - 2 * segments + (j - 2);
                    indices[iid++] = vertices.Length - segments + (j - 1);
                    indices[iid++] = vertices.Length - 2 * segments + (j - 1);
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
