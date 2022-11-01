using System.Collections;
using UnityEngine;

public class Spawn_Mananger : MonoBehaviour
{
    [SerializeField] private bool _startTimer = false;
    [SerializeField] private float _difficultyTimer;
    [SerializeField] private bool _increaseDifficulty;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] private bool _stopspawing = false;
    [SerializeField] GameObject[] _enemy;
    [SerializeField] float _spawnEnemyTimer;
    [SerializeField] private int _newEnemy = 0;
    [SerializeField] GameObject[] _powerUps;
    [SerializeField] float _rarePowerUpTimer;
    [SerializeField] float _powerUpTimer;
    [SerializeField] GameObject[] _waves;
    [SerializeField] private int _amountSpawnPerWave = 1;
    GameObject[] _enemyWave;

    private Vector3 _spawnPOS;
    private void Update()
    {
        IncreaseDifficulty();
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(RareSpawnPowerUp());
        StartTimer();
    }

    IEnumerator SpawnEnemyRoutine()
    {
       
        yield return new WaitForSeconds(3);
        while (_stopspawing == false)
        {
            while (!_increaseDifficulty)
            {
              yield return new WaitForSeconds(_spawnEnemyTimer);
                for(int i = 0; i < _amountSpawnPerWave; i++) 
                { 
                     _spawnPOS = new Vector3(Random.Range(-9.0f, 9.0f),Random.Range(5f,9.0f), 0);
                    GameObject newEnemy = Instantiate(_enemy[Random.Range(0,_newEnemy + 1)], _spawnPOS, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                }
            } 
            _amountSpawnPerWave++;
            if(_newEnemy < _enemy.Length - 1)
            {
                _newEnemy++;
            }
            _increaseDifficulty = false;
        }
    }
            
    private void StartTimer()
    {
        _startTimer = true;
        _difficultyTimer = Time.time + 30f;
    }   

    private void IncreaseDifficulty() 
    {
      if(Time.time > _difficultyTimer && _startTimer) 
        {
            _increaseDifficulty = true;
            _difficultyTimer = Time.time + 30f;
        }
    }
    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3);
        while (_stopspawing == false)
        {
            _powerUpTimer = Random.Range(3.0f, 7.0f);

            int _powerUpsID = Random.Range(0, _powerUps.Length - 2);

            yield return new WaitForSeconds(_powerUpTimer);

            _spawnPOS = new Vector3(Random.Range(-10.0f, 10.0f), 6.8f, 0);

            Instantiate(_powerUps[_powerUpsID], _spawnPOS, Quaternion.identity);
        }
    }

    IEnumerator RareSpawnPowerUp() 
    {
        yield return new WaitForSeconds(3);
        while (_stopspawing == false)
        {

            _rarePowerUpTimer = Random.Range(30.0f,60.0f);
            int _powerUpsID = Random.Range(5,7);
            yield return new WaitForSeconds(_rarePowerUpTimer);
            _spawnPOS = new Vector3(Random.Range(-10.0f, 10.0f), 6.8f, 0);
            Instantiate(_powerUps[_powerUpsID], _spawnPOS, Quaternion.identity);
        }

    }


    public void OnPlayerDeath()
    {
        _stopspawing = true;
    }
}
