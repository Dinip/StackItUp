using System;
using UnityEngine;

public class Utils
{
    public static string ConvertDifficultyToLeaderboard(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return "14537";
            case Difficulty.Medium:
                return "14538";
            case Difficulty.Hard:
                return "14539";
            default:
                return "14537";
        }
    }
}

[Serializable]
public enum Difficulty : int
{
    Easy = 0,
    Medium = 1,
    Hard = 2
}

[Serializable]
public enum MouseMode : int
{
    Normal = 0,
    Toggle = 1,
    Hold = 2,
    Hold_Inv = 3
}

