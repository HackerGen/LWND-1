using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private float _speed = 3.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFireTime = 0.0f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("erreur SpawnManager null dans player classe");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        ShootLasers();
    }

    void CalculateMovement()
    {
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        
        transform.Translate(Time.deltaTime * _speed * directionVector);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8.3f, 8.3f), 
            Mathf.Clamp(transform.position.y, -4.4f, 4.4f),
            transform.position.z
        );
    }

    void ShootLasers()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            Instantiate(
                _laserPrefab, 
                new Vector3(transform.position.x, transform.position.y + 1.0f, 0), 
                Quaternion.identity
            );
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject);
        }
    }
    
}