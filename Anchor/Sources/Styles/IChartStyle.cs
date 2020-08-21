using System;
using SkiaSharp;

namespace Anchor.Styles
{
    // Define Chart Paint Style like line,bar backgroundColor
    public interface IChartStyle
    {
        SKPaint FillPaint();
        SKPaint StrokePaint();
        SKPaint FillAndStrokePaint();

    }
}
