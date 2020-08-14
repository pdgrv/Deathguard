using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            _currentWave = null;

            if (_currentWaveNumber < _waves.Count - 1)
                NextWave();
        }
    }

    private void InstantiateEnemy()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Enemy enemy = Instantiate(_currentWave.Template, spawnPoint.transform).GetComponent<Enemy>();
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
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public int Count;
    public float Delay;
}