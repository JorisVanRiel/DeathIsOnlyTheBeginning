using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestsBase
{
    private float defaultWaitingTime = .1f;

    protected IEnumerator LoadScene(int number)
    {
        AsyncOperation loadSceneTask = SceneManager.LoadSceneAsync(number);
        yield return loadSceneTask;
    }

    protected GameObject GetSut()
    {
        GameObject sut = GameObject.FindGameObjectWithTag("SUT");
        Assert.NotNull(sut, "Subject Under Test was never initiated");
        return sut;
    }

    protected T GetSutComponent<T>()
    {
        T component = GetSut().GetComponent<T>();
        Assert.NotNull(component, $"Component {typeof(T)} not found on SUT");
        return component;
    }

    protected GameObject GetObjectWithTag(string tag)
    {
        GameObject player = GameObject.FindGameObjectWithTag(tag);
        Assert.IsNotNull(player, $"no object tagged with {tag} in scene.");
        return player;
    }

    protected T GetComponentFromObjectWithTag<T>(string tag)
    {
        T result = GetObjectWithTag(tag).GetComponent<T>();
        Assert.IsNotNull(result, $"Component {typeof(T)} not found on object tagged {tag}");
        return result;
    }

    protected void AssertAreAproximatelyEqual(Vector3 expected, Vector3 actual, string message, float tollerance = 0.01f)
    {
        bool areEqual = IsWithinTollerance(expected.x, actual.x, tollerance);
        areEqual = areEqual && IsWithinTollerance(expected.y, actual.y, tollerance);
        areEqual = areEqual && IsWithinTollerance(expected.y, actual.y, tollerance);
        if (!areEqual) Assert.Fail($"{message}\nExpected: {expected}\nActual: {actual}");
    }

    protected bool IsWithinTollerance(float expected, float actual, float tollerance = 0.01f)
    {
        bool isWithinTollerance = actual > expected - tollerance;
        isWithinTollerance = isWithinTollerance && actual < expected + tollerance;
        return isWithinTollerance;
    }

    protected IEnumerator Wait()
    {
        yield return new WaitForSeconds(defaultWaitingTime);
    }
}