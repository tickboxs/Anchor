using System;
using Anchor.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anchor.Domains.BarChart;
using Anchor.Styles;
using Anchor.Utils;
using Anchor.Accessories;
using Anchor.Animations;

namespace Anchor.Charts
{
    public class BarChart : IChartAnimatable,IChart
    {
        public BarChartData Data { private set; get; }
        private SKCanvasView Canvas { set; get; }
        public BarChartOption Option { set; get; }

        public BarChart(
            BarChartData data,
            SKCanvasView canvasView,
            BarChartOption option)
        {
            Data = data;
            Canvas = canvasView;
            Option = option;
        }

        public void Draw(SKPaintSurfaceEventArgs args)
        {
            // Clear Canvas Before Actual Draw
            new SKCanvasUtils(args).Canvas.Clear();

            // Draw Axis
            DrawAxis(args);
           
            // Draw Datasets
            DrawDatasets(args);

            // Draw Title
            //DrawTitle(args);

            // Draw Legends
            //DrawLegends(args);

        }

        public void DrawTitle(SKPaintSurfaceEventArgs args)
        {
            new Title(args,Option.Title).Draw();
        }

        public void DrawLegends(SKPaintSurfaceEventArgs args)
        {
            var backgroundColor = new List<SKColor>();
            var borderColor = new List<SKColor>();
            var labels = Data.Labels;
            foreach (var dataset in Data.Datasets)
            {
                backgroundColor.Add(dataset.BackgroundColor);
                borderColor.Add(dataset.BorderColor);
            }

            // Draw Legends
            new Legends()
            {
                BackgroundColor = backgroundColor,
                BorderColor = borderColor,
                Labels = labels,
                Args = args,
            }.Draw();
        }

        public void DrawDatasets(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            // TODO
            float CHART_PADDING = 20;
            float BAR_PADDING = 10;
            float Category_PADDING = 20;

            // Leave Some Room For Axis X Y
            const float Axis_X = 100;
            const float Axis_Y = 100;

            // Bar Width
            var Category_Width = (info.Width - Axis_X - 2 * CHART_PADDING) / (Data.Datasets[0].Anchors.Count);
            var Bar_Width = (Category_Width - 2 * Category_PADDING - (Data.Datasets.Count - 1) * BAR_PADDING) / Data.Datasets.Count;

            var Max_Value = DatasetUtils.YMax(Data.Datasets[0].Anchors);

            // Draw Bar
            for (int di = 0; di < Data.Datasets.Count; di++)
            {
                var dataset = Data.Datasets[di];

                var barStyle = new BarChartStyle(dataset);
                var fillPaint = barStyle.FillPaint();
                var strokePaint = barStyle.StrokePaint();
                for (int i = 0; i < dataset.Anchors.Count; i++)
                {
                    // Anchor
                    var anchor = dataset.Anchors[i];

                    // Get Height For Each BarChartData
                    var Bar_Height = anchor.Y / Max_Value * (info.Height - Axis_Y);

                    // Reverse Y Coordinate
                    var Y = (info.Height - Axis_Y) - (float)Bar_Height * Progress;

                    // X coordinate
                    var X = Axis_X + // Room for x Axis
                            i * Category_Width + // Category x offset
                            Category_PADDING + // Category padding offset
                            (BAR_PADDING + Bar_Width) * di; // Dataset x offset

                    // Assign YScale To Height To Animate
                    canvas.DrawRect(X, (float)Y, Bar_Width, (float)Bar_Height * Progress, fillPaint);
                    canvas.DrawRect(X, (float)Y, Bar_Width, (float)Bar_Height * Progress, strokePaint);
                }
            }
        }

        public void DrawAxis(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            var canvasUtils = new SKCanvasUtils(args);
            var canvas = canvasUtils.Canvas;
            var screen_width = canvasUtils.Width;
            var screen_height = canvasUtils.Height;

            // Leave Some Room For Axis X Y
            const float Axis_X = 100;
            const float Axis_Y = 100;

            // RESERVED START !!!
            //SKPaint axisPaint = new SKPaint
            //{
            //    Style = SKPaintStyle.Stroke,
            //    Color = SKColor.Parse("#444444"),
            //    StrokeWidth = 1,
            //    StrokeCap = SKStrokeCap.Round
            //};

            //// Draw X Axis
            //canvas.DrawLine(Axis_X, screen_height - Axis_Y, screen_width, screen_height - Axis_Y, axisPaint);

            //// Draw Y Axis
            //canvas.DrawLine(Axis_X, screen_height - Axis_Y, Axis_X, 0, axisPaint);
            // RESERVED END !!!

            // Begin to Draw Scale

            // Calculate Max_X, Max_Y, Min_X, Min_Y
            var anchorPointsList = from dataset in Data.Datasets select dataset.Anchors;
            float max_x = DatasetUtils.XMax(anchorPointsList.ToList());
            float max_y = DatasetUtils.YMax(anchorPointsList.ToList());
            float min_x = DatasetUtils.XMin(anchorPointsList.ToList());
            float min_y = DatasetUtils.YMin(anchorPointsList.ToList());
            float span_x = max_x - min_x;
            float span_y = max_y - min_y;

            var count_x = Data.Labels.Count();
            var count_y = Math.Floor((screen_height - Axis_Y) / Option.DensityY);
            
            var step_y = span_y / count_y;
            var dr_x = (screen_width - Axis_X) / count_x;
            var dr_y = (screen_height - Axis_Y) / count_y;

            var textPaint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = Color.Black.ToSKColor(),
                TextSize = 30
            };

            var assistLinePaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = Color.LightGray.ToSKColor()
            };

            // Draw X Scale
            for (int i = 0; i < count_x; i++)
            {
                var text = Data.Labels[i];
                var textBounds = new SKRect();
                textPaint.MeasureText(text, ref textBounds);

                var textPoint = new SKPoint(
                    Axis_X + i * (float)dr_x + textBounds.Width * 0.5f,
                    screen_height - Axis_Y + textBounds.Height);
                canvas.DrawText(text, textPoint, textPaint);

                // Draw Assist Line X
                var lineFromPoint = new SKPoint(Axis_X + i * (float)dr_x, screen_height - Axis_Y);
                var lineDesePoint = new SKPoint(Axis_X + i * (float)dr_x, 0);
                canvas.DrawLine(lineFromPoint, lineDesePoint, assistLinePaint);
            }

            // Draw Y Scale
            for (int i = 0; i <= count_y; i++)
            {
                var text = string.Format("{0:N1}", min_y + i * step_y);
                var textBounds = new SKRect();
                textPaint.MeasureText(text, ref textBounds);

                var textPoint = new SKPoint(
                    0,
                    screen_height - Axis_Y - i * (float)dr_y + textBounds.Height);
                canvas.DrawText(text, textPoint, textPaint);

                // Draw Assist Line Y
                var lineFromPoint = new SKPoint(Axis_X, screen_height - Axis_Y - i * (float)dr_y);
                var lineDesePoint = new SKPoint(screen_width, screen_height - Axis_Y - i * (float)dr_y);
                canvas.DrawLine(lineFromPoint, lineDesePoint, assistLinePaint);
            }
        }

        public void Invalidate()
        {
            Canvas.InvalidateSurface();
        }

        public override void Animate(float duration = 0.5F)
        {
            Animator.Run((progress) => {
                Progress = progress;
                Canvas.InvalidateSurface();
            }, duration: duration);
        }
    }
}
