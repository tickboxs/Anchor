using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Anchor.Charts;
using Anchor.Domains.PieChart;
using Anchor.Styles;
using Anchor.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Anchor.Domains.PolarChart;
using Anchor.Domains;

namespace Anchor.Views
{
    public partial class PolarChartPage : ContentPage
    {

        public PolarChart PolarChart { set; get; }
        public PolarChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (PolarChart == null) return;
            PolarChart.Draw(args);

        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (PolarChart == null)
            {
                var dataset = new PolarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:2),
                        new AnchorPoint(y:2),
                        new AnchorPoint(y:2),
                        new AnchorPoint(y:3),
                        new AnchorPoint(y:3),
                    },
                    BackgroundColor = new List<SKColor>
                    {
                        SKColor.Parse("#d50000bb"),
                        SKColor.Parse("#c51162bb"),
                        SKColor.Parse("#aa00ffbb"),
                        SKColor.Parse("#6200eabb"),
                        SKColor.Parse("#eeeeeebb")
                    },
                    BorderColor = new List<SKColor>
                    {
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff")
                    },
                    BorderWidth = 5
                };

                var option = new PolarChartOption()
                {

                };


                var polarChartData = new PolarChartData(
                    dataset,
                    new List<string> { "LabelA", "LabelB", "LabelC", "LabelD", "LabelE" }.ToArray());

                PolarChart = new PolarChart(polarChartData, canvas, option);
                //PolarChart.Invalidate();
                PolarChart.Animate();

            }
        }
    }
}
