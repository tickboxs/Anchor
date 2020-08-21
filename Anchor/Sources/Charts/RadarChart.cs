using System;
using Anchor.Interfaces;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Anchor.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anchor.Domains.RadarChart;
using Anchor.Shapes;
using Anchor.Utils;
using Anchor.Styles;
using Anchor.Accessories;
using Anchor.Animations;

namespace Anchor.Charts
{
    public class RadarChart : IChartAnimatable, IChart
    {
        public RadarChartData Data { private set; get; }
        private SKCanvasView Canvas { set; get; }
        private RadarChartOption Option { set; get; }

        public RadarChart(
            RadarChartData data,
            SKCanvasView canvasView,
            RadarChartOption option)
        {
            Data = data;
            Canvas = canvasView;
            Option = option;
        }

        public void Draw(SKPaintSurfaceEventArgs args)
        {
            // Clear Canvas Before Actual Draw
            args.Surface.Canvas.Clear();

            // Draw Axis
            // Web and Scale
            DrawAxis(args);

            // Draw datasets
            DrawDatasets(args);

            // Draw Title
            DrawTitle(args);

            // Draw Legends
            DrawLegends(args);

        }
        

        public void Invalidate()
        {
            Canvas.InvalidateSurface();
        }

        private void DrawWeb(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            SKCanvasUtils canvasUtils = new SKCanvasUtils(args);
            SKPoint center = canvasUtils.Center;
            float radius = (float)canvasUtils.Radius;
            SKCanvas canvas = canvasUtils.Canvas;

            // Draw background Web
            SKPaint webPaint = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#444444")
            };

            // Angle
            var angleStep = 360.0/ (Data.Labels.Count);

            // Rotate Matrix
            SKMatrix matrix = SKMatrix.CreateRotationDegrees((float)angleStep, center.X, center.Y);

            // Top Point
            SKPoint top = new SKPoint(center.X, center.Y - radius);

            foreach (var label in Data.Labels)
            {
                canvas.DrawLine(center, top, webPaint);
                top = matrix.MapPoint(top);
            }

            // Calculate web density according to Option
            var densityCount = Math.Floor(radius / Option.Density);
            var densityStep = radius / densityCount;

            top = new SKPoint(center.X, center.Y - radius);

            for (int i = 0; i < densityCount; i++)
            {
                var movingPoint = new SKPoint(top.X,top.Y);
                foreach (var label in Data.Labels)
                {
                    canvas.DrawLine(movingPoint, matrix.MapPoint(movingPoint), webPaint);
                    movingPoint = matrix.MapPoint(movingPoint);
                }

                top = new SKPoint(center.X, center.Y - radius + (float)densityStep * (i+1));
            }
        }

        private void DrawScale(SKPaintSurfaceEventArgs args)
        {
            // Get Infos
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width / 2, info.Height / 2);

            // Get anchor span
            // Calculate Max
            var anchorPointsList = new List<IList<AnchorPoint>>();
            foreach (RadarChartDataset dataset in Data.Datasets)
            {
                anchorPointsList.Add(dataset.Anchors);
            }

            double max = DatasetUtils.YMax(anchorPointsList);

            var span = max - 0;

            // Calculate web density according to Option
            var densityCount = Math.Floor(radius / Option.Density);
            var scaleStep = MathUtils.NearestFibonacci(span / densityCount);
            var positionStep = (radius / densityCount);

            for (int i = 0; i <= densityCount; i++)
            {
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

        public void DrawTitle(SKPaintSurfaceEventArgs args)
        {
            var canvas = new SKCanvasUtils(args).Canvas;
            var center = new SKCanvasUtils(args).Center;
            var titlePaint = OptionUtils.TitlePaint(Option);

            // Get the bounds of textPathPaint
            SKRect textPathPaintBounds = new SKRect();
            titlePaint.MeasureText(Option.Title.Text, ref textPathPaintBounds);
            float x_offset = textPathPaintBounds.Width * 0.5f;
            // Offset Title center_x align to screen center_x
            float y_offset = (float)Option.Title.Padding;
            var point = new SKPoint(center.X - x_offset, y_offset);

            canvas.DrawText(Option.Title.Text, point, titlePaint);
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
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(info.Width / 2, info.Height / 2);

            // Draw Datasets
            // Calculate Max
            var anchorPointsList = new List<IList<AnchorPoint>>();
            foreach (var dataset in Data.Datasets)
            {
                anchorPointsList.Add(dataset.Anchors);
            }
            double max = DatasetUtils.YMax(anchorPointsList);

            // Calculate dimensionRatio
            double dr = radius / max;

            // Angle
            var angleStep = 360.0 / (Data.Labels.Count);

            for (int di = 0; di < Data.Datasets.Count; di++)
            {
                // Dataset
                var dataset = Data.Datasets[di];
                var radarStyle = new RadarChartStyle(dataset);

                SKPaint fillPaint = radarStyle.FillPaint();

                SKPaint strokePaint = radarStyle.StrokePaint();

                var top = new SKPoint(
                    center.X,
                    center.Y - (float)dr * (float)(dataset.Anchors[0].Y));

                SKPath path = new SKPath();

                var pointFillPaint = radarStyle.PointFillPaint();
                var pointStrokePaint = radarStyle.StrokePaint();

                ShapeFactory.Create(
                    dataset.PointStyle,
                    top,
                    dataset.PointRadius,
                    canvas,
                    pointFillPaint,
                    pointStrokePaint,
                    SKMatrix.Identity)
                    .Draw();

                for (int i = 0; i < Data.Labels.Count; i++)
                {
                    if (i == 0)
                    {
                        path.MoveTo(top);
                    }
                    else
                    {
                        var anchor = dataset.Anchors[i];
                        top = new SKPoint(
                            center.X,
                            center.Y - ((float)dr * (float)anchor.Y) * Progress);

                        // Rotate Matrix
                        SKMatrix matrix = SKMatrix.CreateRotationDegrees((float)angleStep * i, center.X, center.Y);
                        var next = matrix.MapPoint(top);
                        path.LineTo(next);

                        ShapeFactory.Create(
                            dataset.PointStyle,
                            next,
                            dataset.PointRadius,
                            canvas,
                            pointFillPaint,
                            pointStrokePaint,
                            SKMatrix.Identity)
                            .Draw();

                    }


                }

                path.Close();

                canvas.DrawPath(path, fillPaint);
                canvas.DrawPath(path, strokePaint);

            }
        }

        public void DrawAxis(SKPaintSurfaceEventArgs args)
        {
            // Draw background web
            DrawWeb(args);

            // Draw scale
            DrawScale(args);
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
