using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSC : MonoBehaviour
{
    //[HideInInspector] PlayerInforSC infor;

    public string deviceID;
    public bool isFirstPlay;

    //Player's data variables
    [HideInInspector] public string pName;
    [HideInInspector] public int pCoin, pGems;
    [HideInInspector] public int pWoods, pIron, pStone, pCrop, pFruits;
    [HideInInspector] public int pSFX, pTheme;

    //Dialy Patrol
    [HideInInspector] public int pAllowClaimDaily, pDailyStreak;
    [HideInInspector] public string pLastDailyClaim;

    [SerializeField] SaveSystem saveSys;
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
        OnResetJSON();
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
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
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
        pDailyStreak = PlayerPrefs.GetInt("PatrolDailyStreak");
    }
    #endregion

    #region Update Ingame Stats
    public void UpdateWoods(int value)
    {
        pWoods = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateIron(int value)
    {
        pIron = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateStone(int value)
    {
        pStone = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateFruist(int value)
    {
        pFruits = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    public void UpdateCrop(int value)
    {
        pCrop = value;
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin};
        saveSys.OnSaveResourceData(a);
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
    #endregion

    #region JSON HANDLERS
    public void OnLoadInventory()
    {
        InventoryData inventLoad = saveSys.Load();

        pWoods = inventLoad.resourceDatas[0];
        pIron = inventLoad.resourceDatas[1];
        pStone = inventLoad.resourceDatas[2];
        pFruits = inventLoad.resourceDatas[3];
        pCrop = inventLoad.resourceDatas[4];
        pCoin = inventLoad.resourceDatas[5];
    }
    public void OnAutoSaveInfors()
    {
        int[] a = { pWoods, pIron, pStone, pFruits, pCrop, pCoin };
        saveSys.OnSaveResourceData(a);
    }
    private void OnResetJSON() { saveSys.OnReset(); }
    #endregion
}
