using SkiaSharp;

namespace Anchor.Shapes
{
    public class CircleShape : IShape
    {
        public CircleShape(
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
            // Matrix
            Center = Matrix.MapPoint(Center);

            // Draw Circle
            Canvas.DrawCircle(Center, (float)Radius, FillPaint);
            Canvas.DrawCircle(Center, (float)Radius, StrokePaint);
        }
    }
}
