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
        Assert.NotNull(sut, "character was never initiated");
        return sut;
    }

    protected T GetSutComponent<T>()
    {
        T component = GetSut().GetComponent<T>();
        Assert.NotNull(component, "Component not found on SUT");
        return component;
    }
}