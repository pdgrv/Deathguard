using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber;
    private float _timeAfterLastWave;
    private float _waveDelay = 10f;

    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {        
        if (_currentWave == null)
        {
            _timeAfterLastWave += Time.deltaTime;

            if (_currentWaveNumber < _waves.Count - 1)
            {
                if (_timeAfterLastWave >= _waveDelay)
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

    private void LevelComplete()
    {

    }
}