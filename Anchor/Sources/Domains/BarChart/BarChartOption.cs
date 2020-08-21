using System;
using Anchor.Domains;

namespace Anchor.Domains.BarChart
{
    public class BarChartOption
    {
        public BarChartOption()
        {

        }

        // Default 100
        // Minmial gap distance between each major line
        public double Density { private set; get; } = 50;

        public ChartTitle Title { private set; get; } = new ChartTitle();

        // Default 150
        public double DensityY { private set; get; } = 150;
    }
}
