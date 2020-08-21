
using System.Collections.Generic;
using SkiaSharp;
using Anchor.Shapes;

namespace Anchor.Domains.ScatterChart
{
    public class ScatterChartDataset
    {
        public ScatterChartDataset() { }

        public ScatterChartDataset(IList<AnchorPoint> anchors)
        {
            Anchors = anchors;
        }

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        // Default 20
        public float Radius { set; get; } = 20;

        // Default #444444
        // Point fill color.
        public SKColor BackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        // Point stroke color.
        public SKColor BorderColor { set; get; } = SKColor.Parse("#000000");

        // Default 1
        // Point stroke width
        public float BorderWidth { set; get; } = 10;

        // Default = 0
        // Point rotation (in degrees).
        public float Rotation { set; get; } = 0;

        // Default Round
        public SKStrokeCap BorderCapStyle { set; get; } = SKStrokeCap.Round;

        // Default Round
        public SKStrokeJoin BorderJoinStyle { set; get; } = SKStrokeJoin.Round;

        // Default .Circle
        // Point style
        public ShapeType PointStyle { set; get; } = ShapeType.Rect;
    }
}
