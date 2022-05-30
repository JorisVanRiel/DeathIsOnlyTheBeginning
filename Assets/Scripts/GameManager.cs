using DeathIsOnlyTheBeginning;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterSheet characterSheet;
    [SerializeField] GameObject titleScreenPrefab;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] GameObject skillScreenPrefab;

    private GameObject titleScreen;
    private GameObject skillScreen;
    private Character character;

    public GameObject SkillScreen { get => skillScreen; }
    public UnityEvent<Character> CharacterRespawn = new UnityEvent<Character>();

    private int experienceLastRound;
    private int totalExperienceLastRound;

    private void Awake()
    {
        titleScreen = Instantiate(titleScreenPrefab);
        skillScreen = Instantiate(skillScreenPrefab);
        skillScreen.SetActive(false);
    }

    public void StartGame()
    {
        characterSheet.Reset();
        titleScreen.SetActive(false);
        InstantiateCharacter();
        experienceLastRound = 0;
        totalExperienceLastRound = 0;
    }

    public void HandleCharacterDeath()
    {
        
        int experienceThisRound = characterSheet.TotalExperiencePoints - experienceLastRound;
        if(experienceThisRound < experienceLastRound)
        {
            GameOver();
            return;
        }
        experienceLastRound = experienceThisRound;
        skillScreen.SetActive(true);
    }

    private void GameOver()
    {
        titleScreen.GetComponent<TitleScreen>().ShowGamOver();
        titleScreen.SetActive(true);
    }

    public void RespawnCharacter()
    {
        skillScreen.SetActive(false);
        InstantiateCharacter();
    }

    private void InstantiateCharacter()
    {
        character = Instantiate(characterPrefab).GetComponent<Character>();
        CameraController controller = Camera.main.GetComponent<CameraController>();
        controller.ObjectToFollow = character.gameObject;
        character.CharacterDied.AddListener(HandleCharacterDeath);
        character.transform.position = Vector3.zero;
        if (CharacterRespawn != null) CharacterRespawn.Invoke(character);
    }

}
