using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Mananger : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _powerUpContainer;
    [SerializeField] GameObject _tripleShotPowerUp;
    [SerializeField] float _spawnTimer = 5;
    [SerializeField] float _powerUpTimer;
    [SerializeField] private Vector3 _spawnPOS;
    [SerializeField] private bool _stopspawing = false;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopspawing == false)
        {
            yield return new WaitForSeconds(_spawnTimer);
           _spawnPOS = new Vector3(Random.Range(-10.0f,10.0f),6.8f,0);
        
            GameObject newEnemy = Instantiate(_enemy,_spawnPOS, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
        }

    }

    IEnumerator SpawnPowerUp() 
    {
        while(_stopspawing == false) 
        {
            yield return new WaitForSeconds(_powerUpTimer);
            _spawnPOS = new Vector3(Random.Range(-10.0f, 10.0f), 6.8f, 0);
            GameObject _newPowerUp = Instantiate(_tripleShotPowerUp,_spawnPOS, Quaternion.identity);
            _newPowerUp.transform.parent = _powerUpContainer.transform;
        }
    }



    


    public void OnPlayerDeath()
    {
        _stopspawing = true;
    }
}
