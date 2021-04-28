using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = default;
    private float _xPos;
    private float _yPos;
    private int _enemyCount;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (_enemyCount < 10)
        {
            var side = Random.Range(1, 4); // Up down left right

            switch (side)
            {
                case 1:
                    _xPos = Random.Range(-27.55f, 27.55f);
                    _yPos = Random.Range(6.45f, 16.45f);
                    break;
                case 2:
                    _xPos = Random.Range(-27.55f, 25.55f);
                    _yPos = Random.Range(-16.45f, -6.45f);
                    break;
                case 3:
                    _xPos = Random.Range(-27.55f, -16.55f);
                    _yPos = Random.Range(-4.55f, 4.55f);
                    break;
                case 4:
                    _xPos = Random.Range(16.55f, 27.55f);
                    _yPos = Random.Range(-4.55f, 4.55f);
                    break;
            }

            Instantiate(_enemyPrefab, new Vector3(_xPos, _yPos), Quaternion.identity);
            yield return new WaitForSeconds(3f);
            _enemyCount += 1;
        }
    }

    public void DeductFromEnemyCount()
    {
        --_enemyCount;
    }
}
