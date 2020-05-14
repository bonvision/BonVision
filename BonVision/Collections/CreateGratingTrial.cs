using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace BonVision.Collections
{
    public class GratingTrial
    {
        public float Id { get; set; }
        public float Delay { get; set; }
        public float Duration { get; set; }
        public float Diameter { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Contrast { get; set; }
        public float SpatialFrequency { get; set; }
        public float TemporalFrequency { get; set; }
        public float Orientation { get; set; }

        public override string ToString()
        {
            return string.Join(",",
                nameof(Id), Id,
                nameof(Delay), Delay,
                nameof(Duration), Duration,
                nameof(Diameter), Diameter,
                nameof(X), X,
                nameof(Y), Y,
                nameof(Contrast), Contrast,
                nameof(SpatialFrequency), SpatialFrequency,
                nameof(TemporalFrequency), TemporalFrequency,
                nameof(Orientation), Orientation);
        }
    }

    [Description("Creates a sequence of grating trials used for stimulus presentation.")]
    public class CreateGratingTrial : Transform<GratingParameters, GratingTrial>
    {
        public CreateGratingTrial()
        {
            Duration = 1;
            Diameter = 1;
            Contrast = 1;
            SpatialFrequency = 10;
        }

        [Description("The default pre-stimulus interval, in seconds.")]
        public float Delay { get; set; }

        [Description("The default duration of the trial, in seconds.")]
        public float Duration { get; set; }

        [Description("The default diameter of the grating stimulus, in degrees.")]
        public float Diameter { get; set; }

        [Description("The default position of the gratings along the x-axis, in degrees.")]
        public float X { get; set; }

        [Description("The default position of the gratings along the y-axis, in degrees.")]
        public float Y { get; set; }

        [Description("The default contrast of the grating stimulus, where zero is no contrast, and one is maximum contrast.")]
        public float Contrast { get; set; }

        [Description("The default spatial frequency of the gratings, in cycles per degree.")]
        public float SpatialFrequency { get; set; }

        [Description("The default sliding speed of the grating stimulus, in cycles per second.")]
        public float TemporalFrequency { get; set; }

        [XmlIgnore]
        [Range(-Math.PI, Math.PI)]
        [TypeConverter(typeof(DegreeConverter))]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        [Description("The default angle of the grating stimulus.")]
        public float Orientation { get; set; }

        [Browsable(false)]
        [XmlElement(nameof(Orientation))]
        public float OrientationXml
        {
            get { return DegreeConverter.RadianToDegree(Orientation); }
            set { Orientation = DegreeConverter.DegreeToRadian(value); }
        }

        public IObservable<GratingTrial> Process<TSource>(IObservable<TSource> source)
        {
            return source.Select((element, index) => new GratingTrial
            {
                Id = index,
                Delay = Delay,
                Duration = Duration,
                Diameter = Diameter,
                X = X,
                Y = Y,
                Contrast = Contrast,
                SpatialFrequency = SpatialFrequency,
                TemporalFrequency = TemporalFrequency,
                Orientation = Orientation 
            });
        }

        public override IObservable<GratingTrial> Process(IObservable<GratingParameters> source)
        {
            return source.Select((parameters, index) => new GratingTrial
            {
                Id = index,
                Delay = parameters.Delay.GetValueOrDefault(Delay),
                Duration = parameters.Duration.GetValueOrDefault(Duration),
                Diameter = parameters.Diameter.GetValueOrDefault(Diameter),
                X = parameters.X.GetValueOrDefault(X),
                Y = parameters.Y.GetValueOrDefault(Y),
                Contrast = parameters.Contrast.GetValueOrDefault(Contrast),
                SpatialFrequency = parameters.SpatialFrequency.GetValueOrDefault(SpatialFrequency),
                TemporalFrequency = parameters.TemporalFrequency.GetValueOrDefault(TemporalFrequency),
                Orientation = parameters.Orientation.GetValueOrDefault(Orientation)
            });
        }
    }
}
