using System;
using System.Collections.Generic;
using Anchor.Domains;
using SkiaSharp;

namespace Anchor.Domains.PolarChart
{
    public class PolarChartDataset
    {

        public PolarChartDataset() { }

        public PolarChartDataset(IList<AnchorPoint> anchors, IList<SKColor> backgroundColor)
        {
            Anchors = anchors;
            BackgroundColor = backgroundColor;
        }

        // Default rgba(0,0,0,0.1)
        public IList<SKColor> BackgroundColor { set; get; } = new List<SKColor> { SKColor.Parse("#444444") };

        // Default '#fff'
        public IList<SKColor> BorderColor { set; get; } = new List<SKColor> { SKColor.Parse("#000000") };

        // Default 2
        public double BorderWidth { set; get; } = 2;

        // Required
        public IList<AnchorPoint> Anchors { set; get; }

        public IList<SKColor> SelectedBackgroundColor { set; get; }

        public IList<SKColor> SelectedBorderColor { set; get; }

        public double SelectedBorderWidth { set; get; }

        // Default .center
        public PolarChartBorderAlign BorderAlign { set; get; } = PolarChartBorderAlign.Center;
    }
}
