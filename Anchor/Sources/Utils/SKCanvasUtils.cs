using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Anchor.Utils
{
    public class SKCanvasUtils
    {
        public SKPaintSurfaceEventArgs Args { set; get; }

        public SKCanvasUtils(SKPaintSurfaceEventArgs args)
        {
            Args = args;
        }

        // Canvas Width
        public float Width
        {
            get
            {
                return Args.Info.Width;
            }
        }

        // Canvas Height
        public float Height
        {
            get
            {
                return Args.Info.Height;
            }
        }

        // Canvas Center
        public SKPoint Center
        {
            get
            {
                return new SKPoint((float)Width / 2, (float)Height / 2);
            }
        }

        // Canvas Radius
        public float Radius
        {
            get
            {
                return Math.Min(Width / 2, Height / 2);
            }
        }

        // Get canvas
        public SKCanvas Canvas
        {
            get
            {
                return Args.Surface.Canvas;
            }
        }

    }
}
