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
        GameObject objectFollowedByCamera = GetSutComponent<CameraController>().ObjectToFollow;

        Assert.IsNotNull(objectFollowedByCamera, "Camera does not follow an object.");
        Vector3 startDistance = camera.transform.position - objectFollowedByCamera.transform.position;
        yield return null;
        objectFollowedByCamera.transform.position = new Vector3(
            objectFollowedByCamera.transform.position.x + 1,
            objectFollowedByCamera.transform.position.y + 2,
            objectFollowedByCamera.transform.position.z + 3);
        
        yield return null;
        yield return null;

        Vector3 finalDistance = camera.transform.position - objectFollowedByCamera.transform.position;

        AssertAreAproximatelyEqual(startDistance, finalDistance, "The relative position between camera and object it follows should not change.");
    }

    
}