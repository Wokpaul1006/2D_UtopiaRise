using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSC : MonoBehaviour
{
    GeneralContrlSC genCtrl;
    public UtopiaManager gameCtr;
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }
    public void OnReplay() => gameCtr.OnPlay();
    public void OnQuit()
    {
        Application.Quit();
        //Add save player data here
    }
    public void OnToHome()
    {
        //Function build later
    }
}
