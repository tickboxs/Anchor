using System;
namespace Anchor.Domains.PolarChart
{
    public class PolarChartOption
    {

        public PolarChartOption() { }

        public PolarChartOption(ChartTitle title)
        {
            Title = title;
        }

        // Title for entire Chart
        public ChartTitle Title { set; get; } = new ChartTitle();

        // Default 0
        // Starting angle to draw arcs for the first item in a dataset.
        public double StartAngle { private set; get; } = 90;

        // Default true
        // If true, the chart will animate in with a rotation animation. This property is in the options.animation object.
        public bool AnimateRotate { private set; get; } = false;

        // Default 50
        // Minmial gap distance between each string of radar background web in pixel
        public double Density { private set; get; } = 50;

        // Default false
        // When true the sweep angle is not equal and is propotional to anchor.y
        public bool DynamicSweep { private set; get; } = false;
    }
}
