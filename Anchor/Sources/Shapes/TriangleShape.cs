
using SkiaSharp;

namespace Anchor.Shapes
{
    public class TriangleShape : IShape
    {
        public TriangleShape(
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
            var matrix = SKMatrix.CreateRotationDegrees(120, Center.X, Center.Y);

            var top = new SKPoint(Center.X, Center.Y - (float)Radius);
            var next = matrix.MapPoint(top);
            var last = matrix.MapPoint(next);
            var path = new SKPath();

            var matrixedTop = Matrix.MapPoint(top);
            var matrixedNext = Matrix.MapPoint(next);
            var matrixedLast = Matrix.MapPoint(last);

            path.MoveTo(matrixedTop);
            path.LineTo(matrixedNext);
            path.LineTo(matrixedLast);
            path.Close();

            Canvas.DrawPath(path, FillPaint);
            Canvas.DrawPath(path, StrokePaint);

        }
    }
}
