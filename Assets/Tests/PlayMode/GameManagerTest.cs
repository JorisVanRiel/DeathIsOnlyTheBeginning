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
}