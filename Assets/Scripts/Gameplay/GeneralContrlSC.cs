using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GeneralContrlSC : Singleton<GeneralContrlSC>
{
    [HideInInspector] PauseSC pausPnl;
    [HideInInspector] SettingSC settingPnl;
    [HideInInspector] LoseSC losePnl;
    [HideInInspector] PlayerInforSC playerPnl;
    [HideInInspector] DailyRewardSC dailyrewardPnl;
    [HideInInspector] ShopSC shopCtr;
    [HideInInspector] SoundSC sfxMuzik;
    [HideInInspector] MainThemeSC mainthemMuzik;
    [HideInInspector] ArcadeJumpSC arcadeCtr;
    [HideInInspector] ArkMakingMNSC cutwoodCtr;
    [HideInInspector] AnimalFinderSC animalSaver;
    [HideInInspector] CreditSC creditPnl;
    [HideInInspector] RatingSC reatingPnl;
    [HideInInspector] SceneMN sceneCtr;
    [HideInInspector] GameObject readmePnl, tutotialPnl;
    [HideInInspector] LeaderSC leaderPnl;
    [HideInInspector] AchievementSC achievePnl;
    [HideInInspector] DataSC data;
    [SerializeField] AdsMN adsMN;

    public string toDay;
    public int gameMode, deviceType, genThemeAllow, genSFXAllow;
    int adsInterCount, adsRewardCount, targetIntersAdsCount, targetRewardAdsCount;
    void Start() 
    {
        adsInterCount = 0;
        adsRewardCount = 0;
        targetIntersAdsCount = 3;
        targetRewardAdsCount = 5;
        gameMode = 0;
        CheckDeviceType();
        InitEnviroment();
        Invoke(nameof(AssistAdsMn), 10f);
        Invoke(nameof(ShowRatePnl), 600f);
    } 
    void InitEnviroment()
    {
        pausPnl = GameObject.Find("PNL_PausePnl").GetComponent<PauseSC>();
        settingPnl = GameObject.Find("PNL_SettingPnl").GetComponent<SettingSC>();
        losePnl = GameObject.Find("PNL_LosePnl").GetComponent<LoseSC>();
        playerPnl = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
        dailyrewardPnl = GameObject.Find("PNL_DailyReward").GetComponent<DailyRewardSC>();
        shopCtr = GameObject.Find("PNL_Shop").GetComponent<ShopSC>();
        creditPnl = GameObject.Find("PNL_Credit").GetComponent<CreditSC>();
        reatingPnl = GameObject.Find("PNL_Rating").GetComponent<RatingSC>();
        readmePnl = GameObject.Find("PNL_ReadMe");
        leaderPnl = GameObject.Find("PNL_Leaderboard").GetComponent<LeaderSC>();
        achievePnl = GameObject.Find("PNL_Achievement").GetComponent<AchievementSC>();
        tutotialPnl = GameObject.Find("PNL_Tutorial");
        sfxMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainthemMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();
        sceneCtr = GameObject.Find("CAN_GenControl").GetComponent<SceneMN>();
        data = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();
        HideAllPanel();
        
        toDay = DateTime.Today.Day.ToString();
        CheckSoundOnStart();
    }

    #region Handle Panels Visibles
    public void ShowSetting(bool isShow) => settingPnl.gameObject.SetActive(isShow);
    public void ShowPause(bool isShow) => pausPnl.gameObject.SetActive(isShow);
    public void ShowLose(bool isShow) => losePnl.gameObject.SetActive(isShow);
    public void ShowReward(bool isShow) => dailyrewardPnl.gameObject.SetActive(isShow);
    public void ShowShop(bool isShow) => shopCtr.gameObject.SetActive(isShow);
    public void ShowInfor(bool isShow) => playerPnl.gameObject.SetActive(isShow);
    public void ShowCredit(bool isShow) => creditPnl.gameObject.SetActive(isShow);
    public void ShowRating(bool isShow) => reatingPnl.gameObject.SetActive(isShow);
    public void ShowAchievement(bool isShow) => achievePnl.gameObject.SetActive(isShow);
    public void ShowLeader(bool isShow) => leaderPnl.gameObject.SetActive(isShow);
    public void ShowTutorial(bool isShow) => tutotialPnl.gameObject.SetActive(isShow);
    #endregion

    private void CheckDeviceType()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            deviceType = 1;
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer)
        {
            deviceType = 2;
        }
    }
    private void CheckSoundOnStart()
    {
        genThemeAllow = data.pTheme;
        genSFXAllow = data.pSFX;
        switch (genThemeAllow)
        {
            case 0:
                mainthemMuzik.MuteTheme();
                break;
            case 1:
                mainthemMuzik.PlayTheme();
                break;
        }

        switch (genSFXAllow)
        {
            case 0:
                sfxMuzik.MuteSFX();
                break;
            case 1:
                sfxMuzik.PlaySFX();
                break;
        }
    }
    public void UpdateUI()
    {
        arcadeCtr.HandleUIs();
    }
    private void HideAllPanel()
    {
        leaderPnl.gameObject.SetActive(false);
        readmePnl.gameObject.SetActive(false);

        ShowPause(false);
        ShowLose(false);
        ShowShop(false);
        ShowInfor(false);
        ShowCredit(false);
        ShowRating(false);
        ShowReward(false);
        ShowAchievement(false);
        ShowLeader(false);
        ShowTutorial(false);
        ShowSetting(false);
    }
    public void GenLoadScene(sbyte orderScene) 
    {
        gameMode = orderScene;
        sceneCtr.OnLoadScene(orderScene);
    }
    public void AssitsGamemode(int a)
    {
        switch (a)
        {
            case 1:
                //Home
                break;
            case 2:
                arcadeCtr = GameObject.Find("CAN_UtopiaJump").GetComponent<ArcadeJumpSC>();
                break;
            case 3:
                cutwoodCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
                break;
            case 4:
                animalSaver = GameObject.Find("CAN_AnimalSecure").GetComponent<AnimalFinderSC>();
                break;
        }
    }

    #region Handle Loose Events
    public void OnToHome()
    {
        gameMode = 0;
        adsInterCount += 1;
        if(adsInterCount >= targetIntersAdsCount)
        {
            adsMN.ShowAds(1);
            sceneCtr.OnLoadScene(1);
            adsInterCount = 0;
        }else if(adsInterCount < targetIntersAdsCount)
        {
            sceneCtr.OnLoadScene(1);
        }
    }
    public void OnReplay()
    {
        //Load Reward
        //sceneCtr.OnLoadScene("");
    }
    public void OnExistGame()
    {
        Application.Quit(0);
    }
    #endregion

    private void AssistAdsMn()
    {
        adsMN = GameObject.Find("CAN_GenControl").GetComponent<AdsMN>();
        if (adsMN == null) { print("adsMN null"); }
    }
    private void ShowRatePnl()
    {
        //ratePnl.gameObject.SetActive(true);
    }
    public void OnResumeGame()
    {
        if(gameMode == 3)
        {
            cutwoodCtr.isGameStart = true;
        }else if(gameMode == 2)
        {
            arcadeCtr.isGameStart = true;
        }
        ShowPause(false);
    }
    public void OnPlayTheme(int state)
    {
        if(state == 0)
        {
            mainthemMuzik.MuteTheme();
        }else if(state == 1)
        {
            mainthemMuzik.PlayTheme();
        }
        data.UpdateThemeState(state);
    }
    public void OnPlaySFX(int state)
    {
        if (state == 0)
        {
            sfxMuzik.MuteSFX();
        }
        else if (state == 1)
        {
            sfxMuzik.PlaySFX();
        }
        data.UpdateSFXState(state);
    }
}
