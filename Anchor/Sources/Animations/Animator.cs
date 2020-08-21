using System;
using System.Threading.Tasks;

namespace Anchor.Animations
{
    public class Animator
    {
        public const float Frame_Rate = 60.0f;

        public static async void Run(
            Action<float> action,
            IntepolatorType type = IntepolatorType.EaseInOut,
            float minValue = 0.0f,
            float maxValue = 1.0f,
            float duration = 0.25f)
        {

            Intepolatable intepolator = IntepolatorFactory.Create(type, minValue, maxValue, duration);

            while (minValue <= maxValue)
            {
                minValue = intepolator.Next();
                action(minValue);
                await Task.Delay(TimeSpan.FromSeconds(1.0f / Frame_Rate));
            }
        }
    }
}
