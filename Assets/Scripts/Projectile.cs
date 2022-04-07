using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _speed = 2f;
    [SerializeField] AudioClip _hitSound;
    public bool _triggersActivated;
    AudioSource _audioSource;
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        Invoke("ActivateTriggers", 0.2f);
    }
    private void Update() {
        transform.position += transform.up * _speed * Time.deltaTime;
    }
    private void ActivateTriggers()
    {
        _triggersActivated = true;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (_triggersActivated)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Enemy>().GetDestoyed();
            }
            else if (other.tag == "Player")
            {
                other.GetComponent<Player>().GetHurt();
            }

            // print("Projectile collision detected");
            _audioSource.PlayOneShot(_hitSound);
            Destroy(gameObject);
        }
    
    }
}
