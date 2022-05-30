using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject title;
    [SerializeField] GameObject gameOver;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void HandleStartButtonClick()
    {
        gameManager.StartGame();
    }

    public void HandleBackButton()
    {
        credits.SetActive(false);
        gameOver.SetActive(false);
        title.SetActive(true);
    }

    public void HandleCreditsButton()
    {
        credits.SetActive(true);
        title.SetActive(false);
        gameOver.SetActive(false);
    }

    internal void ShowGamOver()
    {
        credits.SetActive(false);
        title.SetActive(false);
        gameOver.SetActive(true);
    }

    public void GameOverBack()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
