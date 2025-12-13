using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArcadeJumpSC utopiaMN;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [HideInInspector] AnimalFinderSC animalFinderMN;
    [HideInInspector] NoahSC noah;
    [HideInInspector] ArkSC ark;
    Vector3 lastPos;
    Vector3 originPos;
    Rigidbody2D rb;
    private float moveSpd, waitToMove;
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        moveSpd = 0;
        waitToMove = 5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        originPos = transform.position;
        lastPos = originPos;
        if (genCtr.gameMode == 2)
        {
            utopiaMN = GameObject.Find("CAN_UtopiaJump").GetComponent<ArcadeJumpSC>();
            ark = GameObject.Find("Ark(Clone)").GetComponent<ArkSC>();
            InvokeRepeating(nameof(AutoAscend), 10f, waitToMove);
        }
        else if(genCtr.gameMode == 3)
        {
            cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
            noah = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
            InvokeRepeating(nameof(AutoFlooding), 10f, waitToMove);
        }
        else if(genCtr.gameMode == 4)
        {
            //Animal Finder
        }
    }
    public void DecideMoveSpeed() => moveSpd += 0.01f;
    public void DecideWaitToMove() => waitToMove -= 0.01f;
    private void AutoAscend()
    {
        gameObject.transform.position = new Vector3(lastPos.x, lastPos.y + (moveSpd * (float)utopiaMN.curLevel), 0);
        lastPos = gameObject.transform.position;
    }
    private void AutoFlooding()
    {
        gameObject.transform.position = new Vector3(lastPos.x + (moveSpd * (float)cutwoodMn.curLv), noah.transform.position.y , 0);
        lastPos = gameObject.transform.position;
    }
    public void ResetPos()
    {
        transform.position = originPos;
    }
}
