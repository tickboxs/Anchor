using System;
namespace Anchor.Charts
{
    public abstract class IChartAnimatable
    {

        // Progress
        protected float Progress { set; get; } = 1.0f;

        // Animate Chart
        public abstract void Animate(float duration = 0.5f);

    }
}
