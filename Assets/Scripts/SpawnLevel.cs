using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public List<Level> Levels;

    private Level _currentLevel;
    private int _currentLevelNumber = 0;

    private void StartLevel()
    {

    }
}

[System.Serializable]
public class Level
{    
    public List<Wave> Waves;
    public float WaveDelay;
}

[System.Serializable]
public class Wave
{
    public List<GameObject> Template;
    public int Count;
    public float Delay;
}
