using System;
using Anchor.Domains.BarChart;
using SkiaSharp;

namespace Anchor.Styles
{
    public class BarChartStyle:IChartStyle
    {
        public BarChartStyle(BarChartDataset dataset)
        {
            Dataset = dataset;
        }

        public BarChartDataset Dataset { private set; get; }

        public SKPaint FillAndStrokePaint()
        {
            throw new NotImplementedException();
        }

        public SKPaint FillPaint()
        {
            return new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Dataset.BackgroundColor
            };
        }

        public SKPaint StrokePaint()
        {
            return new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Dataset.BorderColor,
                StrokeWidth = (float)Dataset.BorderWidth
            };

        }
    }
}