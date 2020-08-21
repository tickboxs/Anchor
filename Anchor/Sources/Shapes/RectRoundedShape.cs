using SkiaSharp;

namespace Anchor.Shapes
{
    public class RectRoundedShape : IShape
    {
        public RectRoundedShape(
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
            // By default radius for rect is 0.5 * radius of dataset
            float rectRadius = 0.5f * (float)Radius;
            var rect = new SKRoundRect(new SKRect(
                Center.X - (float)Radius,
                Center.Y - (float)Radius,
                Center.X + (float)Radius,
                Center.Y + (float)Radius), rectRadius);
            
            Canvas.DrawRoundRect(rect, FillPaint);
            Canvas.DrawRoundRect(rect, StrokePaint);

        }
    }
}
