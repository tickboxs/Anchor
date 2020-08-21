using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Anchor.Charts;
using Anchor.Domains.PieChart;
using Anchor.Styles;
using Anchor.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Anchor.Domains;
using Anchor.Domains.BarChart;

namespace Anchor.Views
{
    public partial class BarChartPage : ContentPage
    {
        public BarChart BarChart { get; set; }

        public BarChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (BarChart == null) return;
            BarChart.Draw(args);
        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (BarChart == null)
            {
                var dataset0 = new BarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:10),
                        new AnchorPoint(y:20),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:40),
                    },
                    BackgroundColor = SKColor.Parse("#d50000"),
                    BorderColor = SKColor.Parse("#ffffffff")
                };

                var dataset1 = new BarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:15),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:50),
                        new AnchorPoint(y:5),
                    },
                    BackgroundColor = SKColor.Parse("#2962ff"),
                    BorderColor = SKColor.Parse("#ffffffff")
                };

                var dataset2 = new BarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:5),
                        new AnchorPoint(y:80),
                        new AnchorPoint(y:20),
                        new AnchorPoint(y:9),
                    },
                    BackgroundColor = SKColor.Parse("#0091ea"),
                    BorderColor = SKColor.Parse("#ffffffff")
                };

                var option = new BarChartOption()
                {

                };

                string[] labels = { "LabelA","LabelB","LabelC","LabelD" };
                var barChartData = new BarChartData(
                    new List<BarChartDataset> { dataset0, dataset1 ,dataset2},
                    labels);

                BarChart = new BarChart(barChartData, canvas, option);
                //BarChart.Invalidate();
                BarChart.Animate();

            }
        }
    }

}
