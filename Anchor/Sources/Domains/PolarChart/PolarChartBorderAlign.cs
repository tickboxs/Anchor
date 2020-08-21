using System;
namespace Anchor.Domains.PolarChart
{
    public enum PolarChartBorderAlign
    {
        Center, // When 'center' is set, the borders of arcs next to each other will overlap.
        Inner   // When 'inner' is set, it is guaranteed that all the borders are not overlap.
    }
}
