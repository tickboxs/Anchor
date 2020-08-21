using System;
using SkiaSharp.Views.Forms;

namespace Anchor.Accessories
{
    public interface IAccessory
    {
        void Draw();
        double Height { get; }
    }
}
