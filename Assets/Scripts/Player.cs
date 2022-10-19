using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
     [SerializeField] private float _playerSpeed = 5.0f;
     [SerializeField] private GameObject _laserPrefab;
     [SerializeField] private bool _fireWeapon;
     [SerializeField] private float _fireRate;
     [SerializeField] private float _playerHealth = 100f;
     [SerializeField] private int _lives = 3;
     [SerializeField] private bool _isTripleShotEnabled = false;
     [SerializeField] private GameObject _tripleShotPrefab;
     [SerializeField] private float _tripleShotLength = 5.0f;
     private Spawn_Mananger _spawnScript;
    void Start()
    {
        _spawnScript = GameObject.FindObjectOfType<Spawn_Mananger>();
       
        transform.position = new Vector3(0,0,0);
       
        _fireWeapon = true;
       
        if (_spawnScript == null)
        {
            Debug.Log("THE SPAWNMANGER IS NULL!! MISSSSIINNG ERRROOORR");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBoundaries();
        playeraxismove();
        Shoot();
    }
    
    private void PlayerBoundaries()
    {
        if(transform.position.y >= 0)
        {
            transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -5)
        {
            transform.position = new Vector3(transform.position.x, -5,0);
        }

        if(transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f,transform.position.y,0);
        }
        else if(transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }
    private void playeraxismove()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(HorizontalInput,VerticalInput,0);
        transform.Translate(direction *_playerSpeed * Time.deltaTime);
    }
    private void Shoot()
    {
        Vector3 laserpos = new Vector3(transform.position.x,transform.position.y + 0.8f, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space) && _fireWeapon)
        {
            if (_isTripleShotEnabled) 
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                _fireWeapon = false;
                StartCoroutine(DelayFireRateRoutine());
            }
            else
           Instantiate(_laserPrefab,laserpos,Quaternion.identity);
          _fireWeapon = false;
          StartCoroutine(DelayFireRateRoutine());
        }
    } 

    IEnumerator DelayFireRateRoutine()
    {
        yield return new WaitForSeconds(_fireRate);
        _fireWeapon = true;
    }

    public void Damage()
    {
        _playerHealth += -20;
        StartCoroutine(FlashRedCourtine());
        if (_playerHealth < 1)
        {   _lives--;
            
            if(_lives < 1)
            {
                _spawnScript.OnPlayerDeath();
                Destroy(this.gameObject);
                _playerHealth = 0;
            }
            else _playerHealth = 100;
        }
        
    }
    IEnumerator FlashRedCourtine()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetTripleShot() 
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    
    IEnumerator TripleShotPowerDownRoutine() 
    {
        yield return new WaitForSeconds(_tripleShotLength);
        _isTripleShotEnabled = false;
    }
}
