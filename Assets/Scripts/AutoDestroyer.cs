using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    [SerializeField] float lifetime = 1.0f;
  
    void Start()
    {
        StartCoroutine(AutoDestruct());

    }

    private IEnumerator AutoDestruct()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
}
