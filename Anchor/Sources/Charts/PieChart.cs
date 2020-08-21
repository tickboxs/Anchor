using System;
using Anchor.Interfaces;
using SkiaSharp.Views.Forms;
using Anchor.Domains;
using System.Collections.Generic;
using SkiaSharp;
using System.Threading.Tasks;
using Anchor.Domains.PieChart;
using Anchor.Utils;
using Anchor.Accessories;
using Anchor.Animations;

namespace Anchor.Charts
{
    public class PieChart : IChartAnimatable, IChart
    {

        public PieChartData Data { private set; get; }
        public SKCanvasView Canvas { private set; get; }
        public PieChartOption Option { private set; get; }
        public PieChartType Type { private set; get; }

        public PieChart(
            PieChartData data,
            SKCanvasView canvasView,
            PieChartOption option,
            PieChartType type = PieChartType.Pie)
        {
            Data = data;
            Canvas = canvasView;
            Option = option;
            Type = type;
        }


        public void Draw(SKPaintSurfaceEventArgs args)
        {
            // Clear canvas
            new SKCanvasUtils(args).Canvas.Clear();

            // Draw Dataset
            DrawDatasets(args);

            // Draw Title
            DrawTitle(args);

            // Draw Legends
            DrawLegends(args);

        }

        // Draw inner border when Donut type
        private void DrawInnerBorder(
            SKPaintSurfaceEventArgs args,
            float borderWidth,
            float scaleOffset,
            float startAngle,
            float sweepAngle,
            SKPaint strokePaint)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);

            // Draw inner border
            float radius = Math.Min(info.Width / 2, info.Height / 2) - 2 * scaleOffset;

            var cutoutPercentage = (float)(Option.CutoutPercentage * 0.01);
            SKRect innerRect = new SKRect(
                center.X - radius * cutoutPercentage - borderWidth,
                center.Y - radius * cutoutPercentage - borderWidth,
                center.X + radius * cutoutPercentage + borderWidth,
                center.Y + radius * cutoutPercentage + borderWidth);

