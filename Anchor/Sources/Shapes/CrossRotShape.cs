using SkiaSharp;

namespace Anchor.Shapes
{
    public class CrossRotShape : IShape
    {
        public CrossRotShape(
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
            // Rotate 45 degress matrix
            var matrix = SKMatrix.CreateRotationDegrees(45, Center.X, Center.Y);

            SKPoint bottom = matrix.MapPoint(Center.X, Center.Y - (float)Radius);
            SKPoint top = matrix.MapPoint(Center.X, Center.Y + (float)Radius);
            SKPoint left = matrix.MapPoint(Center.X - (float)Radius, Center.Y);
            SKPoint right = matrix.MapPoint(Center.X + (float)Radius,Center.Y);

            // Matrix
            SKPoint matrixedBottom = Matrix.MapPoint(bottom);
            SKPoint matrixedTop = Matrix.MapPoint(top);
            SKPoint matrixedLeft = Matrix.MapPoint(left);
            SKPoint matrixedRight = Matrix.MapPoint(right);
            
            Canvas.DrawLine(matrixedBottom, matrixedTop, StrokePaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, StrokePaint);
            Canvas.DrawLine(matrixedBottom, matrixedTop, FillPaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, FillPaint);
        }
    }
}
