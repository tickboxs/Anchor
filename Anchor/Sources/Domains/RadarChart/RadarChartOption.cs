using System;
using Anchor.Domains;

namespace Anchor.Domains.RadarChart
{
    public class RadarChartOption
    {

        // Default 50
        // Minmial gap distance between each string of radar background web in pixel
        public double Density { private set; get; } = 50;

        public ChartTitle Title { set; get; } = new ChartTitle();
    }
}
