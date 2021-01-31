using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var dm = player.GetComponent<DamageManager>();
        dm.OnCharacterDie += GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void BossKilled()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
