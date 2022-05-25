using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DeathIsOnlyTheBeginning;
public class MonsterTests : TestsBase
{
    private const int testScene = 0;

    [UnityTest]
    public IEnumerator WhenMonsterReceivesDamageItsHitpointsShouldDeclineWithTheGivenAmount()
    {
        yield return LoadScene(testScene);
        Monster monster = GetSutComponent<Monster>();

        monster.ReceiveDamage(5);

        Assert.AreEqual(5, monster.HitPoints);
    }

    [UnityTest]
    public IEnumerator MonsterShouldDieWhenHpIsZeroOrLess()
    {
        yield return LoadScene(testScene);
        GameObject monsterObject = GetSut();
        Monster monster = GetSutComponent<Monster>();
        monster.ReceiveDamage(100);
        yield return null;
        AssertCharacterIsDead(monsterObject);
    }

    [UnityTest]
    public IEnumerator MonsterShouldDropLootOnItsLocationWhenItDies()
    {
        yield return LoadScene(testScene);
        Monster monster = GetSutComponent<Monster>();
        Vector3 monsterLocation = monster.transform.position;
        
        monster.ReceiveDamage(monster.HitPoints);
        yield return null;
        
        Item item = GameObject.FindObjectOfType<Item>();
        Assert.NotNull(item, "No item is dropped");
        Vector3 itemLocation = item.transform.position;
        Assert.AreEqual(monsterLocation, itemLocation);
    }

    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected monster to die, but didn't");
    }
}
