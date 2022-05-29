using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManagerTests : TestsBase
{
    private const int gameManagerTestScene = 8;

    [UnityTest]
    public IEnumerator OnAwakeGameManagerShouldInstantiateDungeon()
    {
        yield return AssertObjectWithTagIsInstantiatedAtAwake("Dungeon");
    }

    [UnityTest]
    public IEnumerator OnAwakeGameManagerShouldInstantiateTitleScreen()
    {
        yield return AssertObjectWithTagIsInstantiatedAtAwake("Dungeon");
    }

    [UnityTest]
    public IEnumerator GameManagerShouldInstantiateSkillScreenOnAwake()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();
        Assert.IsTrue(gameManager.SkillScreen != null, "Skill Screen is not instantiated.");
    }

    [UnityTest]
    public IEnumerator GameManagerShouldDisableSkillScreenOnAwak()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();
        Assert.IsFalse(gameManager.SkillScreen.activeInHierarchy, "Skill screen should be disabled on awake");

    }

    [UnityTest]
    public IEnumerator GameManagerShouldShouldResetTheCharacterSheetOnStartGame()
    {
        yield return LoadScene(gameManagerTestScene);
        ScriptableObjectProvider provider = GetComponentFromObjectWithTag<ScriptableObjectProvider>("Provider");
        CharacterSheet characterSheet = provider.Get<CharacterSheet>();
        characterSheet.AddExperiencePoints(100);
        characterSheet.AttackUp();
        characterSheet.DefenceUp();

        GameManager gameManager = GetSutComponent<GameManager>();
        gameManager.StartGame();

        Assert.AreEqual(0, characterSheet.ExperiencePoints);
        Assert.AreEqual(characterSheet.StartingAttack, characterSheet.Attack);
        Assert.AreEqual(characterSheet.StartingDefence, characterSheet.Defence);

    }

    [UnityTest]
    public IEnumerator GamemanagerShouldDisableTitleScreenOnStartGame()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();
        GameObject titleScreen = GetObjectWithTag("TitleScreen");
        gameManager.StartGame();
        yield return new WaitForSeconds(1);
        Assert.IsFalse(titleScreen.activeInHierarchy, "Titlescreen is not disabled.");
    }

    [UnityTest]
    public IEnumerator GameManagerShouldInstantiatCharacterOnStartGame()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();
        gameManager.StartGame();
        GameObject character = GameObject.FindGameObjectWithTag("Character");

        Assert.IsTrue(character != null, "Character should be instantiated on start game.");
    }



    [UnityTest]
    public IEnumerator GameManagerShouldShowSkillScreenWhenCharacterDies()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();

        gameManager.HandleCharacterDeath();

        yield return Wait();

        Assert.IsTrue(gameManager.SkillScreen.activeInHierarchy, "SkillScreen should be shown after character dies");
    }

    [UnityTest]
    public IEnumerator GameManagerShouldDisableSkillScreenOnRespawn()
    {
        yield return LoadScene(gameManagerTestScene);
        GameManager gameManager = GetSutComponent<GameManager>();
        gameManager.HandleCharacterDeath(); 
        yield return Wait();

        gameManager.RespawnCharacter();
        yield return Wait();

        Assert.IsFalse(gameManager.SkillScreen.activeInHierarchy, "SkillScreen Should be disabled after character respawn");
    }

    private IEnumerator AssertObjectWithTagIsInstantiatedAtAwake(string tag)
    {
        yield return LoadScene(gameManagerTestScene);
        Assert.IsTrue(GameObject.FindGameObjectWithTag(tag) != null, $"{tag} not instatiated");
    }
   
}