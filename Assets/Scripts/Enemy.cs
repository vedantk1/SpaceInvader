using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] GameObject _dieFX;
    Player _player;
    float _projDestroyTime = 3f;
    float _minShootTime = 1f;
    float _maxShootTime = 60f;
    float _timeSinceLastShot;
    float _coolDownTime;
    bool _moveRight;
    int _maxLateralMoves = 5;
    int _currentLateralMoves;
    float _destroyFXTime = 3f;
    float _lateralMoveTimer;
    float _lateralMoveDelay = 1f;
    AudioSource _audioSource;

    private void Start() {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _coolDownTime = UnityEngine.Random.Range(_minShootTime, _maxShootTime);
    }

    private void Update() {
        HandleTimers();
        HandleMovement();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (_timeSinceLastShot >= _coolDownTime)
        {
            SpawnProjectile();
        }
    }
    private void SpawnProjectile()
    {
        _timeSinceLastShot = 0f;
        _audioSource.Play();

        GameObject spawnedProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.down);
        Destroy(spawnedProjectile, _projDestroyTime);
    }

    private void HandleTimers()
    {
        _lateralMoveTimer += Time.deltaTime;
        _timeSinceLastShot += Time.deltaTime;
    }

    private void HandleMovement()
    {
        if (_lateralMoveTimer >= _lateralMoveDelay)
        {
            _lateralMoveTimer = 0f;
            _currentLateralMoves ++;
            if (_currentLateralMoves > _maxLateralMoves)
            {
                _currentLateralMoves = 0;
                _moveRight = !_moveRight;
                MoveDown();
            }
            else {MoveLaterally();}
        }
    }

    private void MoveDown()
    {
        transform.position += Vector3.down;
    }

    private void MoveLaterally()
    {
        float dir = _moveRight ? 1 : -1;
        transform.position += Vector3.right * dir;
    }

    public void GetDestoyed()
    {
        // Destroy(Instantiate(dieFX, transform.position, Quaternion.identity), destroyFXTime);
        _player.EnemyKilled();
        print("Enemy Died");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // print("collision with player");
        if (other.tag == "Player")
        {
            StartCoroutine(other.GetComponent<Player>().GameOver());
        }
    }
}
