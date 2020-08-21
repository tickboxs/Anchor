namespace Anchor.Domains.PieChart
{
    public class PieChartOption
    {
        public PieChartOption() { }

        public PieChartOption(
            double cutoutPercentage = 30, // 0-100 represent percentage
            double rotation = -30, // angle ex 0-360
            double circumference = 360, // angle ex 0-360
            bool animateRotate = true,
            double scale = 20,
            bool animateScale = false
            )
        {
            CutoutPercentage = cutoutPercentage;
            Scale = scale;
            Rotation = rotation;
            Circumference = circumference;
            AnimateRotate = animateRotate;
            AnimateScale = animateScale;

        }

        // Default 50 - for doughnut, 0 - for pie
        // The percentage of the chart that is cut out of the middle.
        public double CutoutPercentage { private set; get; } = 50;

        // Default -30
        // Starting angle to draw arcs from.
        public double Rotation { private set; get; } = -30;

        // Default 20
        // Scaling the chart from the center outwards.
        public double Scale { private set; get; } = 20;

        // Default 360
        // Sweep to allow arcs to cover.
        public double Circumference { private set; get; } = 360;

        // Default false
        // If true, the chart will animate in with a rotation animation. This property is in the options.animation object.
        public bool AnimateRotate { private set; get; } = false;

        // Default false
        // If true, will animate scaling the chart from the center outwards.
        public bool AnimateScale { private set; get; } = false;

        // Default true
        // If true, will animate sector sweep
        public bool AnimateSweep { private set; get; } = true;

        // Default 
        // Chart Title
        public ChartTitle Title { set; get; } = new ChartTitle();

    }
}
