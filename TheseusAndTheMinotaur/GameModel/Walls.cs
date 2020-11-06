using System;

namespace TaM
{
    [Flags]
    enum Walls
    {
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
    }
}
