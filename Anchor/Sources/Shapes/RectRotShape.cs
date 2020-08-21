using SkiaSharp;

namespace Anchor.Shapes
{
    public class RectRotShape : IShape
    {
        public RectRotShape(
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
            Canvas.RotateDegrees(45, Center.X, Center.Y);

            var rect = new SKRect(
                Center.X - (float)Radius,
                Center.Y - (float)Radius,
                Center.X + (float)Radius,
                Center.Y + (float)Radius);

            Canvas.DrawRect(
                rect,
                FillPaint);

            Canvas.DrawRect(
                rect,
                StrokePaint);
            Canvas.ResetMatrix();
        }
    }
}
