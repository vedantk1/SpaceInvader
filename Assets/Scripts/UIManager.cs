using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _highScoreText;
    [SerializeField] Text _livesLeftText;
    int _score;
    int _lives;
    int _highScore;
    
    private void Start() {
        GameManager.onEnemyKilled += UpdateScore;
        GameManager.onPlayerHurt += UpdateLives;

        UpdateHS();
        UpdateLives();
        UpdateScore();
    }

    private void UpdateScore()
    {
        _score = GameManager.GetScore();
        _scoreText = _score;
    }
    private void UpdateLives()
    {
        _lives = GameManager.GetScore();
        _livesLeftText = _lives;
    }
    private void UpdateHS()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText = _highScore;
    }
}
