using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSC : MonoBehaviour
{
    //[HideInInspector] PlayerInforSC infor;

    public string deviceID;
    public bool isFirstPlay;

    //Player's data variables
    public string pName;
    public int pCoin, pGems;
    public int pWoods, pIron, pStone, pCrop, pFruits;
    public int pSFX, pTheme;

    //Dialy Patrol
    public int pAllowClaimDaily, pDailyStreak;
    public string pLastDailyClaim;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SettingStart();
    }
    void Start() { }
    void Update() { }

    #region Local Handle
    private void SettingStart()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        if (CheckFirstPlay() == true)
        {
            SetNewPlayer();
        }
        else if (CheckFirstPlay() == false)
        {
            LoadOldPlayer();
        }
        //infor = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
    }

    #endregion

    #region Player Data Handle
    private void SetNewPlayer()
    {
        Debug.Log("in new layer");
        string subStringID = deviceID.Substring(8, 12).ToString();
        string nameFistPlay = "Player" + subStringID;
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.SetString("PlayerName", nameFistPlay);

        PlayerPrefs.SetInt("TotalCoin", 0); //Actual player in-game currency
        PlayerPrefs.SetInt("TotalGems", 0); //Player's PIA currency

        PlayerPrefs.SetInt("soundState", 1);
        PlayerPrefs.SetInt("sfxState", 1);


        //Patrol Reward
        PlayerPrefs.SetInt("AllowClaimDaily", 0);
        PlayerPrefs.SetString("LastPatrolDailyTime", "");
        PlayerPrefs.SetInt("PatrolDailyStreak", 0);

        Invoke("LoadOldPlayer", 3f); //De tam thoi
    }
    private void LoadOldPlayer()
    {
        pName = PlayerPrefs.GetString("PlayerName");
        pGems = PlayerPrefs.GetInt("TotalGems");
        pCoin = PlayerPrefs.GetInt("TotalCoin");
        pTheme = PlayerPrefs.GetInt("soundState");
        pSFX = PlayerPrefs.GetInt("sfxState");

        //Patrol Reward
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimDaily");
        pDailyStreak = PlayerPrefs.GetInt("PatrolDailyStreak");
    }
    public void DataDelete()
    {
        PlayerPrefs.DeleteAll();
        SetNewPlayer();
        //infor.GetPlayerData();
    }
    public void UploadPlayerData()
    {

    }
    #endregion

    #region Data Update
    public void UpdatePName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        pName = PlayerPrefs.GetString("PlayerName");
    }
    public void UpdateTotalScore(int currency)
    {
        PlayerPrefs.SetInt("Totalscore", currency);
        pCoin = PlayerPrefs.GetInt("TotalCoin");
    }
    public void UpdateTotalGem(int gems)
    {
        PlayerPrefs.SetInt("TotalGems", gems);
        pGems = PlayerPrefs.GetInt("TotalGems");
    }
    public void UpdateSFXState(int sfxState)
    {
        PlayerPrefs.SetInt("sfxState", sfxState);
        pSFX = PlayerPrefs.GetInt("sfxState");
    }
    public void UpdateThemeState(int thameState)
    {
        PlayerPrefs.SetInt("soundState", thameState);
        pTheme = PlayerPrefs.GetInt("soundState");
    }

    //Daily Reward
    public void UpdatePatrolDailyReward(string lastPatrolDaily)
    {
        PlayerPrefs.SetString("LastPatrolDailyTime", lastPatrolDaily);
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
    }
    public void UpdateAllowClaimDaily(int state)
    {
        PlayerPrefs.SetInt("AllowClaimDaily", state);
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimAllowClaimDaily");
    }
    public void UpdateStreak(int value)
    {
        PlayerPrefs.SetInt("PatrolDailyStreak", value);
        pDailyStreak = value;
    }
    #endregion

    #region Checking Zone
    private bool CheckFirstPlay()
    {
        //print("FirstPlay: " + PlayerPrefs.GetInt("HasPlayed"));
        if (PlayerPrefs.GetInt("HasPlayed") == 1)
        {
            return isFirstPlay = false;
        }
        else return true;
    }
    private bool CheckNewDay()
    {
        return true;
    }
    public void CheckThemeState()
    {
    }
    public void CheckSoundState()
    {

    }
    #endregion

    #region JSON HANDLERS
    public void OnLoadInventory()
    {
        SaveSystem saveSys = new SaveSystem();

    }
    #endregion
}
