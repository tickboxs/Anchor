using System;
namespace Anchor.Animations
{
    public class IntepolatorFactory
    {
        public IntepolatorFactory()
        {
        }

        public static Intepolatable Create(IntepolatorType type,float minValue, float maxValue, float duration)
        {
            switch (type)
            {
                case IntepolatorType.Linear:
                    return new LinearIntepolator(minValue, maxValue, duration);

                case IntepolatorType.EaseIn:
                    return new EaseInIntepolator(minValue, maxValue, duration);

                case IntepolatorType.EaseOut:
                    return new EaseOutIntepolator(minValue, maxValue, duration);

                case IntepolatorType.EaseInOut:
                    return new EaseInOutIntepolator(minValue, maxValue, duration);

                default:
                    return new LinearIntepolator(minValue, maxValue, duration);
            }
        }
    }
}
