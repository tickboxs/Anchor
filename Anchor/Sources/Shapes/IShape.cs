
using SkiaSharp;
namespace Anchor.Shapes
{
    abstract public class IShape
    {
        protected SKPoint Center { set; get; }
        protected double Radius { set; get; }
        protected SKCanvas Canvas { set; get; }
        protected SKPaint FillPaint { set; get; }
        protected SKPaint StrokePaint { set; get; }
        protected SKMatrix Matrix { set; get; }

        public IShape(
            SKPoint center,
            double radius,
            SKCanvas canvas,
            SKPaint fillPaint,
            SKPaint strokePaint,
            SKMatrix matrix)
        {
            Center = center;
            Radius = radius;
            Canvas = canvas;
            FillPaint = fillPaint;
            StrokePaint = strokePaint;
            Matrix = matrix;
        }

        abstract public void Draw();
    }
}
