using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace BonVision.Collections
{
    public class GratingParameters
    {
        public float? Delay { get; set; }

        public float? Duration { get; set; }

        public float? Diameter { get; set; }

        public float? X { get; set; }

        public float? Y { get; set; }

        public float? Contrast { get; set; }

        public float? SpatialFrequency { get; set; }

        public float? TemporalFrequency { get; set; }

        [XmlIgnore]
        [Range(-Math.PI, Math.PI)]
        [TypeConverter(typeof(NullableDegreeConverter))]
        [Editor(DesignTypes.SliderEditor, DesignTypes.UITypeEditor)]
        public float? Orientation { get; set; }

        [Browsable(false)]
        [XmlElement(nameof(Orientation))]
        public float? OrientationXml
        {
            get { return Orientation.HasValue ? DegreeConverter.RadianToDegree(Orientation.Value) : default(float?); }
            set { this.Orientation = value.HasValue ? DegreeConverter.DegreeToRadian(value.Value) : value; }
        }

        [Browsable(false)]
        public bool DelaySpecified => Delay.HasValue;

        [Browsable(false)]
        public bool DurationSpecified => Duration.HasValue;

        [Browsable(false)]
        public bool DiameterSpecified => Diameter.HasValue;

        [Browsable(false)]
        public bool XSpecified => X.HasValue;

        [Browsable(false)]
        public bool YSpecified => Y.HasValue;

        [Browsable(false)]
        public bool ContrastSpecified => Contrast.HasValue;

        [Browsable(false)]
        public bool SpatialFrequencySpecified => SpatialFrequency.HasValue;

        [Browsable(false)]
        public bool TemporalFrequencySpecified => TemporalFrequency.HasValue;

        [Browsable(false)]
        public bool OrientationSpecified => Orientation.HasValue;

        public override string ToString()
        {
            var parameters = new List<string>();
            const string PropertySeparator = ": ";
            const string ParameterSeparator = ", ";
            if (Delay.HasValue) parameters.Add(nameof(Delay) + PropertySeparator + Delay.Value);
            if (Duration.HasValue) parameters.Add(nameof(Duration) + PropertySeparator + Duration.Value);
            if (Diameter.HasValue) parameters.Add(nameof(Diameter) + PropertySeparator + Diameter.Value);
            if (X.HasValue) parameters.Add(nameof(X) + PropertySeparator + X.Value);
            if (Y.HasValue) parameters.Add(nameof(Y) + PropertySeparator + Y.Value);
            if (Contrast.HasValue) parameters.Add(nameof(Contrast) + PropertySeparator + Contrast.Value);
            if (SpatialFrequency.HasValue) parameters.Add(nameof(SpatialFrequency) + PropertySeparator + SpatialFrequency.Value);
            if (TemporalFrequency.HasValue) parameters.Add(nameof(TemporalFrequency) + PropertySeparator + TemporalFrequency.Value);
            if (Orientation.HasValue) parameters.Add(nameof(Orientation) + PropertySeparator + DegreeConverter.RadianToDegree(Orientation.Value) + "°");
            return parameters.Count > 0 ? string.Join(ParameterSeparator, parameters) : nameof(GratingParameters);
        }
    }

    [Combinator]
    [DefaultProperty(nameof(Trials))]
    [Description("Creates a sequence of grating parameters used for stimulus presentation.")]
    [WorkflowElementCategory(ElementCategory.Source)]
    public class GratingsSpecification
    {
        private List<GratingParameters> trials = new List<GratingParameters>();

        [Description("The sequence of grating parameters used for stimulus presentation.")]
        [Editor("Bonsai.Resources.Design.CollectionEditor, Bonsai.System.Design", DesignTypes.UITypeEditor)]
        public List<GratingParameters> Trials
        {
            get { return trials; }
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
}
