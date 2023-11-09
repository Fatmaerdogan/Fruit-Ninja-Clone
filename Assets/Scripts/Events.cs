
using System;
using UnityEngine;

public class Events
{
    public static Action<Target,Vector3> OnTargetHit;
    public static Action<int, int> UpdateScore;
    public static Action<int> UpdateLives;
    public static Action<bool> DisplayMainMenu;
    public static Action<bool> DisplayGameMenu;
}

[System.Serializable]
public enum State { MainMenu, PauseMenu, Playing };

[System.Serializable]
public enum SpawnOption
{
    RandomWave,
    WaveAllAtOnce,
    WaveOneByOne,
    OneAtOnce
}
