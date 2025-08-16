using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralContrlSC : Singleton<GeneralContrlSC>
{
    PauseSC pausPnl;
    SettingSC settingPnl;
    LoseSC losePnl;
    PlayerInforSC playerPnl;
    DailyRewardSC dailyrewardPnl;
    ShopPanelSC shopPnl;
    SoundSC sfxMuzik;
    MainThemeSC mainthemMuzik;
    UtopiaManager utopiaMN;

    void Start() => InitEnviroment();
    void InitEnviroment()
    {
        pausPnl = GameObject.Find("PNL_PausePnl").GetComponent<PauseSC>();
        settingPnl = GameObject.Find("PNL_SettingPnl").GetComponent<SettingSC>();
        losePnl = GameObject.Find("PNL_LosePnl").GetComponent<LoseSC>();
        playerPnl = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
        dailyrewardPnl = GameObject.Find("PNL_DailyReward").GetComponent<DailyRewardSC>();
        shopPnl = GameObject.Find("PNL_Shop").GetComponent<ShopPanelSC>();

        sfxMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainthemMuzik = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();

        pausPnl.gameObject.SetActive(false);
        settingPnl.gameObject.SetActive(false);
        losePnl.gameObject.SetActive(false);
        playerPnl.gameObject.SetActive(false);
        dailyrewardPnl.gameObject.SetActive(false);
        shopPnl.gameObject.SetActive(false);

        settingPnl.CheckSound();
    }
    public void UpdateElement()
    {
        utopiaMN = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        losePnl.gameCtr = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
    }

    public void ShowSetting() => settingPnl.gameObject.SetActive(true);
    public void ShowPause() => pausPnl.gameObject.SetActive(true);
    public void ShowLose() => losePnl.gameObject.SetActive(true);
    public void ShowReward() => dailyrewardPnl.gameObject.SetActive(true);
    public void ShowShop() => shopPnl.gameObject.SetActive(true);
    public void ShowInfor() => playerPnl.gameObject.SetActive(true);

    private void CheckSoundSetting()
    {

    }
}
