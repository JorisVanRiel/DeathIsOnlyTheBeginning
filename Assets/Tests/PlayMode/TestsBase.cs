using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestsBase
{
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

    protected bool IsWithinTollerance(float expected, float actual, float tollerance = 0.001f)
    {
        bool isWithinTollerance = actual > expected - tollerance;
        isWithinTollerance = isWithinTollerance && actual < expected + tollerance;
        return isWithinTollerance;
    }
}