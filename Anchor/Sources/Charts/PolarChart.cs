
using Anchor.Interfaces;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using Anchor.Utils;
using Anchor.Domains.PolarChart;
using System;
using Anchor.Styles;
using Anchor.Accessories;
using Anchor.Animations;

namespace Anchor.Charts
{
    public class PolarChart : IChartAnimatable, IChart
    {

        public PolarChartData Data { private set; get; }
        public SKCanvasView Canvas { private set; get; }
        public PolarChartOption Option { private set; get; }

        public PolarChart(
            PolarChartData data,
            SKCanvasView canvasView,
            PolarChartOption option)
        {
            Data = data;
            Canvas = canvasView;
            Option = option;
        }

        public void Draw(SKPaintSurfaceEventArgs args)
        {
            // Clear canvas
            new SKCanvasUtils(args).Canvas.Clear();

            // DrawAxis
            // DrawWeb and DrawScale
            DrawAxis(args);

            // Draw Datasets
            DrawDatasets(args);

            // Draw Title
            DrawTitle(args);

            // Draw Legends
            DrawLegends(args); 

        }

        public void DrawTitle(SKPaintSurfaceEventArgs args)
        {
            new Title(args,Option.Title).Draw();
        }

        public void DrawLegends(SKPaintSurfaceEventArgs args)
        {
            var backgroundColor = Data.Dataset.BackgroundColor;
            var borderColor = Data.Dataset.BorderColor;
            var labels = Data.Labels;
            new Legends(args,backgroundColor, borderColor, labels).Draw();
        }

        private void DrawWeb(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var canvasUtil = new SKCanvasUtils(args);

            SKPoint center = canvasUtil.Center;
            float radius = (float)canvasUtil.Radius;

            // Draw background Web
            SKPaint webPaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColor.Parse("#444444")
            };

            // Calculate web density according to Option
            var densityCount = Math.Floor(radius / Option.Density);
            var densityStep = radius / densityCount;

            for (int i = 0; i < densityCount; i++)
            {
                canvas.DrawCircle(center, (float)(radius - i * densityStep), webPaint);
            }
        }

        private void DrawScale(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var canvasUtils = new SKCanvasUtils(args);

            SKPoint center = canvasUtils.Center;
            float radius = (float)canvasUtils.Radius;

            var max = DatasetUtils.YMax(Data.Dataset.Anchors);

            // No negtive Y allowed
            var span = max - 0;

            // Calculate web density according to Option
            var densityCount = Math.Floor(radius / Option.Density);
            var scaleStep = MathUtils.NearestFibonacci(span / densityCount);
            var positionStep = (radius / densityCount);

            for (int i = 0; i <= densityCount; i++)
            {
                // Y in scale always starts with 0
                var scaleText = string.Format("{0}", 0 + i * scaleStep);
                var scalePoint = new SKPoint(center.X, center.Y - (float)positionStep * i);
                var scalePaint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse("#000000"),
                    TextSize = 30
                };

                canvas.DrawText(scaleText, scalePoint, scalePaint);
            }
        }

        public void DrawDatasets(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            var canvasUtils = new SKCanvasUtils(args);

            // Get total value of data for one dataset
            double ySum = DatasetUtils.YSum(Data.Dataset.Anchors);

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);

            // Assign ZRotateAndTranslate To Animate Offset
            float radius = (float)canvasUtils.Radius;

            var max = DatasetUtils.YMax(Data.Dataset.Anchors);

            // No negtive Y allowed
            var span = max - 0;

            // Calculate web density according to Option
            var densityCount = Math.Floor(radius / Option.Density);
            var scaleStep = span / densityCount;

            var yMax = MathUtils.NearestFibonacci(scaleStep) * densityCount;

            var y_dr = radius / yMax;
            // Assign ZRotateAndTranslate To Rotation
            var defaultStartRotation = (float)(Option.StartAngle);

            // Rotate Animation
            var dataset = Data.Dataset;
            float startAngle = Option.AnimateRotate ? 0 + (defaultStartRotation * Progress) : defaultStartRotation;
            for (int i = 0; i < dataset.Anchors.Count; i++)
            {
                var anchor = dataset.Anchors[i];
                float radiusForAnchor = (float)(anchor.Y * y_dr);
                SKRect outterRect = new SKRect(
                    center.X - radiusForAnchor * Progress,
                    center.Y - radiusForAnchor * Progress,
                    center.X + radiusForAnchor * Progress,
                    center.Y + radiusForAnchor * Progress);

                float sweepAngle = Option.DynamicSweep == true ?
                    (float)(360.0f * anchor.Y / ySum) : // Dynamic Sweep Angle
                    (float)(360.0f / Data.Dataset.Anchors.Count); // Equal Sweep Angle

                using (SKPath path = new SKPath())
                {
                    // Config Style
                    var polarStyle = new PolarChartStyle(dataset);
                    var fillPaint = polarStyle.FillPaintAtAnchorIndex(i);
                    var strokePaint = polarStyle.StrokePaintAtAnchorIndex(i);

                    path.MoveTo(center);
                    path.ArcTo(outterRect, startAngle, sweepAngle, false);
                    path.Close();

                    canvas.Save();

                    // Fill and Stroke the path
                    canvas.DrawPath(path, fillPaint);
                    canvas.DrawPath(path, strokePaint);

                    canvas.Restore();

                }

                startAngle += sweepAngle;
            }
        }

        public void DrawAxis(SKPaintSurfaceEventArgs args)
        {
            DrawWeb(args);
            DrawScale(args);
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
