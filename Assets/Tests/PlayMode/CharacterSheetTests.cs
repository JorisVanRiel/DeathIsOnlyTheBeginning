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

    private CharacterSheet GetCharacterSheet()
    {
        return GetComponentFromObjectWithTag<ScriptableObjectProvider>("Provider")
            .Get<CharacterSheet>();
    }
}
