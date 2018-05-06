using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Added.Recaptcha
{
    public enum Version
    {
        v2 = 2,
        v3 = 3
    }

    public enum RenderMode
    {
        OnLoad = 0,
        Explicit = 1
    }

    public enum Theme
    {
        Light = 0,
        Dark = 1
    }

    public enum BadgeLocation
    {
        Inline = -1,
        BottomRight = 0,
        BottomLeft = 1,
    }

    public enum Size
    {
        Invisible = -1,
        Normal = 0,
        Compact = 1
    }

    public enum ScreenSide
    {
        Right = 0,
        Left = 1,
    }
}
