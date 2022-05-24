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
        AsyncOperation loadSceneTask = SceneManager.LoadSceneAsync(0);
        yield return loadSceneTask;

        GameObject characterObject = GameObject.FindGameObjectWithTag("SUT");
        Assert.NotNull(characterObject, "character was never initiated");
        Character characterComponent  = characterObject.GetComponent<Character>();

        yield return new WaitForSeconds(2);

        Assert.IsTrue(characterObject == null);
    }
}
