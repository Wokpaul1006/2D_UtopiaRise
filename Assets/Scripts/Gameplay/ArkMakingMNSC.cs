using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkMakingMNSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtrl;
    [HideInInspector] CameraFollowPlayerSC camFollow;
    [HideInInspector] DataSC dataCtr;

    [SerializeField] Text woodsAmountTxt, ironAmountsTxt, stoneAmountsTxt, fruitAmountsTxt, cropAmountsTxt, moneyAmountsTxt;

    [SerializeField] NoahSC player;
    [SerializeField] GameObject theMap, treeA, treeB;

    [HideInInspector] public int deviceType;

    //Gameplay Control variables
    [HideInInspector]
    public bool isGameStart; //Use for detech game start and pauise even
    private int woodAmount, ironAmount, stoneAmount, fruistAmount, cropAmount, moneyAmount;
    private int treeOnScreenCapacity;
    public int curTreeOnScreen;
    [HideInInspector]
    private int randTreeToSpawn;
    //Collect Item Mode
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_Follow").GetComponent<CameraFollowPlayerSC>();
        dataCtr = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        isGameStart = false;
        deviceType = genCtrl.deviceType;
        curTreeOnScreen = 0;
        treeOnScreenCapacity = 100;
        genCtrl.AssitsGamemode(3);
        GetPlayerDatas();
        OnInitMap();
    }

    private void GetPlayerDatas()
    {
        woodAmount = dataCtr.pWoods;
        ironAmount = dataCtr.pIron;
        stoneAmount = dataCtr.pStone;
        fruistAmount = dataCtr.pFruits;
        cropAmount = dataCtr.pCrop;
        moneyAmount = dataCtr.pCoin;

        OnHandleUIs();
    }

    #region Handle Gameplay
    private void OnInitMap()
    {
        //theMap = Instantiate(theMap, Vector3.zero, Quaternion.identity);
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        camFollow.AssistCamFollowCutWood(player);
        isGameStart = true;
        InvokeRepeating(nameof(SpawnTree), 0f, 2);
    }
    private void SpawnTree()
    {
        if(isGameStart == true)
        {
            if(curTreeOnScreen <= treeOnScreenCapacity)
            {
                randTreeToSpawn = Random.Range(0, 2);
                Vector3 randPos;
                randPos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
                randPos.y = Random.Range(player.transform.position.y - 5, player.transform.position.y + 5);
                int tempAmountTreeToSpawn = Random.Range(1, 10);
                for (int i = 0; i < tempAmountTreeToSpawn; i++)
                {
                    curTreeOnScreen++;
                    if (randTreeToSpawn == 0) Instantiate(treeA, new Vector3(randPos.x + (i / 10), randPos.y, 0), Quaternion.identity);
                    else if (randTreeToSpawn != 0) Instantiate(treeB, new Vector3(randPos.x + (i / 5), randPos.y, 0), Quaternion.identity);
                }
            }
        }
    }
    #endregion


    #region Handle UI events
    private void OnHandleUIs()
    {
        woodsAmountTxt.text = woodAmount.ToString();
        ironAmountsTxt.text = ironAmount.ToString();
        stoneAmountsTxt.text = stoneAmount.ToString();
        cropAmountsTxt.text = cropAmount.ToString();
        fruitAmountsTxt.text = fruistAmount.ToString();
        moneyAmountsTxt.text = moneyAmount.ToString();
    }
    public void OnPause()
    {
        //Handle pause game
        isGameStart = false;
        genCtrl.ShowPause(true);
    }
    public void OnLoose()
    {
        isGameStart = false;
        genCtrl.ShowLose(true);
    }
    #endregion
    
}
