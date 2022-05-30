using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject credits;
    [SerializeField] GameObject title;

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
        title.SetActive(true);
    }

    public void HandleCreditsButton()
    {
        credits.SetActive(true);
        title.SetActive(false);
    }

}
