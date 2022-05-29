using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterSheet characterSheet;
    [SerializeField] GameObject titleScreenPrefab;
    [SerializeField] GameObject dungeonPrefab;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject skillScreenPrefab;

    private GameObject titleScreen;
    private GameObject skillScreen;

    public GameObject SkillScreen { get => skillScreen; }

    private void Awake()
    {
        titleScreen = Instantiate(titleScreenPrefab);
        skillScreen = Instantiate(skillScreenPrefab);
        Instantiate(dungeonPrefab);
        skillScreen.SetActive(false);
    }

    public void StartGame()
    {
        characterSheet.Reset();
        titleScreen.SetActive(false);
        Instantiate(characterPrefab);
    }

    public void HandleCharacterDeath()
    {
        skillScreen.SetActive(true);
    }

    public void RespawnCharacter()
    {
        skillScreen.SetActive(false);
    }
}
