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
    private const int characterAttackTestScene = 6;
    private float defaultWaitingTime = .1f;
    private Vector3 outOfRange = new Vector3(1, 0, 20);


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
    

    [UnityTest]
    public IEnumerator CharacterSchouldDealDamageWhenAttackingMonster()
    {
        yield return LoadScene(characterAttackTestScene);
        Character character = GetSutComponent<Character>();
        Monster monster = GetComponentFromObjectWithTag<Monster>("Monster");
        int startHp = monster.HitPoints;
        character.Attack(monster);
        yield return new WaitForSeconds(defaultWaitingTime);
        Assert.Less(monster.HitPoints, startHp);
    }

    [UnityTest]
    public IEnumerator CharacterShouldNotDealDamageWhenAttackingMonsterIsOutOfRange()
    {
        yield return LoadScene(characterAttackTestScene);
        Character character = GetSutComponent<Character>();
        Monster monster = GetComponentFromObjectWithTag<Monster>("Monster");
        int startHp = monster.HitPoints;
        character.Attack(monster);
        yield return new WaitForSeconds(defaultWaitingTime);
        int hpAfterFirstAttack = monster.HitPoints;
        Assert.Less(hpAfterFirstAttack, startHp, "Did not deal damage when close");
        monster.transform.position = outOfRange;
        yield return new WaitForSeconds(defaultWaitingTime);
        character.Attack(monster);
        yield return new WaitForSeconds(defaultWaitingTime);
        Assert.AreEqual(hpAfterFirstAttack, monster.HitPoints, "Character dealt damage while monster out of attack range");
    }
    private static void AssertCharacterIsDead(GameObject characterObject)
    {
        Assert.IsTrue(characterObject == null, "Expected character to die, but didn't");
    }
}
