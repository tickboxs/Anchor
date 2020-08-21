using System;
using SkiaSharp;

namespace Anchor.Styles
{
    // Define Point(Shape) style
    public interface IChartPointStyle
    {
        SKPaint PointFillPaint();
        SKPaint PointStrokePaint();
        SKPaint PointFillAndStrokePaint();
    }
}
