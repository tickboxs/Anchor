
using System.Collections.Generic;
using Anchor.Domains;

namespace Anchor.Utils
{
    public class DatasetUtils
    {

        // Sum Y
        public static float YSum(IList<AnchorPoint> anchorPoints)
        {
            float sum = 0;
            foreach (var anchorPoint in anchorPoints)
            {
                sum += anchorPoint.Y;
            }

            return sum;  
        }

        // Sum Y in mutiple Dataset
        public static float YSum(IList<IList<AnchorPoint>> anchorPointsList)
        {
            float sum = 0;
            foreach (var anchorPoints in anchorPointsList)
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    sum += anchorPoint.Y;
                }
            }
            
            return sum;
        }

        // Max X in mutiple Dataset
        public static float XMax(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var max_x = -float.MaxValue;
            foreach (var anchorPoints in anchorPointsList)
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    max_x = max_x < anchorPoint.X ? anchorPoint.X : max_x;
                }
            }

            return max_x;
        }

        // Max X
        public static float XMax(IList<AnchorPoint> anchorPoints)
        {
            var max_x = -float.MaxValue;
            foreach (var anchorPoint in anchorPoints)
            {
                max_x = max_x < anchorPoint.X ? anchorPoint.X : max_x;
            }

            return max_x;
        }

        // Min X in mutiple Dataset
        public static float XMin(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var min_x = float.MaxValue;
            foreach (var anchorPoints in anchorPointsList)
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    min_x = min_x > anchorPoint.X ? anchorPoint.X : min_x;
                }
            }

            return min_x;
        }

        // Min X
        public static float XMin(IList<AnchorPoint> anchorPoints)
        {
            var min_x = float.MaxValue;
            foreach (var anchorPoint in anchorPoints)
            {
                min_x = min_x > anchorPoint.X ? anchorPoint.X : min_x;
            }
            return min_x;
        }

        // Max Y in mutiple Dataset
        public static float YMax(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var max_y = -float.MaxValue;
            foreach (var anchorPoints in anchorPointsList)
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    max_y = max_y < anchorPoint.Y ? anchorPoint.Y : max_y;
                }
            }

            return max_y;
        }

        // Max X
        public static float YMax(IList<AnchorPoint> anchorPoints)
        {
            var max_y = -float.MaxValue;
            foreach (var anchorPoint in anchorPoints)
            {
                max_y = max_y < anchorPoint.Y ? anchorPoint.Y : max_y;
            }

            return max_y;
        }

        // Min Y in mutiple Dataset
        public static float YMin(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var min_y = float.MaxValue;
            foreach (var anchorPoints in anchorPointsList)
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    min_y = min_y > anchorPoint.Y ? anchorPoint.Y : min_y;
                }
            }

            return min_y;
        }

        // Min Y
        public static float YMin(IList<AnchorPoint> anchorPoints)
        {
            var min_y = float.MaxValue;
            foreach (var anchorPoint in anchorPoints)
            {
                min_y = min_y > anchorPoint.Y ? anchorPoint.Y : min_y;
            }

            return min_y;
        }

        // Span X in mutiple Dataset
        public static float XSpan(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var max_x = XMax(anchorPointsList);
            var min_x = XMin(anchorPointsList);
            return max_x - min_x;
        }

        // Span Y in mutiple Dataset
        public static float YSpan(IList<IList<AnchorPoint>> anchorPointsList)
        {
            var max_y = YMax(anchorPointsList);
            var min_y = YMin(anchorPointsList);
            return max_y - min_y;
        }

        // Span X
        public static float XSpan(IList<AnchorPoint> anchorPoints)
        {
            var max_x = XMax(anchorPoints);
            var min_x = XMin(anchorPoints);
            return max_x - min_x;
        }

        // Span Y
        public static float YSpan(IList<AnchorPoint> anchorPoints)
        {
            var max_y = YMax(anchorPoints);
            var min_y = YMin(anchorPoints);
            return max_y - min_y;
        }
    }
}
