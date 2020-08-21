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
using Anchor.Domains.LineChart;

namespace Anchor.Views
{
    public partial class LineChartPage : ContentPage
    {
        public LineChart LineChart { set; get; }
        public LineChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (LineChart == null) return;
            LineChart.Draw(args);
        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (LineChart == null)
            {
                var dataset0 = new LineChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:10),
                        new AnchorPoint(y:20),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:40),
                    },
                    BackgroundColor = SKColor.Parse("#d50000"),
                    BorderColor = SKColor.Parse("#000000"),
                    PointBackgroundColor = Color.Orange.ToSKColor(),
                    PointBorderColor = Color.LightGray.ToSKColor(),
                    PointStyle = ShapeType.Circle
                };

                var dataset1 = new LineChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:150),
                        new AnchorPoint(y:300),
                        new AnchorPoint(y:500),
                        new AnchorPoint(y:50),
                    },
                    BackgroundColor = SKColor.Parse("#2962ff"),
                    BorderColor = SKColor.Parse("#000000"),
                    PointBackgroundColor = Color.Purple.ToSKColor(),
                    PointBorderColor = Color.LightGray.ToSKColor(),
                    PointStyle = ShapeType.Triangle
                };

                var dataset2 = new LineChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:112),
                        new AnchorPoint(y:96),
                        new AnchorPoint(y:202),
                        new AnchorPoint(y:97),
                    },
                    BackgroundColor = SKColor.Parse("#0091ea"),
                    BorderColor = SKColor.Parse("#000000"),
                    PointBackgroundColor = Color.Green.ToSKColor(),
                    PointBorderColor = Color.LightGray.ToSKColor(),
                    PointStyle = ShapeType.RectRot
                };

                var option = new LineChartOption()
                {
                    ScaleType = ScaleType.Category
                };

                string[] labels = { "LabelA", "LabelB", "LabelC", "LabelD" };
                var lineChartData = new LineChartData(
                    new List<LineChartDataset> { dataset0, dataset1,dataset2 },
                    labels);

                LineChart = new LineChart(lineChartData, canvas, option);
                //LineChart.Invalidate();
                LineChart.Animate();

            }
        }
    }
}
