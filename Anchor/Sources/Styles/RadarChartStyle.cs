using System;
using Anchor.Domains.RadarChart;
using SkiaSharp;

namespace Anchor.Styles
{
    public class RadarChartStyle:IChartStyle,IChartPointStyle
    {
        public RadarChartStyle(RadarChartDataset dataset)
        {
            Dataset = dataset;
        }

        public RadarChartDataset Dataset { private set; get; }

        public SKPaint FillAndStrokePaint()
        {
            throw new NotImplementedException();
        }

        public SKPaint FillPaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = Dataset.BackgroundColor
            };


        }

        public SKPaint StrokePaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = Dataset.BorderColor,
                StrokeWidth = (float)(Dataset.BorderWidth),
                StrokeCap = Dataset.BorderCapStyle,
                StrokeJoin = Dataset.BorderJoinStyle

            };
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
                Color = Dataset.BorderColor,
                StrokeWidth = (float)Dataset.PointBorderWidth
            };
        }
    }
}
