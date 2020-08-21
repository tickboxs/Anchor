using System;
using Anchor.Domains.PolarChart;
using SkiaSharp;

namespace Anchor.Styles
{
    public class PolarChartStyle : IChartStyle
    {
        public PolarChartStyle(PolarChartDataset dataset)
        {
            Dataset = dataset;
        }

        public PolarChartDataset Dataset { private set; get; }

        public SKPaint FillAndStrokePaint()
        {
            throw new NotImplementedException();
        }

        public SKPaint FillPaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Fill
            };
        }

        public SKPaint StrokePaint()
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = (float)Dataset.BorderWidth
            };

        }

        // For Polar and Pie,donut
        public SKPaint FillPaintAtAnchorIndex(int index)
        {
            var fillPaint = FillPaint();

            // If there is one backgroundColor,set all backgroundColor to the first one
            // If there is not coressponding backgroundColor,then use the default #444444
            SKColor backgroundColor;
            backgroundColor = Dataset.BackgroundColor.Count == 1 ?
                Dataset.BackgroundColor[0] :
                (index <= Dataset.BackgroundColor.Count - 1 ?
                Dataset.BackgroundColor[index] : SKColor.Parse("#444444"));

            // Assign pie sector backgroundColor
            fillPaint.Color = backgroundColor;

            return fillPaint;
        }

        public SKPaint StrokePaintAtAnchorIndex(int index)
        {
            var strokePaint = StrokePaint();

            // Assign border color
            SKColor borderColor;
            borderColor = Dataset.BorderColor.Count == 1 ?
                Dataset.BorderColor[0] :
                (index <= Dataset.BorderColor.Count - 1 ?
                Dataset.BorderColor[index] : SKColor.Parse("#000000"));
            strokePaint.Color = borderColor;

            return strokePaint;
        }
    }
}
