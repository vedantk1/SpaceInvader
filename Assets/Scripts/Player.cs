using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip _deathSFX;
    [SerializeField] GameObject _dieFX;
    [SerializeField] GameObject _projectile;
    [SerializeField] float _maxHorMovement = 5f;
    [SerializeField] float _speed = 10f;
    [SerializeField] float _coolDownTimer = 0.5f;
    float _timeSinceLastShot;
    float _projDestroyTime = 3f;
    float _gameOverDelay = 2f;
    float _fxDestroyTime = 2f;
    AudioSource _audioSource;
    private void OnEnable() {
        GameManager.onGameOver += _StartGameOverSeq;
    }
    private void OnDisable() {
        GameManager.onGameOver -= _StartGameOverSeq;
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        InputHandler();
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _timeSinceLastShot += Time.deltaTime;
    }

    private void InputHandler()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            transform.position = Vector2.MoveTowards(transform.position, Vector2.right * _maxHorMovement * dir, _speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _timeSinceLastShot > _coolDownTimer)
        {
            SpawnProjectile();
        }
    }

    public void GetHurt()
    {
        GameManager.PlayerHurt();
        
    }
    private void _StartGameOverSeq()
    {
        StartCoroutine(GameOver());
    }
    public IEnumerator GameOver()
    {
        Destroy(Instantiate(_dieFX, transform.position, Quaternion.identity), _fxDestroyTime);
        _audioSource.PlayOneShot(_deathSFX);
        
        Destroy(gameObject);
        print("Game Over!");
        yield return new WaitForSeconds(_gameOverDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SpawnProjectile()
    {
        _timeSinceLastShot = 0f;
        _audioSource.Play();

        GameObject spawnedProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        Destroy(spawnedProjectile, _projDestroyTime);
    }
}
