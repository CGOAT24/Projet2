using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private static float _speed = 5f;
    [SerializeField] private float _speedIncrease = 2f;
    [SerializeField] private float _sniperStoppingDistance = 5f;
    [SerializeField] private float _startTimeBetweenShots;
    [SerializeField] private GameObject _projectile = default;
    private SpawnManager _spawnManager;
    private float _timeBetweenShots;
    private bool _isSniper;
    private bool _canShoot;
    private UImanager _uiManager;



    /// <summary>
    /// Cette fonction sert � trouver le gameobject de type SpawnManager
    /// S'ex�cute au d�but de l'instanciation de l'objet
    /// </summary>
    void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        _timeBetweenShots = _startTimeBetweenShots;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _uiManager = _uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector2.Distance(transform.position, _player.position) > _sniperStoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, _player.position) < _sniperStoppingDistance)
        {
            if (!_canShoot)
            {
                _canShoot = true;
            }
            transform.position = transform.position;
        }

        if (_canShoot)
        {
            if (_timeBetweenShots <= 0)
            {
                Instantiate(_projectile, transform.position, Quaternion.identity);
                _timeBetweenShots = _startTimeBetweenShots;
            }
            else
            {
                _timeBetweenShots -= Time.deltaTime;
            }
        }
    }

    public void IncreaseSpeed()
    {
        _speed += _speedIncrease;
        Debug.Log(_speed);
    }

    /// <summary>
    /// Cette fonction s'ex�cute lorsque le gameobject entre en contact avec un autre collider
    /// </summary>
    /// <param name="other">gameobject en contact avec l'ennemie</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spawnManager.DeductFromEnemyCount();
            Destroy(gameObject);
            _uiManager.AjouterScore(10);
        }
    }
}
