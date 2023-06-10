using System;

namespace Helpers.Enums
{
    [Flags]
    public enum GameState
    {
        Default = 0,
        Playing = 1,
        Moving =2,
        Pause = 4,
        Lose = 8,
        Win = 16
    }
}

