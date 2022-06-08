using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CameraTests : TestsBase
{
    private const int cameraTestScene = 2;
    
    [UnityTest]
    public IEnumerator CameraShouldHaveAFixedRelativePostionToObjectItFollows()
    {
        yield return LoadScene(cameraTestScene);
        GameObject camera = GetSut();
        GameObject objectFollowedByCamera = GetObjectWithTag("Character");
        GetSutComponent<CameraController>().ObjectToFollow = objectFollowedByCamera;
        Assert.IsNotNull(objectFollowedByCamera, "Camera does not follow an object.");
        yield return new WaitForSeconds(1);
        objectFollowedByCamera.transform.position = new Vector3(
            objectFollowedByCamera.transform.position.x + 2,
            objectFollowedByCamera.transform.position.y,
            objectFollowedByCamera.transform.position.z + 3);

        yield return new WaitForSeconds(1);
        Vector3 startDistance = camera.transform.position - objectFollowedByCamera.transform.position;
        yield return null;
        objectFollowedByCamera.transform.position = new Vector3(
            objectFollowedByCamera.transform.position.x + 2,
            objectFollowedByCamera.transform.position.y,
            objectFollowedByCamera.transform.position.z + 3);

        yield return new WaitForSeconds(1);

        Vector3 finalDistance = camera.transform.position - objectFollowedByCamera.transform.position;

        AssertAreAproximatelyEqual(startDistance, finalDistance, "The relative position between camera and object it follows should not change.", .2f);
    }

    
}