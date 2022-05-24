using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DeathIsOnlyTheBeginning;
public class CharacterTests
{

    [UnityTest]
    public IEnumerator CharacterShouldDieWhenTimeIsUp()
    {

        yield return LoadScene();
        GameObject characterObject = GetSut();
        yield return new WaitForSeconds(2);
        AssertCharacterIsDead(characterObject);
    }

    [UnityTest]
    public IEnumerator WhenCharacterReceivesDamageItsHitpointsShouldDeclineWithTheGivenAmount()
    {
        yield return LoadScene();

        Character character = GetSUTComponen<Character>();
        character.ReceiveDamage(10);

        Assert.AreEqual(90, character.HitPoints);
    }

    [UnityTest]
    public IEnumerator CharacterShouldDieWhenHpIsZeroOrLess()
    {
        yield return LoadScene();
        GameObject characterObject = GetSut();
        Character character = GetSUTComponen<Character>();

        character.ReceiveDamage(100);
        yield return null;

        AssertCharacterIsDead(characterObject);
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation loadSceneTask = SceneManager.LoadSceneAsync(0);
        yield return loadSceneTask;
    }

    private GameObject GetSut()
    {
        GameObject sut = GameObject.FindGameObjectWithTag("SUT");
        Assert.NotNull(sut, "character was never initiated");
        return sut;
    }

    private T GetSUTComponen<T>()
    {
        T component = GetSut().GetComponent<T>();
        Assert.NotNull(component, "Component not found on SUT");
        return component;
    }


    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected character to die, but didn't");
    }
}
