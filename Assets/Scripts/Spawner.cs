using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;

    public event UnityAction<int> LevelComplete;

    private Level _currentLevel;
    private int _currentLevelNumber;

    private List<Wave> _waves;
    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _timeAfterLastWave;

    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Update()
    {        
        if (_currentWave == null) //если волна полностью заспавнилась
        {
            _timeAfterLastWave += Time.deltaTime;

            if (_currentWaveNumber < _waves.Count - 1)
            {
                if (_timeAfterLastWave >= _currentLevel.WaveDelay)
                {
                    NextWave();
                    _timeAfterLastWave = 0;
                }
            }
            else
            {
                //if all enemy killed
                //levelcomplete
                //
            }

            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_spawned >= _currentWave.Count)
        {
            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        GameObject template = _currentWave.Template[Random.Range(0, _currentWave.Template.Count)];

        Enemy enemy = Instantiate(template, spawnPoint.transform).GetComponent<Enemy>();
        enemy.Init(_player);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void SetLevel(int index)
    {
        _currentLevel = _levels[index];
        _waves = _currentLevel.Waves;
    }

    public void NextLevel()
    {
        SetLevel(_currentLevelNumber);
    }

    public void StartLevel()
    {
        _waves = _currentLevel.Waves;
        SetWave(0);
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