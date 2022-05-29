using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    private Vector3 distance;

    public GameObject ObjectToFollow { get { return this.objectToFollow; } }

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
