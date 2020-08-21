using SkiaSharp;

namespace Anchor.Shapes
{
    public class CrossShape : IShape
    {
        public CrossShape(
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

            SKPoint bottom = new SKPoint(Center.X, Center.Y - (float)Radius);
            SKPoint top = new SKPoint(Center.X, Center.Y + (float)Radius);
            SKPoint left = new SKPoint(Center.X - (float)Radius, Center.Y);
            SKPoint right = new SKPoint(Center.X + (float)Radius, Center.Y);

            // Matrixed 
            SKPoint matrixedbottom = Matrix.MapPoint(bottom);
            SKPoint matrixedTop = Matrix.MapPoint(top);
            SKPoint matrixedLeft = Matrix.MapPoint(left);
            SKPoint matrixedRight = Matrix.MapPoint(right);

            Canvas.DrawLine(matrixedbottom, matrixedTop, StrokePaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, StrokePaint);
            Canvas.DrawLine(matrixedbottom, matrixedTop, FillPaint);
            Canvas.DrawLine(matrixedLeft, matrixedRight, FillPaint);
        }
    }
}
