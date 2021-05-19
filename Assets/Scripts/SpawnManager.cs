using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = default;
    private float _xPos;
    private float _yPos;
    private int _enemyCount;
    private UImanager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //_uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();
        StartCoroutine(EnemySpawn());
    }

    /// <summary>
    /// Cette coroutine sert à gêrer l'instanciation d'ennemis
    /// Elle s'exécute lorsque le nombre d'ennemi est plus petit que 10
    /// </summary>
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

    /// <summary>
    /// Cette fonction s'exécute lorsqu'un ennemi est détruit
    /// Elle sert à mettre à jour le nombre d'ennemis
    /// </summary>
    public void DeductFromEnemyCount()
    {
        --_enemyCount;
        _uiManager.AjouterScore(10);
    }
}
