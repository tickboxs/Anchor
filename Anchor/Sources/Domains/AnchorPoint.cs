using System;
namespace Anchor.Domains
{
    public class AnchorPoint
    {

        public float X { private set; get; }
        public float Y { private set; get; }
        public float R { private set; get; }

        public AnchorPoint(float x = 0, float y = 0, float r = 0)
        {
            X = x;
            Y = y;
            R = r;
        }
    }
}

