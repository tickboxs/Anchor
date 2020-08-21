using System;
using Anchor.Domains;

namespace Anchor.Domains.LineChart
{
    public class LineChartOption
    {
        public LineChartOption() { }

        // Default ""
        // Title for entire Chart
        public ChartTitle Title { set; get; } = new ChartTitle();

        // Default .Category
        // .Category .Linear
        public ScaleType ScaleType { set; get; } = ScaleType.Category;

        // Default 150
        public double DensityX { private set; get; } = 150;

        // Default 150
        public double DensityY { private set; get; } = 150;
    }
}
