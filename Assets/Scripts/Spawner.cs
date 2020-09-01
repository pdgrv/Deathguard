using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _endPanel;    

    private Level _currentLevel;
    private int _levelNumber;

    private List<Wave> _waves;
    private Wave _currentWave;
    private int _waveNumber;
    private float _timeAfterLastWave;

    private int _spawned;
    private float _timeAfterLastSpawn;
    private bool _isStopSpawn = true;
    private int _enemyAlive;

    public event UnityAction<int> LevelComplete;

    private void Start()
    {
        SetLevel(0);
    }

    private void Update()
    {
        if (_isStopSpawn)
            return;

        if (_spawned >= _currentWave.Count)
        {
            if (_waveNumber + 1 < _waves.Count)
            {
                _timeAfterLastWave += Time.deltaTime;

                if (_timeAfterLastWave >= _currentLevel.WaveDelay)
                {
                    _timeAfterLastWave = 0;

                    SetWave(++_waveNumber);
                }
            }
            else if (_enemyAlive == 0)
            {
                _isStopSpawn = true;
                LevelComplete?.Invoke(_levelNumber + 1);

                if (_levelNumber + 1 < _levels.Count)
                    SetLevel(++_levelNumber);
                else
                {
                    _menu.OpenPanel(_endPanel);
                    Debug.Log("Все уровни пройдены!");
                }
            }
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            _timeAfterLastSpawn = 0;

            InstantiateEnemy();
            _spawned++;
        }
    }

    private void InstantiateEnemy()
    {
        _enemyAlive++;
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)]; 
        GameObject template = _currentWave.Template;

        Enemy enemy = Instantiate(template, spawnPoint.transform).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _waveNumber = index;

        _spawned = 0;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _enemyAlive--;

        _player.AddReward(enemy.Exp, enemy.Money);
    }

    private void SetLevel(int index)
    {
        _currentLevel = _levels[index];
        _levelNumber = index;

        _waves = _currentLevel.Waves;
        _waveNumber = 0;
    }

    public void StartLevel()
    {        
        SetWave(0);
        _isStopSpawn = false;
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
    public GameObject Template;
    public int Count;
    public float Delay;
}