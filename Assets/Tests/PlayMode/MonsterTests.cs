using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DeathIsOnlyTheBeginning;
using DeathIsOnlyTheBeginning.Controlls;

public class MonsterTests : TestsBase
{
    private const int monsterTestScene = 1;
    private const int monsterWithCharacterTestScene = 4;
    private const float waitingTime = 0.1f;

    [UnityTest]
    public IEnumerator WhenMonsterReceivesDamageItsHitpointsShouldDeclineWithTheGivenAmount()
    {
        yield return LoadScene(monsterTestScene);
        Monster monster = GetSutComponent<Monster>();

        monster.ReceiveDamage(5);

        Assert.AreEqual(5, monster.HitPoints);
    }

    [UnityTest]
    public IEnumerator MonsterShouldDieWhenHpIsZeroOrLess()
    {
        yield return LoadScene(monsterTestScene);
        GameObject monsterObject = GetSut();
        Monster monster = GetSutComponent<Monster>();
        monster.ReceiveDamage(100);
        yield return null;
        AssertCharacterIsDead(monsterObject);
    }

    [UnityTest]
    public IEnumerator MonsterShouldDropLootOnItsLocationWhenItDies()
    {
        yield return LoadScene(monsterTestScene);
        Monster monster = GetSutComponent<Monster>();
        Vector3 monsterLocation = monster.transform.position;
        
        monster.ReceiveDamage(monster.HitPoints);
        yield return null;
        
        Item item = GameObject.FindObjectOfType<Item>();
        Assert.NotNull(item, "No item is dropped");
        Vector3 itemLocation = item.transform.position;
        Assert.AreEqual(monsterLocation, itemLocation);
    }

    [UnityTest]
    public IEnumerator MonsterShouldMoveTowardsPlayerWhenDoorIsOpenend()
    {
        yield return LoadScene(monsterWithCharacterTestScene);
        GameObject monster = GetSut();
        Vector3 monsterLocation = monster.transform.position;
        GameObject player = GetObjectWithTag("Player");
        Vector3 playerLocation = player.transform.position;
        float startDistance =Vector3.Distance(monsterLocation, playerLocation);
        DoorController doorController = GetComponentFromObjectWithTag<DoorController>("Door");

        yield return new WaitForSeconds(waitingTime);
        doorController.Open();
        yield return new WaitForSeconds(waitingTime);

        monsterLocation = monster.transform.position;
        float finalDistance =Vector3.Distance(monsterLocation, playerLocation);

        Assert.Less(finalDistance, startDistance, "Monster did not move in direction of player.");
    }

    [UnityTest]
    public IEnumerator MonsterShouldStayInPositionBeforeDoorIsOpenend()
    {
        yield return LoadScene(monsterWithCharacterTestScene);
        GameObject monster = GetSut();
        Vector3 monsterLocation = monster.transform.position;
        
        yield return new WaitForSeconds(waitingTime);

        AssertAreAproximatelyEqual(monsterLocation, monster.transform.position, "Monster moved before door was opened");
    }

    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected monster to die, but didn't");
    }
}
