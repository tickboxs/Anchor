using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Anchor.Charts;
using Anchor.Domains.PieChart;
using Anchor.Styles;
using Anchor.Shapes;
using SkiaSharp.Views.Forms;
using Anchor.Domains;
using SkiaSharp;

namespace Anchor.Views
{
    public partial class PieChartPage : ContentPage
    {
        public PieChart PieChart { set; get; }
        public PieChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (PieChart == null) return;
            PieChart.Draw(args);

        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (PieChart == null)
            {
                var dataset0 = new PieChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:10),
                        new AnchorPoint(y:20),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:40),
                    },
                    BackgroundColor = new List<SKColor>
                    {
                        SKColor.Parse("#d50000"),
                        SKColor.Parse("#c51162"),
                        SKColor.Parse("#aa00ff"),
                        SKColor.Parse("#6200ea")
                    },
                    BorderColor = new List<SKColor>
                    {
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                    },
                    Labels = new List<string> { "LabelA", "LabelB", "LabelC", "LabelD" }
                };

                var dataset1 = new PieChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:15),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:50),
                        new AnchorPoint(y:5),
                    },
                    BackgroundColor = new List<SKColor>
                    {
                        SKColor.Parse("#304ffe"),
                        SKColor.Parse("#2962ff"),
                        SKColor.Parse("#0091ea"),
                        SKColor.Parse("#00b8d4")
                    },
                    BorderColor = new List<SKColor>
                    {
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                        SKColor.Parse("#ffffffff"),
                    },
                    Labels = new List<string> { "LabelE", "LabelF", "LabelG", "LabelH" }
                };

                var option = new PieChartOption()
                {

                };

                
                var pieChartData = new PieChartData(new List<PieChartDataset> { dataset0, dataset1 });

                PieChart = new PieChart(pieChartData, canvas, option, PieChartType.Pie);
                //PieChart.Invalidate();
                PieChart.Animate();

            }
        }
    }
}
