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
    private const int monsterAttackCharacterTestScene = 5;
    private const float waitingTime = 0.1f;
    private Vector3 positionInHallway = new Vector3(5, 0, 10);


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
        AssertMonsterIsDead(monsterObject);
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
        AssertAreAproximatelyEqual(monsterLocation, itemLocation, "Item did not drop on monster location", 0.1f);
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
        yield return new WaitForSeconds(1);

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

    [UnityTest]
    public IEnumerator MonsterShouldAttackPlayerWhenInAttackingDistance()
    {
        yield return LoadScene(monsterAttackCharacterTestScene);
        Character character = GetComponentFromObjectWithTag<Character>("Player");
        int hpStart = character.HitPoints;
        yield return new WaitForSeconds(1f);
        Assert.Less(character.HitPoints, hpStart, "monster dealt no damage to player.");
    }

    [UnityTest]
    public IEnumerator MonsterShouldNoLongerAttackPlayerWhenPlayerGetOutOfReach()
    {
        yield return LoadScene(monsterAttackCharacterTestScene);
        Character character = GetComponentFromObjectWithTag<Character>("Player");
        int hpStart = character.HitPoints;
        yield return new WaitForSeconds(2f);
        Assert.Less(character.HitPoints, hpStart, "Character in attacking distance should get damage.");
        character.transform.position = positionInHallway;
        yield return new WaitForSeconds(waitingTime);
        hpStart = character.HitPoints;
        yield return new WaitForSeconds(2);
        Assert.AreEqual(hpStart, character.HitPoints, "Character received damage while out of reach");
    }

    [UnityTest]
    public IEnumerator MonstersAttackShouldBeSperatedByAttackTime()
    {
        yield return LoadScene(monsterAttackCharacterTestScene);
        Character character = GetComponentFromObjectWithTag<Character>("Player");
        int hpStart = character.HitPoints;
        yield return new WaitForSeconds(.5f);
        int afterFirstAttack = character.HitPoints;
        Assert.Less(afterFirstAttack,hpStart);
        yield return new WaitForSeconds(.1f);
        int hpBetweenAttacks = character.HitPoints;
        Assert.AreEqual(afterFirstAttack, hpBetweenAttacks);
        yield return new WaitForSeconds(.6f);
        int hpAfterSecondAttack = character.HitPoints;
        Assert.Less(hpAfterSecondAttack, afterFirstAttack);


    }

    [UnityTest]
    public IEnumerator WhenMonsterDiesItShouldAddExperiencePointsToCharactersTotal()
    {
        yield return LoadScene(monsterTestScene);
        ScriptableObjectProvider provider = GetComponentFromObjectWithTag<ScriptableObjectProvider>("Provider");
        CharacterSheet sheet = provider.Get<CharacterSheet>();
        GameObject monsterObject = GetSut();
        int startXp = sheet.ExperiencePoints;

        Monster monster = GetSutComponent<Monster>();
        monster.ReceiveDamage(monster.HitPoints);
        yield return Wait();
        AssertMonsterIsDead(monsterObject);
        Assert.Greater(sheet.ExperiencePoints, startXp, "No experiensce points added to sheet.");
    }

    private static void AssertMonsterIsDead(GameObject monsterObject)
    {
        Assert.IsTrue(monsterObject == null, "Expected monster to die, but didn't");
    }
}
