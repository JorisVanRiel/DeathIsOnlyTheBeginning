using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterSheetTests : TestsBase
{

    private const int characterSheetTestScene = 7;

    [UnityTest]
    public IEnumerator ResetShouldSetExperiencePointsToZero()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.AddExperiencePoints(12);
        sheet.Reset();
        Assert.AreEqual(0, sheet.ExperiencePoints);

    }

    [UnityTest]
    public IEnumerator AddExperienceShouldAddTheAmountOfExperience()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();

        int startingExperience = sheet.ExperiencePoints;
        sheet.AddExperiencePoints(10);
        Assert.AreEqual(startingExperience + 10, sheet.ExperiencePoints);
    }

    [UnityTest]
    public IEnumerator AttackUpShouldAddOnePointToTheAttack()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startAttack = sheet.Attack;
        sheet.AddExperiencePoints(100);
        sheet.AttackUp();
        Assert.AreEqual(startAttack + 1, sheet.Attack);
    }

    [UnityTest]
    public IEnumerator AttackUpShouldCostExperiencePointsEqualToCurrentValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startAttack = sheet.Attack;
        sheet.AddExperiencePoints(100);
        sheet.AttackUp();
        Assert.AreEqual(100 - startAttack, sheet.ExperiencePoints);
    }

    [UnityTest]
    public IEnumerator AttackUpShouldDoNothingWhenExperiencePointsAreLessThenAttackValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startAttack = sheet.Attack;
        sheet.AddExperiencePoints(startAttack - 1);
        int startExperience = sheet.ExperiencePoints;
        Assert.Less(startExperience, startAttack, "Experience points should be lower then attack value for this test");
        sheet.AttackUp();
        Assert.AreEqual(startExperience, sheet.ExperiencePoints, "Experience points should not change");
        Assert.AreEqual(startAttack, sheet.Attack, "Attack value should not change.");
    }

    [UnityTest]
    public IEnumerator DefenceUpShouldDoNothingWhenExperiencePointsAreLessThenDefenceValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startDefence = sheet.Defence; 
        sheet.AddExperiencePoints(startDefence - 1);
        int startExperience = sheet.ExperiencePoints;
        Assert.Less(startExperience, startDefence, "Experience points should be lower then defence value for this test");
        sheet.DefenceUp();
        Assert.AreEqual(startExperience, sheet.ExperiencePoints, "Experience points should not change");
        Assert.AreEqual(startDefence, sheet.Defence, "Defence value should not change.");
    }


    [UnityTest]
    public IEnumerator DefenceUpShouldCostExperiencePointsEqualToCurrentValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startDefence = sheet.Defence;
        sheet.AddExperiencePoints(100);
        sheet.DefenceUp();
        Assert.AreEqual(100 - startDefence, sheet.ExperiencePoints);
    }


    [UnityTest]
    public IEnumerator ResetShouldSetAttackToTheStartingAttackValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        sheet.AddExperiencePoints(100);
        sheet.AttackUp();
        sheet.Reset();

        Assert.AreEqual(10, sheet.Attack);
    }

    [UnityTest]
    public IEnumerator ResetShouldSetDefenceToTheStartingDefenceValue()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        sheet.AddExperiencePoints(100);
        sheet.DefenceUp();
        sheet.Reset();

        Assert.AreEqual(sheet.StartingDefence, sheet.Defence);
    }


    [UnityTest]
    public IEnumerator DefenceUpShouldAddOnePointToDefence()
    {
        yield return LoadScene(characterSheetTestScene);
        CharacterSheet sheet = GetCharacterSheet();
        sheet.Reset();
        int startDefence = sheet.Defence;
        sheet.AddExperiencePoints(100);
        sheet.DefenceUp();
        Assert.AreEqual(startDefence + 1, sheet.Defence);
    }

    private CharacterSheet GetCharacterSheet()
    {
        return GetComponentFromObjectWithTag<ScriptableObjectProvider>("Provider")
            .Get<CharacterSheet>();
    }
}
