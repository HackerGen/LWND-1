using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{

    private float _spawnTime = 5.0f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false; 
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator SpawnCoroutine()
    {
        while(_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
            // spawn
        }
    }

    public void StopSpawning()
    {
        _stopSpawning = true;
    }
}
