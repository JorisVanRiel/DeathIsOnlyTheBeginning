using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject objectToFollow;

    [SerializeField] Vector3 distance;

    public GameObject ObjectToFollow { 
        get { return this.objectToFollow; }
        set { this.objectToFollow = value;}
    }

    private void Start()
    {
        if (objectToFollow == null) return;
        distance =  this.transform.position - objectToFollow.transform.position;
    }

    private void Update()
    {
        if (objectToFollow == null) return;

        this.transform.position = objectToFollow.transform.position + distance;
    }

}
