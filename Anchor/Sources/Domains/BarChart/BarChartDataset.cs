using System.Collections.Generic;
using SkiaSharp;

namespace Anchor.Domains.BarChart
{
    public class BarChartDataset
    {
        public BarChartDataset() { }

        public BarChartDataset(SKColor backgroundColor, IList<AnchorPoint> anchors)
        {
            BackgroundColor = backgroundColor;
            Anchors = anchors;
        }

        // Default rgba(0,0,0,0.1)
        public SKColor BackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        public SKColor BorderColor { set; get; } = SKColor.Parse("#000000");

        // Default None
        public BarChartBorderSkipped BorderSkipped { set; get; }

        // Default 0
        public double BorderWidth { set; get; }

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        // Default rgba(0,0,0,0.1)
        public SKColor SelectedBackgroundColor { set; get; } = SKColor.Parse("#444444");

        // Default #000000
        public SKColor SelectedBorderColor { set; get; } = SKColor.Parse("#000000");

        // Default 0
        public double SelectedBorderWidth { set; get; }

        // Labels
        public string Label { set; get; }

        // Default 0
        public int Order { set; get; } = 0;

        // X axis
        public string XAxisID { set; get; }

        // Y axis
        public string YAxisID { set; get; }

    }
}
