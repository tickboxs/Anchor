using System;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using System.Collections.Generic;

namespace Anchor.Accessories
{
    public class Legends : IAccessory
    {
        public SKPaintSurfaceEventArgs Args { set; get; }
        public IList<SKColor> BackgroundColor { get; set; }
        public IList<SKColor> BorderColor { get; set; }
        public IList<string> Labels { get; set; }

        public Legends() { }
        public Legends(
            SKPaintSurfaceEventArgs args,
            IList<SKColor> backgroundColor,
            IList<SKColor> borderColor,
            IList<string> labels)
        {
            Args = args;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            Labels = labels;
        }

        public double Height
        {
            get
            {
                // Get Infos
                SKImageInfo info = Args.Info;
                SKSurface surface = Args.Surface;
                SKCanvas canvas = surface.Canvas;

                float x = 0;
                float y = 0;

                float RECT_WIDTH = 50; // Rect Width
                float RECT_HEIGHT = 30; // Rect Height

                float RECT_LABEL_PADDING = 5; // Padding between rect and label
                float RECT_PADDING = 20; // Padding between each rect
                float LINE_PADDING = 20; // padding between line

                float max_h = 0;
                for (int i = 0; i < BackgroundColor.Count; i++)
                {
                    var labelPaint = new SKPaint()
                    {
                        Style = SKPaintStyle.StrokeAndFill,
                        Color = SKColor.Parse("#444444"),
                        TextSize = 30
                    };

                    // Measure Text Bounds
                    var bounds = new SKRect();
                    labelPaint.MeasureText(Labels[i], ref bounds);

                    max_h = bounds.Height;

                    // If exceed screen width,then go to the second line
                    var combine_width = bounds.Width + RECT_WIDTH + RECT_LABEL_PADDING;
                    if (x + combine_width > info.Width)
                    {
                        y += bounds.Height > RECT_HEIGHT ? bounds.Height : RECT_HEIGHT;
                        y += LINE_PADDING;
                        x = 0;
                    }

                    // Offset X
                    x += (RECT_WIDTH + RECT_LABEL_PADDING);

                    // Offset X
                    x += (bounds.Width + RECT_PADDING);
                }

                y += max_h > RECT_HEIGHT ? max_h : RECT_HEIGHT;

                return y;
            }
        }


        public void Draw()
        {

            // Get Infos
            SKImageInfo info = Args.Info;
            SKSurface surface = Args.Surface;
            SKCanvas canvas = surface.Canvas;

            float x = 0;
            float y = 0;

            float RECT_WIDTH = 50; // Rect Width
            float RECT_HEIGHT = 30; // Rect Height

            float RECT_LABEL_PADDING = 5; // Padding between rect and label
            float RECT_PADDING = 20; // Padding between each rect
            float LINE_PADDING = 20; // padding between line
          
            for (int i = 0; i < BackgroundColor.Count; i++)
            {
                var rectFillPaint = new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = BackgroundColor[i]
                };

                var rectStrokePaint = new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    Color = BorderColor[i],
                    StrokeWidth = 1
                };

                var labelPaint = new SKPaint()
                {
                    Style = SKPaintStyle.StrokeAndFill,
                    Color = SKColor.Parse("#444444"),
                    TextSize = 30
                };

                // Measure Text Bounds
                var bounds = new SKRect();
                labelPaint.MeasureText(Labels[i], ref bounds);

                // If exceed screen width,then go to the second line
                var combine_width = bounds.Width + RECT_WIDTH + RECT_LABEL_PADDING;
                if (x + combine_width > info.Width)
                {
                    y += bounds.Height > RECT_HEIGHT ? bounds.Height : RECT_HEIGHT;
                    y += LINE_PADDING;
                    x = 0;
                }

                // Draw Rect
                canvas.DrawRect(x, y, RECT_WIDTH, RECT_HEIGHT, rectFillPaint);
                canvas.DrawRect(x, y, RECT_WIDTH, RECT_HEIGHT, rectStrokePaint);

                // Offset X
                x += (RECT_WIDTH + RECT_LABEL_PADDING);

                // Draw Label
                canvas.DrawText(Labels[i], new SKPoint(x, y + bounds.Height), labelPaint);

                // Offset X
                x += (bounds.Width + RECT_PADDING);

            }
        }
    }
}
