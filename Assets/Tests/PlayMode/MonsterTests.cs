using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DeathIsOnlyTheBeginning;
public class MonsterTests
{
    [UnityTest]
    public IEnumerator WhenMonsterReceivesDamageItsHitpointsShouldDeclineWithTheGivenAmount()
    {
        yield return LoadScene();
        Monster monster = GetSUTComponen<Monster>();

        monster.ReceiveDamage(5);

        Assert.AreEqual(5, monster.HitPoints);
    }

    [UnityTest]
    public IEnumerator MonsterShouldDieWhenHpIsZeroOrLess()
    {
        yield return LoadScene();
        GameObject monsterObject = GetSut();
        Monster monster = GetSUTComponen<Monster>();
        monster.ReceiveDamage(100);
        yield return null;
        AssertCharacterIsDead(monsterObject);
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation loadSceneTask = SceneManager.LoadSceneAsync(1);
        yield return loadSceneTask;
    }

    private GameObject GetSut()
    {
        GameObject sut = GameObject.FindGameObjectWithTag("SUT");
        Assert.NotNull(sut, "sut was never initiated");
        return sut;
    }

    private T GetSUTComponen<T>()
    {
        T component = GetSut().GetComponent<T>();
        Assert.NotNull(component, $"Component {typeof(T)} not found on SUT");
        return component;
    }

    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected monster to die, but didn't");
    }
}
