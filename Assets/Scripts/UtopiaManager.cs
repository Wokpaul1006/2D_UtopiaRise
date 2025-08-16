using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class UtopiaManager : MonoBehaviour
{
    //UTOPIA RISE
    //Motif: Legend, jump, platformer
    //Control: Toch screen to jump, swipe screen to change direction
    //Obstacle: Cloud, flood, bird

    //Common zone
    [SerializeField] Text curretScore, currentLevel, maxLevel, totalMoney;
    public Text facingText;
    [SerializeField] Canvas MenuCAN, GameplayCAN;
    [SerializeField] GameObject coundonwPanel, mimicObjs;
    GeneralContrlSC genCtrl;
    CameraFollowPlayerSC camFollow;
    public int gameState; //Show state of the game. 0 is idle, 1 is in-play, 2 is end
    public bool isReplay;
    public int timeBeKill; //variable count the time player being killed every play, reset if close game.
                           //Specific zone
    [SerializeField] AdsMN adsMn;
    [SerializeField] List<GameObject> stepList = new List<GameObject>();
    [SerializeField] List<GameObject> cloundList = new List<GameObject>();
    [SerializeField] List<GameObject> heartList = new List<GameObject>();
    [SerializeField] public UtopiaCharSC character;
    [SerializeField] TidalWaveSC theTide;
    [SerializeField] GameObject ground;
    public Button changeDir;
    public GameObject step1InStepListToShow, step2InStepListToShow;
    Vector3 startPos = new Vector3(-2,0, 0);
    Vector3 startTidalPos;
    public Vector3 newestStepPos, nextStepPos;

    private int baseScore = 0;
    private int baseLevel = 1;
    private int randStepOder; //Random oder of footstep in the list
    private float randStepX, randStepY;
    public int stepListOnScreen;

    //Scoring variables
    int curScore;
    internal int curLevel;
    private int nextLvlTarget;
    private void Awake()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_CamToFollow").GetComponent<CameraFollowPlayerSC>();
        genCtrl.UpdateElement();
        startTidalPos = Vector3.zero;
        isReplay = false;
        timeBeKill = 0;
        gameState = 0;
    }
    void Start()
    {
        SettingStart();
        HandleUIs();

        #region Ads init
        adsMn = GameObject.Find("Ads_MN").GetComponent<AdsMN>();
        if (adsMn == null) { print("adsMN null"); }
        #endregion
    }

    public void OnPlay()
    {
        stepListOnScreen = 0;
        mimicObjs.gameObject.SetActive(false);
        MenuCAN.gameObject.SetActive(false);
        if(GameplayCAN.gameObject.activeSelf == false) GameplayCAN.gameObject.SetActive(true);
        ClearGameScreenOnPlay();
        randStepY = 0.25f;
        UpdateGameState(1);
        HandleUIs(); //Update player score ub ganmeplay Canvas
        GenerateGamplay();
    }
    private void ClearGameScreenOnPlay()
    {
        //int leghtofSteplist = stepListOnScreen;
        //if (leghtofSteplist == 1)
        //{
        //    step1InStepListToShow = null;
        //    Destroy(step1InStepListToShow);
        //    stepListOnScreen=1;
        //}
        //else if (leghtofSteplist == 2)
        //{
        //    step1InStepListToShow = null;
        //    Destroy(step1InStepListToShow);
        //    step2InStepListToShow = null;
        //    Destroy(step2InStepListToShow);
        //    stepListOnScreen = 0;
        //}

        Destroy(GameObject.Find("Platform1(Clone)"));
        Destroy(GameObject.Find("Platform2(Clone)"));
        Destroy(GameObject.Find("Platform3(Clone)"));
        Destroy(GameObject.Find("Platform4(Clone)"));
    }
    private void GenerateGamplay()
    {
        //Control all gameplay generation each time start the game
        if(timeBeKill == 0)
        {
            SpawnTidalWave();
        }else theTide.transform.position = startTidalPos;
        SpawnCharacter();
        SpawnGround();
        ShowHeartList();
        step1InStepListToShow = Instantiate(stepList[1], new Vector2(0.5f, randStepY), Quaternion.identity);
        stepListOnScreen = 1;
        camFollow.AssitCamToFollow();
    }
    private void ShowHeartList()
    {
        for(int i = 0; i< heartList.Count; i++) { if (heartList[i].gameObject.activeSelf == false) heartList[i].gameObject.SetActive(true); }
    }
    private void SpawnCharacter() => Instantiate(character, new Vector3(-2,-1,0),Quaternion.identity);
    private void SpawnTidalWave()
    {
        theTide = Instantiate(theTide, new Vector3(0, -12, 0), Quaternion.identity);
        startTidalPos = theTide.transform.position;
    }
    private void SpawnGround() => Instantiate(ground, new Vector3(0, -2 , 0), Quaternion.identity);
    private void SettingStart()
    {
        UpdateGameState(0);//Idle
        curScore = baseScore = 0;
        baseLevel = 0;
        randStepY = 0;
        randStepX = 0;

        curLevel = 1;
        nextLvlTarget = 10;

        StartCoroutine(SpawnClound());
    }
    public void RotatePlayer()
    {
        character.ChangDir();
    }
    #region Internal Handle
    private void HandleUIs()
    {
        if(gameState == 0)
        {
            maxLevel.text = PlayerPrefs.GetInt("PHighestLevel").ToString();
            totalMoney.text = PlayerPrefs.GetInt("PTotalScore").ToString();
        }else if(gameState == 1)
        {
            curretScore.text = curScore.ToString();
            currentLevel.text = curLevel.ToString();
        }

    }
    #endregion

    #region Gameplay Handle
    private IEnumerator SpawnClound()
    {
        yield return new WaitForSeconds(2);
        float randY = 0f;
        if (gameState == 0) { randY = Random.Range(2, 6); }
        else if(gameState == 1 || gameState == 2) { randY = character.transform.position.y + Random.Range(-2, 6); }
        Instantiate(cloundList[Random.Range(0, 2)], new Vector3(4, randY, 0), Quaternion.identity);
        StartCoroutine(SpawnClound());
    }
    public void DecideStepSpawn()
    {
        if (stepListOnScreen < 2)
        {
            randStepOder = Random.Range(0, 3); //Decide which step prefab to spawn
            RandPosStepSpawn(); //Caculatin position of step
            int temp = randStepOder % 2;
            if (temp == 1.25f)
            {
                if(stepListOnScreen == 0 || step1InStepListToShow == null)
                {
                    step1InStepListToShow = Instantiate(stepList[randStepOder], new Vector2(randStepX, randStepY), Quaternion.identity);
                    stepListOnScreen = 1;
                }
                else if(stepListOnScreen > 0 || step2InStepListToShow == null)
                {
                    step2InStepListToShow = Instantiate(stepList[randStepOder], new Vector2(randStepX, randStepY), Quaternion.identity);
                    stepListOnScreen = 2;
                }
            } 
            else
            {
                if(stepListOnScreen == 0 || step1InStepListToShow == null)
                {
                    step1InStepListToShow = Instantiate(stepList[randStepOder], new Vector2(randStepX, randStepY), Quaternion.identity);
                    stepListOnScreen = 1;
                }
                else if (stepListOnScreen > 0 || step2InStepListToShow == null)
                {
                    step2InStepListToShow = Instantiate(stepList[randStepOder], new Vector2(randStepX, randStepY), Quaternion.identity);
                    stepListOnScreen = 2;
                }
            }

        }
    }
    private void RandPosStepSpawn()
    {
        randStepX = Random.Range(-(newestStepPos.x) - 1.25f, newestStepPos.x + 1.25f);
        randStepY += 1.25f;
        nextStepPos = new Vector3(randStepX, randStepY, 0);
    }
    #endregion
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2) StopAllCoroutines();
    }
    public void IncreaseScore()
    {
        curScore++;
        if(curScore == nextLvlTarget)
        {
            IncreaseLevel();
            DecideNextLevelTarget();
            UpdtatePlayerPrefs();
        }
        HandleUIs();
    }
    private void IncreaseLevel()
    {
        curLevel++;
        theTide.DecideGravityScale();
    }
    private void DecideNextLevelTarget() => nextLvlTarget = (curLevel * 5);
    private void UpdtatePlayerPrefs()
    {
        //Get player prefs section
        int currenTotalScore;
        int highestScoreToCompare;
        int highestLevelToCompare;

        int newTotalScore;

        currenTotalScore = PlayerPrefs.GetInt("PTotalScore");
        highestLevelToCompare = PlayerPrefs.GetInt("PHighestLevel");
        highestScoreToCompare = PlayerPrefs.GetInt("PHighestScore");

        //Update total score
        newTotalScore = currenTotalScore + curScore;
        PlayerPrefs.SetInt("PTotalScore", curScore); //total of score that player have earn

        //Update highest score
        if (highestScoreToCompare < curScore) PlayerPrefs.SetInt("PHighestScore", curScore); //highest score that player can reach of all games

        //Update highets level
        if (highestLevelToCompare < curLevel) PlayerPrefs.SetInt("PHighestLevel", curLevel); //highest level player can reach
    }

    #region UI handler
    public void OnHome()
    {
        if(GameplayCAN.gameObject.activeSelf == true) { GameplayCAN.gameObject.SetActive(false); }
        UpdateGameState(0);
        MenuCAN.gameObject.SetActive(true);
    }
    public void OnShowSetting() => genCtrl.ShowSetting();
    public void OnShowPause() => genCtrl.ShowPause();
    public void OnShowLoose() => genCtrl.ShowLose();
    public void OnShowReward() => genCtrl.ShowReward();
    public void OnShowShop() => genCtrl.ShowShop();
    public void IsShowLive(int curLive)
    {
        switch (curLive)
        {
            case 0:
                heartList[0].SetActive(false);
                break;
            case 1:
                heartList[1].SetActive(false);
                break;
            case 2:
                heartList[2].SetActive(false);
                break;
        }
    }
    #endregion
}
