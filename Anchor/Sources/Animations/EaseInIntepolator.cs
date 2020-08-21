

//Math.easeInCubic = function(t, b, c, d)
//{
//    t /= d;
//    return c * t * t * t + b;
//};
namespace Anchor.Animations
{
    public class EaseInIntepolator : Intepolatable
    {

        public EaseInIntepolator(
            float minValue,
            float maxValue,
            float duration) :
            base(minValue, maxValue, duration) { }

        public override float Next()
        {
            {
                Current_Frame++;

                var D = Animator.Frame_Rate * Duration;
                var T = Current_Frame / D;
                var B = MinValue;
                var C = MaxValue - MinValue;
                return T * T * T * C + B;
            }
        }
    }
}