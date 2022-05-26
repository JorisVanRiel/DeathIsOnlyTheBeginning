using DeathIsOnlyTheBeginning.Controlls;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


public class DoorTests : TestsBase
{
    private const int doorTestScene = 3;
    private float waitingTime = 0.1f;
    [UnityTest]
    public IEnumerator DoorShouldOnlyOpenWhenPlayerIsNear()
    {
        yield return LoadScene(3);
        DoorController door = GetSutComponent<DoorController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        door.Open();
        yield return new WaitForSeconds(waitingTime);
        Assert.IsTrue(IsWithinTollerance(0f, door.transform.position.y), "Door opend while player not near.");
        
        player.transform.position = door.transform.position - new Vector3(1f, 0, 1f);
        yield return new WaitForSeconds(waitingTime);
        player.transform.position = door.transform.position - new Vector3(5f, 0, 5f);
        yield return new WaitForSeconds(waitingTime);
        door.Open();
        yield return new WaitForSeconds(waitingTime);
        Assert.IsTrue(IsWithinTollerance(0f, door.transform.position.y), "Door opend while player not near.");
        
        Assert.IsNotNull(player, "no object tagger Player in scene.");
        player.transform.position = door.transform.position - new Vector3(1f, 0, 1f);
        yield return new WaitForSeconds(waitingTime);
        door.Open();
        yield return new WaitForSeconds(waitingTime);
        Assert.IsTrue(IsWithinTollerance(3f, door.transform.position.y), "Door did not open while player was near.");
    }

    [UnityTest]
    public IEnumerator DoorShouldOnlyOpenOnce()
    {
        yield return LoadScene(3);
        DoorController door = GetSutComponent<DoorController>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Assert.IsNotNull(player, "no object tagger Player in scene.");
        player.transform.position = door.transform.position - new Vector3(1f, 0, 1f);
        yield return new WaitForSeconds(waitingTime);
        door.Open();
        yield return new WaitForSeconds(waitingTime);
        yield return new WaitForSeconds(waitingTime);
        Assert.IsTrue(IsWithinTollerance(3f, door.transform.position.y), "Door did not open");
        door.Open();
        yield return new WaitForSeconds(waitingTime);
        Assert.IsTrue(IsWithinTollerance(3f, door.transform.position.y), "Door did is not in normal open position");
    }

   }
