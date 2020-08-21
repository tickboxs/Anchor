using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Anchor.Charts;
using Anchor.Domains.PieChart;
using Anchor.Styles;
using Anchor.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Anchor.Domains.RadarChart;
using Anchor.Domains;

namespace Anchor.Views
{
    public partial class RadarChartPage : ContentPage
    {
        public RadarChart RadarChart { get; set; }

        public RadarChartPage()
        {
            InitializeComponent();
        }

        // OnDraw Callback
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if (RadarChart == null) return;
            RadarChart.Draw(args);

        }

        protected override void OnAppearing()
        {
            // Load BarChartDatas Only Once
            if (RadarChart == null)
            {
                var dataset0 = new RadarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:10),
                        new AnchorPoint(y:20),
                        new AnchorPoint(y:30),
                        new AnchorPoint(y:40),
                        new AnchorPoint(y:50),
                    },
                    BackgroundColor = SKColor.Parse("#d50000ff"),
                    BorderColor = SKColor.Parse("#ffffffff")
                };

                var dataset1 = new RadarChartDataset()
                {
                    Anchors = new List<AnchorPoint>
                    {
                        new AnchorPoint(y:25),
                        new AnchorPoint(y:15),
                        new AnchorPoint(y:40),
                        new AnchorPoint(y:90),
                        new AnchorPoint(y:10),
                    },
                    BackgroundColor = SKColor.Parse("#aa11ffff"),
                    BorderColor = SKColor.Parse("#ffffffff")
                };

                var option = new RadarChartOption()
                {

                };

                string[] labels = { "LabelA", "LabelB", "LabelC", "LabelD", "LabelE" };
                var radarChartData = new RadarChartData(new List<RadarChartDataset> { dataset0,dataset1 }, labels);

                RadarChart = new RadarChart(radarChartData, canvas,option);
                //RadarChart.Invalidate();
                RadarChart.Animate();

            }
        }
    }
}
