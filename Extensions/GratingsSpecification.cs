using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

public class GratingParameters
{
    public float? Duration { get; set; }
    
    public float? Diameter { get; set; }

    public float? X { get; set; }

    public float? Y { get; set; }
    
    public float? Contrast { get; set; }
    
    public float? SpatialFrequency { get; set; }
    
    public float? TemporalFrequency { get; set; }

    public float? Orientation { get; set; }
}

[Combinator]
[Description("Creates a sequence of grating parameters used for stimulus presentation.")]
[WorkflowElementCategory(ElementCategory.Source)]
public class GratingsSpecification
{
    private List<GratingParameters> trials = new List<GratingParameters>();

    public List<GratingParameters> Trials
    {
        get { return trials;}
    }
    
    public IObservable<GratingParameters> Process()
    {
        return trials.ToObservable();
    }

    public IObservable<GratingParameters> Process<TSource>(IObservable<TSource> source)
    {
        return source.SelectMany(input => trials);
    }
}
