//Math.easeOutCubic = function(t, b, c, d)
//{
//    t /= d;
//    t--;
//    return c * (t * t * t + 1) + b;
//};
namespace Anchor.Animations
{
    public class EaseOutIntepolator : Intepolatable
    {
        public EaseOutIntepolator(
            float minValue,
            float maxValue,
            float duration) :
            base(minValue, maxValue, duration)
        { }

        public override float Next()
        {
            Current_Frame++;

            var D = Animator.Frame_Rate * Duration;
            var T = Current_Frame / D - 1.0f;
            var B = MinValue;
            var C = MaxValue - MinValue;
            return C * (T * T * T + 1.0f) + B;
        }
    }
}

