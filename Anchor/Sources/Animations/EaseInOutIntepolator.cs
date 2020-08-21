
//Math.easeInOutCubic = function(t, b, c, d)
//{
//    t /= d / 2;
//    if (t < 1) return c / 2 * t * t * t + b;
//    t -= 2;
//    return c / 2 * (t * t * t + 2) + b;
//};
using System;

namespace Anchor.Animations
{
    public class EaseInOutIntepolator : Intepolatable
    {

        public EaseInOutIntepolator(
            float minValue,
            float maxValue,
            float duration) :
            base(minValue, maxValue, duration)
        { }

        public override float Next()
        {
            Current_Frame++;

            var D = Animator.Frame_Rate * Duration;
            var T = Current_Frame / D * 0.5f;
            var C = MaxValue - MinValue;
            var B = MinValue;

            if (T < 1)
            {
                return C * 0.5f * (T * T * T) + B;
            }
            else
            {
                T -= 2;
                return C * 0.5f * (T * T * T + 2) + B;
            }
        }
    }
}
