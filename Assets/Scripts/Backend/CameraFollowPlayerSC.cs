using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowPlayerSC : MonoBehaviour
{
    public CinemachineVirtualCamera vcam; // Drag your Cinemachine virtual camera here
    private ArcadeJumpSC gamectr;
    private ArkMakingMNSC gameCtrCutWood;
    void Start() { }
    void Update() { }

    public void AssitCamToFollow()
    {
        gamectr = GameObject.Find("CAN_UtopiaJump").GetComponent<ArcadeJumpSC>();
        vcam.Follow = gamectr.arkChar.GetComponent<Transform>();
    }
    public void AssistCamFollowCutWood(NoahSC a)
    {
        gameCtrCutWood = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        vcam.Follow = a.GetComponent<Transform>();
    }
}