            SKPath innerBorderPath = new SKPath();
            innerBorderPath.MoveTo(center);
            innerBorderPath.ArcTo(innerRect, startAngle, sweepAngle, false);
            innerBorderPath.Close();
            canvas.DrawPath(innerBorderPath, strokePaint);
        }

        // Draw Center Clip
        private void DrawCenterClip(SKPaintSurfaceEventArgs args,float scaleOffset)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            var cutoutPercentage = (float)(Option.CutoutPercentage*0.01);

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width / 2, info.Height / 2) - 2 * scaleOffset;

            SKPath clipPath = new SKPath();

            // Clip to create Donut Chart
            clipPath.AddCircle(center.X, center.Y, radius * cutoutPercentage);
            canvas.ClipPath(clipPath, operation: SKClipOperation.Difference);
        }

        public void DrawTitle(SKPaintSurfaceEventArgs args)
        {
            new Title(args,Option.Title).Draw();
        }

        public void DrawDatasets(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            // di stands for dataset index
            for (int di = 0; di < Data.Datasets.Count; di++)
            {

                var dataset = Data.Datasets[di];

                // Get total value of data for one dataset
                double ySum = DatasetUtils.YSum(dataset.Anchors);

                SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);

                // Assign ZRotateAndTranslate To Animate Offset
                // Scale Animation
                float scaleOffset = Option.AnimateScale == true ? ((float)(Option.Scale) * Progress) : 0;
                float radius = Math.Min(info.Width / 2, info.Height / 2) - 2 * scaleOffset;
                float radiusStep =
                    (radius -
                    (Type == PieChartType.Pie ?
                    0 :
                    radius * (float)(Option.CutoutPercentage * 0.01)))
                    / Data.Datasets.Count;

                SKRect outterRect = new SKRect(
                    center.X - radius + di * radiusStep,
                    center.Y - radius + di * radiusStep,
                    center.X + radius - di * radiusStep,
                    center.Y + radius - di * radiusStep);

                // Assign ZRotateAndTranslate To Rotation
                var defaultStartRotation = (float)(Option.Rotation);

                // Rotate Animation
                float startAngle = Option.AnimateRotate ? 0 + (defaultStartRotation * Progress) : defaultStartRotation;
                for (int i = 0; i < dataset.Anchors.Count; i++)
                {
                    var anchor = dataset.Anchors[i];
                    float sweepAngle =
                        Option.AnimateSweep == true ?
                        (float)(360.0f * anchor.Y / ySum) * Progress :
                        (float)(360.0f * anchor.Y / ySum);

                    using (SKPath path = new SKPath())
                    using (SKPaint fillPaint = new SKPaint())
                    using (SKPaint strokePaint = new SKPaint())
                    {
                        path.MoveTo(center);
                        path.ArcTo(outterRect, startAngle, sweepAngle, false);
                        path.Close();

                        fillPaint.Style = SKPaintStyle.Fill;

                        // If there is one backgroundColor,set all backgroundColor to the first one
                        // If there is not coressponding backgroundColor,then use the default #444444
                        SKColor backgroundColor;
                        backgroundColor = dataset.BackgroundColor.Count == 1 ?
                            dataset.BackgroundColor[0] :
                            (i <= dataset.BackgroundColor.Count - 1 ?
                            dataset.BackgroundColor[i] : SKColor.Parse("#444444"));

                        // Assign pie sector backgroundColor
                        fillPaint.Color = backgroundColor;

                        strokePaint.Style = SKPaintStyle.Stroke;

                        // Assign border width
                        strokePaint.StrokeWidth = (float)dataset.BorderWidth;

                        // Assign border color
                        SKColor borderColor;
                        borderColor = dataset.BorderColor.Count == 1 ?
                            dataset.BorderColor[0] :
                            (i <= dataset.BorderColor.Count - 1 ?
                            dataset.BorderColor[i] : SKColor.Parse("#000000"));
                        strokePaint.Color = borderColor;

                        // Calculate "scale" transform
                        float angle = startAngle + 0.5f * sweepAngle;
                        float x = scaleOffset * (float)Math.Cos(Math.PI * angle / 180);
                        float y = scaleOffset * (float)Math.Sin(Math.PI * angle / 180);

                        canvas.Save();
                        canvas.Translate(x, y);

                        // If Donut Clip
                        if (Type == PieChartType.Donut)
                        {
                            DrawCenterClip(args, scaleOffset);
                        }

                        // Fill and Stroke the path
                        canvas.DrawPath(path, fillPaint);
                        canvas.DrawPath(path, strokePaint);

                        // If Donut draw inner path
                        if (Type == PieChartType.Donut)
                        {
                            DrawInnerBorder(
                                args,
                                (float)dataset.BorderWidth,
                                scaleOffset,
                                startAngle,
                                sweepAngle,
                                strokePaint);
                        }

                        canvas.Restore();

                    }

                    startAngle += sweepAngle;
                }
            }
        }

        public void DrawAxis(SKPaintSurfaceEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Invalidate()
        {
            Canvas.InvalidateSurface();
        }

        public void DrawLegends(SKPaintSurfaceEventArgs args)
        {
            var backgroundColor = new List<SKColor>();
            var borderColor = new List<SKColor>();
            var labels = new List<string>();
            foreach (var dataset in Data.Datasets)
            {
                foreach (var bgcolor in dataset.BackgroundColor)
                {
                    backgroundColor.Add(bgcolor);
                }

                foreach (var label in dataset.Labels)
                {
                    labels.Add(label);
                }

                foreach (var borderc in dataset.BorderColor)
                {
                    borderColor.Add(borderc);
                }
            }
            new Legends()
            {
                BackgroundColor = backgroundColor,
                BorderColor = borderColor,
                Labels = labels,
                Args = args
            }.Draw();
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
