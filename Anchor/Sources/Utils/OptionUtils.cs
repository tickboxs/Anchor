using System;
using Anchor.Domains.PolarChart;
using SkiaSharp;
using Anchor.Domains.RadarChart;
namespace Anchor.Utils
{
    public class OptionUtils
    {
        public static SKPaint TitlePaint(PolarChartOption option)
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = option.Title.FontColor,
                TextSize = (float)option.Title.FontSize
            };

        }

        public static SKPaint TitlePaint(RadarChartOption option)
        {
            return new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = option.Title.FontColor,
                TextSize = (float)option.Title.FontSize
            };

        }
    }
}
