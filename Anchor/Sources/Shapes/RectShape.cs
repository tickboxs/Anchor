using SkiaSharp;

namespace Anchor.Shapes
{
    public class RectShape : IShape
    {
        public RectShape(
            SKPoint center,
            double radius,
            SKCanvas canvas,
            SKPaint fillPaint,
            SKPaint strokePaint,
            SKMatrix matrix) :
            base(center, radius, canvas, fillPaint, strokePaint, matrix)
        {

        }

        public override void Draw()
        {
            Canvas.DrawRect(
                Center.X - (float)Radius,
                Center.Y - (float)Radius,
                2 * (float)Radius,
                2 * (float)Radius,
                FillPaint);

            Canvas.DrawRect(
                Center.X - (float)Radius,
                Center.Y - (float)Radius,
                2 * (float)Radius,
                2 * (float)Radius,
                StrokePaint);
        }
    }
}
