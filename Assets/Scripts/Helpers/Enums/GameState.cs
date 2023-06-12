using System;

namespace Helpers.Enums
{
    [Flags]
    public enum GameState
    {
        Default = 0,
        Playing = 1,
        Moving =2,
        Dropping = 4,
        Pause = 8,
        Lose = 16,
        Win = 32
    }
}

