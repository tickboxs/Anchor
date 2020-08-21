
using SkiaSharp;
using SkiaSharp.Views.Forms;


namespace Anchor.Interfaces
{
    public interface IChart
    {
        // Draw Chart Title
        void DrawTitle(SKPaintSurfaceEventArgs args);

        // Draw Chart Legends
        void DrawLegends(SKPaintSurfaceEventArgs args);

        // Draw Datasets
        void DrawDatasets(SKPaintSurfaceEventArgs args);

        // Draw Axis
        void DrawAxis(SKPaintSurfaceEventArgs args);

        // Draw Entry 
        void Draw(SKPaintSurfaceEventArgs args);

        // Force Canvas to redraw
        void Invalidate();
    }
}
