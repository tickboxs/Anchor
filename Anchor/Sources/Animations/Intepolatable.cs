using System;
namespace Anchor.Animations
{
    public abstract class Intepolatable
    {

        protected float Current_Frame = 0.0f;

        protected float MinValue { set; get; }
        protected float MaxValue { set; get; }
        protected float Duration { set; get; }

        public Intepolatable(float minValue, float maxValue, float duration)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Duration = duration;
        }

        public abstract float Next();
    }
}
