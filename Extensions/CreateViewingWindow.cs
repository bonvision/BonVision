using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenTK;

public class ViewingWindow
{
    public Matrix4 View;

    public Matrix4 Projection;
}

[Combinator]
[Description("Creates view and projection matrices for an off-center perspective camera covering the specified part of the visual field.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class CreateViewingWindow
{
    public CreateViewingWindow()
    {
        NearClip = 0.1f;
        FarClip = 100;
    }

    [Description("The angle of the left edge of the viewing window.")]
    public float Left { get; set; }

    [Description("The angle of the right edge of the viewing window.")]
    public float Right { get; set; }

    [Description("The angle of the bottom edge of the viewing window.")]
    public float Bottom { get; set; }

    [Description("The angle of the top edge of the viewing window.")]
    public float Top { get; set; }

    [Category("Z-Clipping")]
    [Description("The distance to the near clip plane.")]
    public float NearClip { get; set; }

    [Category("Z-Clipping")]
    [Description("The distance to the far clip plane.")]
    public float FarClip { get; set; }

    private static ViewingWindow Create(float left, float right, float bottom, float top, float zNear, float zFar)
    {
        // Compute central spherical coordinates of viewing window and subtract from edges
        var centerX = (right + left) / 2;
        var centerY = (top + bottom) / 2;

        // Compute relative frustum dimensions proportional to the near clip plane
        left = (float)(zNear * Math.Tan(left - centerX));
        right = (float)(zNear * Math.Tan(right - centerX));
        bottom = (float)(zNear * Math.Tan(bottom - centerY));
        top = (float)(zNear * Math.Tan(top - centerY));

        // Compute view matrix from latitude and longitude spherical coordinates of center
        // and projection matrix from relative frustum dimensions
        var latitude = Matrix4.CreateRotationX(centerY);
        var longitude = Matrix4.CreateRotationY(centerX);
        var view = latitude * longitude * Matrix4.LookAt(Vector3.Zero, -Vector3.UnitX, Vector3.UnitY);
        var projection = Matrix4.CreatePerspectiveOffCenter(left, right, bottom, top, zNear, zFar);
        return new ViewingWindow
        {
            View = view,
            Projection = projection
        };
    }

    public IObservable<ViewingWindow> Process()
    {
        return Observable.Defer(() => Observable.Return(Create(Left, Right, Bottom, Top, NearClip, FarClip)));
    }

    public IObservable<ViewingWindow> Process<TSource>(IObservable<TSource> source)
    {
        return source.Select(input => Create(Left, Right, Bottom, Top, NearClip, FarClip));
    }
}
