using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSC : MonoBehaviour
{
    GeneralContrlSC genCtrl;
    ArkMakingMNSC gameCtr;
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }
    public void OnReplay() => genCtrl.OnReplay();
    public void OnQuit() => genCtrl.OnExistGame();
    public void OnToHome() => genCtrl.OnToHome();
}
