using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Anchor.Charts;
using Anchor.Domains.PieChart;
using Anchor.Styles;
using Anchor.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Anchor.Domains.ScatterChart;
using Anchor.Domains;

namespace Anchor.Views
{
    public partial class ScatterChartPage : ContentPage
    {

        public ScatterChart ScatterChart { set; get; }

        public ScatterChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (ScatterChart == null) return;
            ScatterChart.Draw(args);
        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (ScatterChart == null)
            {
                var dataset0 = new ScatterChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(x:10,y:10),
                        new AnchorPoint(x:20,y:20),
                        new AnchorPoint(x:30,y:30),
                        new AnchorPoint(x:40,y:40),
                        new AnchorPoint(x:50,y:50),
                    },
                    BackgroundColor = SKColor.Parse("#d50000ff"),
                    BorderColor = SKColor.Parse("#ffffffff"),
                    BorderWidth = 0,
                    PointStyle = ShapeType.Triangle
                };

                var dataset1 = new ScatterChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(x:15,y:25),
                        new AnchorPoint(x:25,y:15),
                        new AnchorPoint(x:35,y:40),
                        new AnchorPoint(x:45,y:90),
                        new AnchorPoint(x:55,y:10),
                    },
                    BackgroundColor = SKColor.Parse("#aa11ffff"),
                    BorderColor = SKColor.Parse("#ffffffff"),
                    PointStyle = ShapeType.Triangle,
                    BorderWidth = 0
                };

                var option = new ScatterChartOption()
                {

                };

                string[] labels = { "LabelA", "LabelB" };
                var scatterChartData = new ScatterChartData(
                    new List<ScatterChartDataset> { dataset0, dataset1 },
                    labels);

                ScatterChart = new ScatterChart(scatterChartData, canvas, option);
                //ScatterChart.Invalidate();
                ScatterChart.Animate();

            }
        }
    }
}
