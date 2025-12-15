using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArkMakingMNSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtrl;
    [HideInInspector] CameraFollowPlayerSC camFollow;
    [SerializeField] Text currentScoreTxt, currentLvlTxt, timeCountdownTxt, goalTxt;
    [SerializeField] TidalWaveSC theTide;
    [SerializeField] NoahSC player;
    [SerializeField] GameObject theMap, treeA, treeB;

    [HideInInspector] public int deviceType;

    //Gameplay Control variables
    [HideInInspector]
    public bool isGameStart; //Use for detech game start and pauise even
    private int curScore; //Current wood collected
    [HideInInspector]
    public int curLv; //Current Level of Player ingame
    private int countdownTime; //Time limit of player
    private int curGoal; //Goal player must reach, caculating each time level up;
    private int randTreeToSpawn;
    //Collect Item Mode
    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_Follow").GetComponent<CameraFollowPlayerSC>();
        isGameStart = false;
        curScore = 0;
        curLv = curGoal = 1;
        countdownTime = 60;
        deviceType = genCtrl.deviceType;
        genCtrl.AssitsGamemode(3);
        OnHandleUIs();
        OnInitMap();
    }

    // Update is called once per frame
    void Update() 
    {

    }

    #region Handle Gameplay
    private void CountdonwDecrease()
    {
        if (isGameStart == true)
        {
            countdownTime--;
            if (countdownTime <= 0)
            {
                isGameStart = false;
                OnLoose();
                //Update score
            }
            OnHandleCountdown(countdownTime);
        }      
    }
    private void OnInitMap()
    {
        theMap = Instantiate(theMap, Vector3.zero, Quaternion.identity);
        player = Instantiate(player, Vector3.zero, Quaternion.identity);
        camFollow.AssistCamFollowCutWood(player);
        theTide = Instantiate(theTide, new Vector3(-12, 0, 0), Quaternion.identity);
        isGameStart = true;
        InvokeRepeating(nameof(SpawnTree), 0f, 2);
        InvokeRepeating(nameof(CountdonwDecrease), 0f, 1f);
    }
    private void SpawnTree()
    {
        if(isGameStart == true)
        {
            randTreeToSpawn = Random.Range(0, 2);
            Vector3 randPos;
            randPos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
            randPos.y = Random.Range(player.transform.position.y - 5, player.transform.position.y + 5);
            if(randTreeToSpawn == 0)    Instantiate(treeA, new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
            else if(randTreeToSpawn != 0)   Instantiate(treeB, new Vector3(randPos.x, randPos.y, 0), Quaternion.identity);
        }
    }
    public void IncreaseScore()
    {
        curScore++;
        if(curScore >= curGoal)
        {
            curLv++;
            int tempCurSecond;
            tempCurSecond = countdownTime;
            countdownTime = tempCurSecond + 60;

            int tempNewGoal;
            tempNewGoal = curLv * curGoal;
            curGoal = tempNewGoal;
            theTide.ResetPos();
        }
        OnHandleUIs();
    }
    public void OnUpdatePlayerScore()
    {
        genCtrl.OnUpdatePlayerInformationAfterGames(curScore);
    }
    #endregion


    #region Handle UI events
    private void OnHandleUIs()
    {
        currentScoreTxt.text = curScore.ToString() ;
        currentLvlTxt.text = curLv.ToString();
        goalTxt.text = curGoal.ToString();
    }
    private void OnHandleCountdown(int a)
    {
        timeCountdownTxt.text = a.ToString() + "s";
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
        OnUpdatePlayerScore();
        genCtrl.ShowLose(true);
    }
    #endregion
    
}
