using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector3 _enemyDirection = new Vector3(0,-1,0);
    [SerializeField] private float _enemySpawn = 6.8f;
    [SerializeField] private Vector3 _randomSpawn;
    [SerializeField] private float _enemySpeed = 4.0f;
    private Player  _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }
    void Update()
    {
        EnemyMovement();    
    }

    private void EnemyMovement()
    {
        transform.Translate((_enemyDirection) *_enemySpeed * Time.deltaTime);
        if(transform.position.y < -6.5f )
        {
            _randomSpawn = new Vector3(Random.Range(-11.18f,11.18f),_enemySpawn,0);
            transform.position =_randomSpawn;
        } 
    }

private void OnTriggerEnter2D(Collider2D other) 
{
    
  
    
    if(other.gameObject.tag == "Laser")
    {
        if(_player != null) 
            { 
            
                _player.AddScore(Random.Range(0,50));
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
    }
            
            
    else if(other.gameObject.tag == "Player")
    {
        if(_player != null)
        {
           Destroy(this.gameObject);
           _player.Damage();
        }
        else
        Debug.Log("COMPONENT MISSING");
            
    }
}

}
