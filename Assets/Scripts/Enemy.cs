using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private SpawnManager _spawnManager;

    void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = _player.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
        direction.Normalize();
        _movement = direction;
    }

    private void FixedUpdate()
    {
        MoveCharacter(_movement);
    }

    public void MoveCharacter(Vector2 direction)
    {
        _rb.MovePosition((Vector2)transform.position + (direction * _speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spawnManager.DeductFromEnemyCount();
            Destroy(gameObject);
        }
    }
}
