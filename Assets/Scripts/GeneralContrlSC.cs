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
    [HideInInspector] ShopPanelSC shopPnl;
    [HideInInspector] SoundSC sfxMuzik;
    [HideInInspector] MainThemeSC mainthemMuzik;
    [HideInInspector] UtopiaManager utopiaMN;
    [HideInInspector] CreditSC creditPnl;
    [HideInInspector] RatingSC reatingPnl;
    [HideInInspector] GameObject readmePnl, leaderPnl;

    public string toDay;
    void Start() => InitEnviroment();
    void InitEnviroment()
    {
        pausPnl = GameObject.Find("PNL_PausePnl").GetComponent<PauseSC>();
        settingPnl = GameObject.Find("PNL_SettingPnl").GetComponent<SettingSC>();
        losePnl = GameObject.Find("PNL_LosePnl").GetComponent<LoseSC>();
        playerPnl = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
        dailyrewardPnl = GameObject.Find("PNL_DailyReward").GetComponent<DailyRewardSC>();
        shopPnl = GameObject.Find("PNL_Shop").GetComponent<ShopPanelSC>();
        creditPnl = GameObject.Find("PNL_Credit").GetComponent<CreditSC>();
        reatingPnl = GameObject.Find("PNL_Rating").GetComponent<RatingSC>();
        readmePnl = GameObject.Find("PNL_ReadMe");
        leaderPnl = GameObject.Find("PNL_Leaderboard");
        sfxMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainthemMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();
        HideAllPanel();
        
        toDay = DateTime.Today.Day.ToString();
        settingPnl.CheckSound();
    }
    public void UpdateElement()
    {
        utopiaMN = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        losePnl.gameCtr = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
    }

    public void ShowSetting(bool isShow) => settingPnl.gameObject.SetActive(isShow);
    public void ShowPause(bool isShow) => pausPnl.gameObject.SetActive(isShow);
    public void ShowLose(bool isShow) => losePnl.gameObject.SetActive(isShow);
    public void ShowReward(bool isShow) => dailyrewardPnl.gameObject.SetActive(isShow);
    public void ShowShop(bool isShow) => shopPnl.gameObject.SetActive(isShow);
    public void ShowInfor(bool isShow) => playerPnl.gameObject.SetActive(isShow);
    public void ShowCredit(bool isShow) => creditPnl.gameObject.SetActive(isShow);
    public void ShowRating(bool isShow) => reatingPnl.gameObject.SetActive(isShow);

    private void CheckSoundSetting()
    {

    }
    public void UpdateUI()
    {
        utopiaMN.HandleUIs();
    }
    private void HideAllPanel()
    {
        leaderPnl.gameObject.SetActive(false);
        readmePnl.gameObject.SetActive(false);
        ShowSetting(false);
        ShowPause(false);
        ShowLose(false);
        ShowShop(false);
        ShowInfor(false);
        ShowCredit(false);
        ShowRating(false);
        ShowReward(false);
    }
}
