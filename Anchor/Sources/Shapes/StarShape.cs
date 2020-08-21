using SkiaSharp;

namespace Anchor.Shapes
{
    public class StarShape : IShape
    {
        public StarShape(
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

            SKMatrix matrixPositive = SKMatrix.CreateRotationDegrees(45, Center.X, Center.Y);
            SKMatrix matrixNegtive = SKMatrix.CreateRotationDegrees(-45, Center.X, Center.Y);

            SKPoint bottom = new SKPoint(Center.X, Center.Y - (float)Radius);
            SKPoint top = new SKPoint(Center.X, Center.Y + (float)Radius);
            SKPoint left = new SKPoint(Center.X - (float)Radius, Center.Y);
            SKPoint right = new SKPoint(Center.X + (float)Radius, Center.Y);

            SKPoint topLeft = matrixPositive.MapPoint(top);
            SKPoint topRight = matrixNegtive.MapPoint(top);
            SKPoint bottomLeft = matrixNegtive.MapPoint(bottom);
            SKPoint bottomRight = matrixPositive.MapPoint(bottom);

            // Matrix
            SKPoint matrixedBottom = Matrix.MapPoint(bottom);
            SKPoint matrixedTop = Matrix.MapPoint(top);
            SKPoint matrixedLeft = Matrix.MapPoint(left);
            SKPoint matrixedRight = Matrix.MapPoint(right);

            SKPoint matrixedTopLeft = Matrix.MapPoint(topLeft);
            SKPoint matrixedTopRight = Matrix.MapPoint(topRight);
            SKPoint matrixedBottomLeft = Matrix.MapPoint(bottomLeft);
            SKPoint matrixedBottomRight = Matrix.MapPoint(bottomRight);

            Canvas.DrawLine(matrixedBottom, matrixedTop, StrokePaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, StrokePaint);
            Canvas.DrawLine(matrixedBottom, matrixedTop, FillPaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, FillPaint);

            Canvas.DrawLine(matrixedTopLeft, matrixedBottomRight, StrokePaint);
            Canvas.DrawLine(matrixedTopRight, matrixedBottomLeft, StrokePaint);
            Canvas.DrawLine(matrixedTopLeft, matrixedBottomRight, FillPaint);
            Canvas.DrawLine(matrixedTopRight, matrixedBottomLeft, FillPaint);
        }
    }
}
