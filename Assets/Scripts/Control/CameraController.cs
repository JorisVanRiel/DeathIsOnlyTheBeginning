using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private GameObject objectToFollow;
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField] Vector3 distance;

    public GameObject ObjectToFollow { 
        get 
        { 
            return virtualCamera.Follow.gameObject; 
        }
        set 
        { 
            virtualCamera.Follow = value.transform;
        }
    }

    private void Awake()
    {
        this.virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (objectToFollow == null) return;

        this.transform.position = objectToFollow.transform.position + distance;
    }

}
