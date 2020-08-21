using System;
using Anchor.Domains.LineChart;
using SkiaSharp;

namespace Anchor.Styles
{
    public class LineChartStyle:IChartStyle,IChartPointStyle
    {
        public LineChartStyle(LineChartDataset dataset)
        {
            Dataset = dataset;
        }

        public LineChartDataset Dataset { private set; get; }

        // Draw Line with fill and stroke
        public SKPaint FillAndStrokePaint()
        {

            float[] dashIntervel = Dataset.BorderDash;
            var dashEffect = SKPathEffect.CreateDash(dashIntervel, Dataset.BorderDashOffset);
            return new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = Dataset.BorderColor,
                StrokeWidth = (float)Dataset.BorderWidth,
                StrokeCap = Dataset.BorderCapStyle,
                StrokeJoin = Dataset.BorderJoinStyle,
                PathEffect = dashEffect
            };
        }

        public SKPaint FillPaint()
        {
            throw new NotImplementedException();
        }

        public SKPaint PointFillAndStrokePaint()
        {
            throw new NotImplementedException();
        }

        public SKPaint PointFillPaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = Dataset.PointBackgroundColor

            };
        }

        public SKPaint PointStrokePaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = Dataset.PointBorderColor,
                StrokeWidth = (float)Dataset.PointBorderWidth
            };
        }

        public SKPaint StrokePaint()
        {
            throw new NotImplementedException();
        }
    }
}
