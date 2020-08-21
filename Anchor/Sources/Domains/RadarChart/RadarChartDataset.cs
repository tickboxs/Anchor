using System.Collections.Generic;
using SkiaSharp;
using Anchor.Shapes;

namespace Anchor.Domains.RadarChart
{
    public class RadarChartDataset
    {
        public RadarChartDataset() { }

        public RadarChartDataset(SKColor backgroundColor, IList<AnchorPoint> anchors)
        {
            BackgroundColor = backgroundColor;
            Anchors = anchors;
        }

        // Default rgba(0,0,0,0.1)
        public SKColor BackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        public SKColor BorderColor { set; get; } = SKColor.Parse("#000000");

        // Default 1
        public double BorderWidth { set; get; } = 5;

        // Default Round
        public SKStrokeCap BorderCapStyle { set; get; } = SKStrokeCap.Round;

        // Default Round
        public SKStrokeJoin BorderJoinStyle { set; get; } = SKStrokeJoin.Round;

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        // Default rgba(0,0,0,0.1)
        public IList<SKColor> SelectedBackgroundColor { set; get; } = new List<SKColor> { SKColor.Parse("#444444") };

        // Default #000000
        public IList<SKColor> SelectedBorderColor { set; get; } = new List<SKColor> { SKColor.Parse("#000000") };

        // Default 0
        public double SelectedBorderWidth { set; get; }

        // Labels
        public string Label { set; get; }

        // Default 0
        public int Order { set; get; } = 0;

        // Default true
        public bool Fill { set; get; } = true;

        public SKColor PointBackgroundColor { set; get; } = SKColor.Parse("#FFFFFF");

        public SKColor PointBorderColor { set; get; } = SKColor.Parse("#000000");

        public double PointBorderWidth { set; get; } = 2;

        public double PointRadius { set; get; } = 5;

        public double PointRotation { set; get; } = 0;

        public ShapeType PointStyle { set; get; } = ShapeType.Circle;

        public double LineTension { set; get; } = 0;

    }
}
