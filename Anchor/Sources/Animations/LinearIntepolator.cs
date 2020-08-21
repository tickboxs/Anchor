using System;
namespace Anchor.Animations
{
    public class LinearIntepolator : Intepolatable
    {
        public LinearIntepolator(
            float minValue,
            float maxValue,
            float duration) :
            base(minValue, maxValue, duration)
        { }

        public override float Next()
        {
            Current_Frame++;

            var D = Animator.Frame_Rate * Duration;
            var T = Current_Frame;
            var C = MaxValue - MinValue;
            var B = MinValue;

            return C * T / D + B;
        }
    }
}
