using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DeathIsOnlyTheBeginning;
public class CharacterTests : TestsBase
{
    private const int characterTestScene = 0;

    [UnityTest]
    public IEnumerator CharacterShouldDieWhenTimeIsUp()
    {
        yield return LoadScene(characterTestScene);
        GameObject characterObject = GetSut();
        yield return new WaitForSeconds(2);
        AssertCharacterIsDead(characterObject);
    }

    [UnityTest]
    public IEnumerator WhenCharacterReceivesDamageItsHitpointsShouldDeclineWithTheGivenAmount()
    {
        yield return LoadScene(characterTestScene);

        Character character = GetSutComponent<Character>();
        character.ReceiveDamage(10);

        Assert.AreEqual(90, character.HitPoints);
    }

    [UnityTest]
    public IEnumerator CharacterShouldDieWhenHpIsZeroOrLess()
    {
        yield return LoadScene(characterTestScene);
        GameObject characterObject = GetSut();
        Character character = GetSutComponent<Character>();

        character.ReceiveDamage(100);
        yield return null;

        AssertCharacterIsDead(characterObject);
    }
    
    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected character to die, but didn't");
    }
}
