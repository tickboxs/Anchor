using SkiaSharp;

namespace Anchor.Shapes
{
    public class ShapeFactory
    {

        public static IShape Create(
            ShapeType type,
            SKPoint center,
            double radius,
            SKCanvas canvas,
            SKPaint fillPaint,
            SKPaint strokePaint,
            SKMatrix matrix)
        {
            switch (type)
            {
                case ShapeType.Circle:
                    return new CircleShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.Cross:
                    return new CrossShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.CrossRot:
                    return new CrossRotShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.Rect:
                    return new RectShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.RectRounded:
                    return new RectRoundedShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.RectRot:
                    return new RectRotShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.Triangle:
                    return new TriangleShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                case ShapeType.Star:
                    return new StarShape(center, radius, canvas, fillPaint, strokePaint, matrix);
                default:
                    return new CircleShape(center, radius, canvas, fillPaint, strokePaint, matrix);
            }
        }

    }
}
