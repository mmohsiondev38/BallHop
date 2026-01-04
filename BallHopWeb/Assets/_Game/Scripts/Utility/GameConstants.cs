using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants
{
    public static string ChangeDifficulty()
    {
        string difficulty = GetDifficulty();
        if (difficulty == "Easy")
        {
            SetDifficulty("Medium");
        }
        else if (difficulty == "Medium")
        {
            SetDifficulty("Hard");
        }
        else if (difficulty == "Hard")
        {
            SetDifficulty("Easy");
        }
        return GetDifficulty();
    }
    public static string GetDifficulty()
    {
        return PlayerPrefs.GetString("Difficulty", "Easy");
    }
    public static void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
    }
}
