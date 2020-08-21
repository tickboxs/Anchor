using System;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using Anchor.Utils;
using Anchor.Domains;

namespace Anchor.Accessories
{
    public class Title : IAccessory
    {

        public ChartTitle ChartTitle { get; set; } = new ChartTitle();
        public SKPaintSurfaceEventArgs Args { set; get; }

        public SKPaint TitlePaint
        {
            get
            {
                return new SKPaint()
                {
                    Color = ChartTitle.FontColor,
                    TextSize = ChartTitle.FontSize
                };
            }
        }

        public double Height
        {
            get
            {
                // Get the bounds of textPathPaint
                SKRect titlePathPaintBounds = new SKRect();
                TitlePaint.MeasureText(ChartTitle.Text, ref titlePathPaintBounds);

                return titlePathPaintBounds.Height + 2 * ChartTitle.Padding;
            }
        }

        public Title(SKPaintSurfaceEventArgs args,ChartTitle chartTitle)
        {
            Args = args;
            ChartTitle = chartTitle;
        }

        public void Draw()
        {
            // Not draw if display == false
            if (ChartTitle.Display == false) { return; }

            // Get Infos
            var canvasUtils = new SKCanvasUtils(Args);
            var canvas = canvasUtils.Canvas;
            var center = canvasUtils.Center;

            // Get the bounds of textPathPaint
            SKRect titlePathPaintBounds = new SKRect();
            TitlePaint.MeasureText(ChartTitle.Text, ref titlePathPaintBounds);
            float x_offset = titlePathPaintBounds.Width * 0.5f;

            // Offset Title center_x align to screen center_x
            center.X = center.X - x_offset;

            canvas.DrawText(ChartTitle.Text, center, TitlePaint);
        }

    }
}
