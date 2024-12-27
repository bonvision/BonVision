using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

namespace BonVision
{
    [Combinator]
    [Description("Converts a sequence of vertices in the format (x,y,u,v,b) into a quad vertex grid.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class CreateVertexGrid
    {
        public IObservable<Matrix2x3[]> Process(IObservable<float[]> source)
        {
            return source.ToArray().Select(value =>
            {
                // The number of rows in the grid is equal to the number of distinct values of 'v'
                var rows = value.Select(row => row[3]).Distinct().Count();
                var cols = value.Length / rows;

                var i = 0;
                var result = new Matrix2x3[(rows - 1) * (cols - 1) * 4];
                for (int j = 0; j < rows - 1; j++)
                {
                    for (int k = 0; k < cols - 1; k++)
                    {
                        // Skip quads at the transition between vertical rings
                        if (k % rows == rows - 1) continue;

                        // Compute the indices for each vertex in the quad
                        var v0 = j * cols + k;
                        var v1 = j * cols + k + 1;
                        var v2 = (j + 1) * cols + k + 1;
                        var v3 = (j + 1) * cols + k;

                        // Invert y-axis in both position and tex coordinates
                        result[i++] = new Matrix2x3(value[v0][0], -value[v0][1], 0, value[v0][2], 1 - value[v0][3], value[v0][4]);
                        result[i++] = new Matrix2x3(value[v1][0], -value[v1][1], 0, value[v1][2], 1 - value[v1][3], value[v1][4]);
                        result[i++] = new Matrix2x3(value[v2][0], -value[v2][1], 0, value[v2][2], 1 - value[v2][3], value[v2][4]);
                        result[i++] = new Matrix2x3(value[v3][0], -value[v3][1], 0, value[v3][2], 1 - value[v3][3], value[v3][4]);
                    }
                }
                return result;
            });
        }
    }
}
