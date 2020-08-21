using System;
using Anchor.Domains;
namespace Anchor.Domains.ScatterChart
{
    public class ScatterChartOption
    {
        public ScatterChartOption() { }

        public ScatterChartOption(ChartTitle title)
        {
            Title = title;
        }

        // Default ""
        // Title for entire Chart
        public ChartTitle Title { set; get; } = new ChartTitle();

        // Default 150
        public double DensityX { private set; get; } = 150;

        // Default 150
        public double DensityY { private set; get; } = 150;
    }
}
