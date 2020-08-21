
using Anchor.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Anchor.Domains.LineChart;
using System.Collections.Generic;
using Anchor.Shapes;
using Anchor.Domains;
using Anchor.Utils;
using Anchor.Styles;
using Anchor.Accessories;
using Anchor.Animations;
using System.Linq;
using System;
using Xamarin.Forms;

namespace Anchor.Charts
{
    public class LineChart : IChartAnimatable, IChart
    {
        public LineChartData Data { private set; get; }
        public SKCanvasView Canvas { private set; get; }
        public LineChartOption Option { private set; get; }

        public LineChart(LineChartData data, SKCanvasView canvas, LineChartOption option)
        {
            Data = data;
            Canvas = canvas;
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

        private void DrawLine(SKCanvas canvas,LineChartDataset dataset,SKPoint[] points)
        {

            var lineStyle = new LineChartStyle(dataset);
            var linePaint = lineStyle.FillAndStrokePaint();

            // Draw Lines Here
            canvas.DrawPoints(SKPointMode.Polygon, points, linePaint);

            if (dataset.Fill == true)
            {
                // Draw Fill TODO 这里是画填充图

            }
        }

        private void DrawShapes(
            SKCanvas canvas,
            LineChartDataset dataset,
            SKPoint[] points)
        {
            var lineStyle = new LineChartStyle(dataset);
            var pointFillPaint = lineStyle.PointFillPaint();
            var pointStrokePaint = lineStyle.PointStrokePaint();

            // Draw Points Shapes
            foreach (var point in points)
            {
                var rotation = SKMatrix.CreateRotationDegrees(0, point.X, point.Y);

                // Draw Points Here
                ShapeFactory.Create(
                    dataset.PointStyle,
                    rotation.MapPoint(point),
                    dataset.PointRadius,
                    canvas,
                    pointFillPaint,
                    pointStrokePaint,
                    rotation)
                    .Draw();
            }

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
            var labels = Data.Labels;
            new Legends(args,backgroundColor, borderColor, labels).Draw();
        }

        public void DrawDatasets(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            var canvasUtils = new SKCanvasUtils(args);
            var canvas = canvasUtils.Canvas;
            var screen_width = canvasUtils.Width;
            var screen_height = canvasUtils.Height;

            // Leave Some Room For Axis X Y
            const float Axis_X = 100;
            const float Axis_Y = 100;

            // Calculate Max_X, Max_Y, Min_X, Min_Y
            var anchorPointsList = from dataset in Data.Datasets select dataset.Anchors;
            float max_x = DatasetUtils.XMax(anchorPointsList.ToList());
            float max_y = DatasetUtils.YMax(anchorPointsList.ToList());
            float min_x = DatasetUtils.XMin(anchorPointsList.ToList());
            float min_y = DatasetUtils.YMin(anchorPointsList.ToList());
            float span_x = max_x - min_x;
            float span_y = max_y - min_y;
            var count_x = Data.Labels.Count();

            // Calculate dimensionRation
            float dr_x =
                Option.ScaleType ==
                ScaleType.Category ?
                (screen_width - Axis_X) / count_x :
                (screen_width - Axis_X) / span_x;
            float dr_y = (screen_height - Axis_Y) / span_y;

           

            for (int di = 0; di < Data.Datasets.Count; di++)
            {
                var dataset = Data.Datasets[di];
                var points = new List<SKPoint> { };

                for (int i = 0; i < dataset.Anchors.Count; i++)
                {
                    var anchor = dataset.Anchors[i];
                    var x =
                        Option.ScaleType == ScaleType.Category ?
                        Axis_X + i * dr_x + 0.5f * dr_x :
                        Axis_X + i * dr_x;
                    var y = (screen_height - Axis_Y) - dr_y * anchor.Y * Progress;
                    points.Add(new SKPoint(x, y));
                }

                if (dataset.ShowLine == true)
                {
                    // Draw Line
                    DrawLine(canvas, dataset, points.ToArray());
                }

                // DrawShapes
                DrawShapes(canvas, dataset, points.ToArray());

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
            var count_x =
                Option.ScaleType == ScaleType.Category ?
                Data.Labels.Count() :
                Math.Floor((screen_width - Axis_X) / Option.DensityX);
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
                var text = Data.Labels[i];
                var textBounds = new SKRect();
                textPaint.MeasureText(text, ref textBounds);


                var textPoint_x = Option.ScaleType ==
                                    ScaleType.Category ?
                                    Axis_X + i * (float)dr_x + textBounds.Width * 0.5f :
                                    Axis_X + i * (float)dr_x;
                var textPoint = new SKPoint(
                    textPoint_x,
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
