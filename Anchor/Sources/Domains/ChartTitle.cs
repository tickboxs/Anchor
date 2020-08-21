using System;
using SkiaSharp;

namespace Anchor.Domains
{
    public class ChartTitle
    {
        public ChartTitle()
        {

        }

        // Default false
        // Is the title shown?
        public bool Display { set; get; } = false;

        // Default ""
        // Title text to display. If specified as an array, text is rendered on multiple lines.
        public string Text { set; get; } = "";

        // Default .top
        // Position of title.
        public ChartTitlePosition Position { set; get; }

        // Default 12
        // Font size
        public float FontSize { set; get; } = 30;

        // Font family for the title text.
        public string FontFamily { set; get; }

        // Default #666
        // Font color.
        public SKColor FontColor { set; get; } = SKColor.Parse("#000000");

        // Font style.
        public string FontStyle { set; get; }

        // Default 10
        // Number of pixels to add above and below the title text.
        public double Padding { set; get; } = 20;

        // Default 1.2
        // Height of an individual line of text. See
        public double LineHeight { set; get; }







    }
}
