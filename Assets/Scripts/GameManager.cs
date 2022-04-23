using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static event Action onEnemyKilled;
    public static event Action onPlayerHurt;
    public static event Action onGameOver;
    
    [SerializeField] int _killsToWin = 18;
    [SerializeField] int _maxLives = 3;
    int _score;
    private void Awake() {
        instance = this;
    }
    public static void EnemyKilled()
    {
        if (onEnemyKilled != null)
        {
            onEnemyKilled();
        }
    }
    public static void PlayerHurt()
    {
        if (onPlayerHurt != null)
        {
            onPlayerHurt();
        }
        
    }
    public static int GetScore()
    {
        return instance._score;
    }
    public static int GetLives()
    {
        return instance._maxLives;
    }
    private void OnEnable() {
        onEnemyKilled += _EnemyKilled;
        onPlayerHurt += _PlayerHurt;
        onGameOver += _SaveHS;
        
    }
    private void OnDisable() {
        onEnemyKilled -= _EnemyKilled;
        onPlayerHurt -= _PlayerHurt;
        onGameOver -= _SaveHS;        
    }

    private void _EnemyKilled()
    {
        _score++;
        _killsToWin--;
        if (_killsToWin <= 0 && onGameOver != null)
        {
            onGameOver();
        }
    }
    private void _PlayerHurt()
    {
        _maxLives--;
        
        if (_maxLives <= 0 && onGameOver != null)
        {
            onGameOver();
        }
    }
    private void _SaveHS()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < _score)
        {
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }
    
}
