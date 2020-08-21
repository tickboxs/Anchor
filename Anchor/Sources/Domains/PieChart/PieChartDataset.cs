using System.Collections.Generic;
using SkiaSharp;
using Anchor.Domains;

namespace Anchor.Domains.PieChart
{
    public class PieChartDataset
    {
        public PieChartDataset() { }
        
        public PieChartDataset(
            IList<AnchorPoint> anchors,
            IList<SKColor> backgroundColor,
            IList<string> labels)   
        {

            Anchors = anchors;
            BackgroundColor = backgroundColor;
            Labels = labels;
        }

        public IList<string> Labels { set; get; }

        // Default rgba(0,0,0,0.1)
        public IList<SKColor> BackgroundColor { set; get; } = new List<SKColor> { SKColor.Parse("#444444") };

        // Default Center
        public PieChartBorderAlign BorderAlign { set; get; } = PieChartBorderAlign.Center;

        // Default '#fff'
        public IList<SKColor> BorderColor { set; get; } = new List<SKColor> { SKColor.Parse("#000000") };

        // Default 2
        public double BorderWidth { set; get; } = 2;

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        public IList<SKColor> SelectedBackgroundColor { set; get; }

        public IList<SKColor> SelectedBorderColor { set; get; }

        public double SelectedBorderWidth { set; get; }

        // Default 1
        public double Weight { set; get; } = 1;

    }
}
