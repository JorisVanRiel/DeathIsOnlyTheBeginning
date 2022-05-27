using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptableObjectProvider : MonoBehaviour
{
    [SerializeReference] List<ScriptableObject> scriptableObjects;

    internal T Get<T>() where T : ScriptableObject
    {
        return scriptableObjects.FirstOrDefault(s => s is T) as T;
    }
}
