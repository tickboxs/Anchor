using System;
using Anchor.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Anchor.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anchor.Domains.ScatterChart;
using Anchor.Shapes;
using Anchor.Accessories;
using Anchor.Utils;
using Anchor.Animations;

namespace Anchor.Charts
{
    public class ScatterChart : IChartAnimatable, IChart
    {
        public ScatterChartData Data { private set; get; }
        private SKCanvasView Canvas { set; get; }
        public ScatterChartOption Option { set; get; }

        public ScatterChart() { }

        public ScatterChart(
            ScatterChartData data,
            SKCanvasView canvasView,
            ScatterChartOption option 
            )
        {
            Data = data;
            Canvas = canvasView;
            Option = option;
        }

        public void Draw(SKPaintSurfaceEventArgs args)
        {
            // Clear canvas before each draw
            new SKCanvasUtils(args).Canvas.Clear();

            // Draw Axis
            DrawAxis(args);

            // Draw Datasets
            DrawDatasets(args);

            // Draw Legends
            //DrawLegends(args);

            // Draw Title
            //DrawTitle(args);

        }

        public void Invalidate()
        {
            Canvas.InvalidateSurface();
        }

        public void DrawTitle(SKPaintSurfaceEventArgs args)
        {
            new Title(args,Option.Title).Draw();
        }

        public void DrawLegends(SKPaintSurfaceEventArgs args)
        {
            var backgroundColor = new List<SKColor>();
            var borderColor = new List<SKColor>();
            foreach (var dataset in Data.Datasets)
            {
                backgroundColor.Add(dataset.BackgroundColor);
                borderColor.Add(dataset.BorderColor);
            }
            new Legends(args,backgroundColor, borderColor, Data.Labels).Draw();
        }

        public void DrawDatasets(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            var canvasUtils = new SKCanvasUtils(args);
            var canvas = canvasUtils.Canvas;
            var screen_width = canvasUtils.Width;
            var screen_height = canvasUtils.Height;

            // Calculate Max_X, Max_Y, Min_X, Min_Y
            var anchorPointsList = from dataset in Data.Datasets select dataset.Anchors;
            float max_x = DatasetUtils.XMax(anchorPointsList.ToList());
            float max_y = DatasetUtils.YMax(anchorPointsList.ToList());
            float min_x = DatasetUtils.XMin(anchorPointsList.ToList());
            float min_y = DatasetUtils.YMin(anchorPointsList.ToList());
            float span_x = max_x - min_x;
            float span_y = max_y - min_y;

            // Leave Some Room For Axis X Y
            const float Axis_X = 100;
            const float Axis_Y = 100;

            // Calculate dimensionRatio
            float dr_x = (screen_width - Axis_X) / span_x;
            float dr_y = (screen_height - Axis_Y) / span_y;

            for (int di = 0; di < Data.Datasets.Count; di++)
            {
                // Dataset
                var dataset = Data.Datasets[di];


                // Draw Scatter
                for (int i = 0; i < dataset.Anchors.Count; i++)
                {
                    // Anchor
                    var anchor = dataset.Anchors[i];

                    // Begin Draw Each ChartData
                    // Fill
                    SKPaint fillPaint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = dataset.BackgroundColor
                    };

                    // Stroke
                    SKPaint strokePaint = new SKPaint
                    {
                        Style = SKPaintStyle.Stroke,
                        Color = dataset.BorderColor,
                        StrokeWidth = (float)(dataset.BorderWidth),
                        StrokeCap = dataset.BorderCapStyle,
                        StrokeJoin = dataset.BorderJoinStyle
                    };


                    // Draw Shape
                    var shapeCenter = new SKPoint(
                        ((anchor.X - min_x)* dr_x) + Axis_X,
                        screen_height - Axis_Y - (anchor.Y - min_y) * dr_y * Progress);

                    // AnchorPoint.R own higher priority than Dataset.Radius
                    float radius = anchor.R != 0 ? anchor.R : dataset.Radius;

                    ShapeFactory.Create(
                        dataset.PointStyle,
                        shapeCenter,
                        radius,
                        canvas,
                        fillPaint,
                        strokePaint,
                        SKMatrix.Identity)
                        .Draw();

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

            var count_x = Math.Floor((screen_width - Axis_X) / Option.DensityX);
            var count_y = Math.Floor((screen_height - Axis_Y) / Option.DensityY);
            var step_x = span_x / count_x;
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
                var text = string.Format("{0:N1}",min_x + i * step_x);
                var textBounds = new SKRect();
                textPaint.MeasureText(text, ref textBounds);

                var textPoint = new SKPoint(
                    Axis_X + i * (float)dr_x,
                    screen_height - Axis_Y + textBounds.Height);
                canvas.DrawText(text, textPoint, textPaint);

                // Draw Assist Line X
                var lineFromPoint = new SKPoint(Axis_X + i * (float)dr_x, screen_height - Axis_Y);
                var lineDesePoint = new SKPoint(textPoint.X,0);
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
                    screen_height-Axis_Y-i*(float)dr_y + textBounds.Height);
                canvas.DrawText(text, textPoint, textPaint);

                // Draw Assist Line Y
                var lineFromPoint = new SKPoint(Axis_X, screen_height - Axis_Y - i * (float)dr_y);
                var lineDesePoint = new SKPoint(screen_width, screen_height - Axis_Y - i * (float)dr_y);
                canvas.DrawLine(lineFromPoint, lineDesePoint, assistLinePaint);
            }


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
