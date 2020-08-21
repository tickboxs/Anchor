using System.Collections.Generic;
using SkiaSharp;
using Anchor.Shapes;
using Anchor.Domains;

namespace Anchor.Domains.LineChart
{
    public class LineChartDataset
    {
        public LineChartDataset() { }

        public LineChartDataset(SKColor backgroundColor, IList<AnchorPoint> anchors)
        {
            BackgroundColor = backgroundColor;
            Anchors = anchors;
        }

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        // Default rgba(0,0,0,0.1)
        public SKColor BackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        public SKColor BorderColor { set; get; } = SKColor.Parse("#000000");

        // Default Butt
        public SKStrokeCap BorderCapStyle { set; get; } = SKStrokeCap.Round;

        // Default []
        public float[] BorderDash { set; get; } = { 20,20};

        // Default 0.0
        public float BorderDashOffset { set; get; } = 0.0f;

        // Default Miter
        public SKStrokeJoin BorderJoinStyle { set; get; } = SKStrokeJoin.Miter;

        // Default 10
        public double BorderWidth { set; get; } = 1;

        // Default BorderWidth/2
        public double Clip { set; get; }

        // Default True
        public bool Fill { set; get; } = true;

        // Default rgba(0,0,0,0.1)
        public SKColor SelectedBackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        public SKColor SelectedBorderColor { set; get; } = SKColor.Parse("#000000");

        // Default []
        public IList<double> SelectedBorderDash { set; get; } = new List<double> { };

        // Default 0.0
        public double SelectedBorderDashOffset { set; get; } = 0.0;

        // Default Miter
        public SKStrokeJoin SelectedBorderJoinStyle { set; get; } = SKStrokeJoin.Round;

        // Default 3
        public double SelectedBorderWidth { set; get; } = 3;

        // Labels
        public string Label { set; get; }

        // Default 0.4
        public double LineTension { set; get; } = 0.4;

        // Default 0
        public int Order { set; get; } = 0;

        // X axis
        public string XAxisID { set; get; }

        // Y axis
        public string YAxisID { set; get; }

        // The fill color for points.
        public SKColor PointBackgroundColor { set; get; } = SKColor.Parse("#EEEEEE");

        // The border color for points.
        public SKColor PointBorderColor { set; get; } = SKColor.Parse("#000000");

        // Default 1
        // The width of the point border in pixels.
        public double PointBorderWidth { set; get; } = 1;

        // Default 10
        // The radius of the point shape. If set to 0, the point is not rendered.
        public double PointRadius { set; get; } = 10;

        // Default 0 in degree
        // The rotation of the point in degrees.
        public float PointRotation { set; get; } = 10;

        // Style of the point.
        public ShapeType PointStyle { set; get; } = ShapeType.Circle;

        // Default true
        // If false, the line is not drawn for this dataset.
        public bool ShowLine { set; get; } = true;

        public bool SpanGaps { set; get; }

    }
}
