using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArcadeJumpSC : MonoBehaviour
{
    //UTOPIA RISE
    //Motif: Legend, jump, platformer
    //Control: Toch screen to jump, swipe screen to change direction
    //Obstacle: Cloud, flood, bird
    //Arcade Mode
    [HideInInspector] GeneralContrlSC genCtrl;
    [HideInInspector] CameraFollowPlayerSC camFollow;
    [SerializeField] List<GameObject> stepListToShow = new List<GameObject>();
    [SerializeField] List<GameObject> cloundList = new List<GameObject>();
    [SerializeField] List<GameObject> heartList = new List<GameObject>();
    [SerializeField] TidalWaveSC theTide;
    [SerializeField] GameObject ground;
    [SerializeField] Button rotatecharBtn;
    [SerializeField] Text currentScoreTxt, currentLvlTxt, surviveTimeTxt, surviveSecondTxt;

    [HideInInspector] public bool isGameStart;
    Vector3 previousStepPos;
    Vector3 startTidalPos;
    Vector3 lastStepPos;
    public Vector3 newestStepPos, nextStepPos;
    public GameObject step1InStepListToShow, step2InStepListToShow;
    public ArkSC arkChar;
    public int gameState; //Show state of the game. 0 is idle, 1 is in-play, 2 is end
    private int baseScore = 0;
    private int baseLevel = 1;
    private int randStepOder; //Random oder of footstep in the list to show
    public int pHeightJumped;
    public float pHeightCoordiante;
    public int deviceType;

    //Scoring variables
    int curScore;
    internal int curLevel;
    private int nextLvlTarget;
    int surviveSeconds, surviveMinute;
    int stepOrder;

    void Start()
    {
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        camFollow = GameObject.Find("CAM_Follow").GetComponent<CameraFollowPlayerSC>();
        curScore = baseScore = 0;
        previousStepPos = Vector3.zero;
        startTidalPos = Vector3.zero;
        lastStepPos = Vector3.zero;
        baseLevel = 0;
        curLevel = 1;
        surviveSeconds = 0;
        surviveMinute = 0;
        nextLvlTarget = 10;
        pHeightJumped = 0;
        stepOrder = 0;
        deviceType = genCtrl.deviceType;
        isGameStart = false;
        OnPlay();
        genCtrl.AssitsGamemode(2);
        InvokeRepeating(nameof(SpawnClound), 0f, 2f);
        InvokeRepeating(nameof(IncreaseSurviveTime), 0f, 1f);
    }

    // Update is called once per frame
    void Update() { }
    public void OnPlay()
    {
        UpdateGameState(1);
        HandleUIs(); //Update player score ub ganmeplay Canvas
        GenerateGamplay();
    }
    private void GenerateGamplay()
    {
        //Control all gameplay generation each time start the game
        SpawnTidalWave();
        SpawnCharacter();
        SpawnGround();
        ShowHeartList();
        SpawnPlatforms();
        camFollow.AssitCamToFollow();
        isGameStart = true;
    }
    private void ShowHeartList()
    {
        for (int i = 0; i < heartList.Count; i++) { if (heartList[i].gameObject.activeSelf == false) heartList[i].gameObject.SetActive(true); }
    }
    private void SpawnCharacter() => arkChar = Instantiate(arkChar, new Vector3(-2, -1, 0), Quaternion.identity);
    private void SpawnTidalWave()
    {
        theTide = Instantiate(theTide, new Vector3(0, -12, 0), Quaternion.identity);
        startTidalPos = theTide.transform.position;
    }
    private void SpawnGround() => Instantiate(ground, new Vector3(0, -2, 0), Quaternion.identity);
    public void RotatePlayer()
    {
        arkChar.ChangDir();
    }
    #region Internal Handle
    public void HandleUIs()
    {
        surviveTimeTxt.text = surviveMinute.ToString() + " 0";
        surviveSecondTxt.text = surviveSeconds.ToString();
        currentLvlTxt.text = curLevel.ToString();
        currentScoreTxt.text = curScore.ToString();
    }
    public void UpdateGameState(int state)
    {
        gameState = state;
        if (gameState == 2) StopAllCoroutines();
    }
    public void IncreaseScore()
    {
        curScore++;
        if (curScore == nextLvlTarget)
        {
            IncreaseLevel();
            DecideNextLevelTarget();
            UpdtatePlayerPrefs();
        }

        if(pHeightJumped >= 10 || pHeightCoordiante >= lastStepPos.y)
        {
            SpawnPlatforms();
            pHeightJumped = 0;
        }
    }
    private void SpawnPlatforms()
    {
        for (int i = 0; i < 10; i++)
        {
            stepOrder++;
            float randStepX;
            int randStepOrder;
            randStepX = Random.Range(lastStepPos.x - 2, lastStepPos.x + 2);
            randStepOrder = Random.Range(0, stepListToShow.Count);

            if (previousStepPos.x == 0 && previousStepPos.y == 0)
            {
                //Spawn first step
                Instantiate(stepListToShow[randStepOrder], new Vector3(randStepX, 0.5f), Quaternion.identity);
                previousStepPos = new Vector3(randStepX, 0.5f, 0);
            }
            else if (previousStepPos.y != 0)
            {
                if(previousStepPos.x == randStepX)
                {
                    float tempRandStepX;
                    tempRandStepX = randStepX + 0.5f;
                    randStepX = tempRandStepX;
                    Instantiate(stepListToShow[randStepOrder], new Vector3(randStepX+0.5f, previousStepPos.y + 0.25f), Quaternion.identity);
                }else if(previousStepPos.x != randStepX)
                {
                    Instantiate(stepListToShow[randStepOrder], new Vector3(randStepX, previousStepPos.y + 0.25f), Quaternion.identity);
                }

                previousStepPos = new Vector3(randStepX, previousStepPos.y + 0.5f, 0);
                if(stepOrder >= 10)
                {
                    lastStepPos = previousStepPos;
                }
            }
        }
    }
    private void IncreaseSurviveTime()
    {
        surviveSeconds += 1;
        if (surviveSeconds >= 60)
        {
            surviveMinute++;
            surviveSeconds = 0;
        }
        HandleUIs();
    }
    private void IncreaseLevel()
    {
        curLevel++;
        theTide.DecideMoveSpeed();
        theTide.DecideWaitToMove();
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

    #endregion

    #region Gameplay Handle
    private void SpawnClound()
    {
        float randY = 0f;
        if (gameState == 0) { randY = RandomInt(2, 6); }
        else if (gameState == 1 || gameState == 2) 
        {
            if(arkChar != null)
            {
                randY = arkChar.transform.position.y + UnityEngine.Random.Range(-2, 6);
            }
        }
    }
    public void CheckPlayerHieght()
    {
        pHeightCoordiante = arkChar.transform.position.y;
        pHeightJumped = (int)pHeightCoordiante;
    }
    #endregion

    public void OnHome()
    {
        genCtrl.OnToHome();
    }
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
    int RandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    float RandomFloat(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public void OnShowLoose()
    {
        isGameStart = false;
        genCtrl.ShowLose(true);
    }
    public void OnPause()
    {
        //Handle pause game
        isGameStart             = false;
        genCtrl.ShowPause(true);
    }
}
