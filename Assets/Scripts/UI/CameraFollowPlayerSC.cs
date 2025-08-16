using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowPlayerSC : MonoBehaviour
{
    public CinemachineVirtualCamera vcam; // Drag your Cinemachine virtual camera here
    private UtopiaManager gameplayCtrl;
    void Start() { }
    void Update() { }

    public void AssitCamToFollow()
    {
        gameplayCtrl = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
    }
}
