using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    SoundSC sfxMNs;
    MainThemeSC mainThemeMN;
    int themeAllow, sfxAllow;
    [SerializeField] GeneralContrlSC genCtrl;
    [SerializeField] Image themeLoud, themeMute, sfxLoud, sfxMute;
    private void Awake()
    {
        sfxMNs = GameObject.Find("OBJ_SoundControl").GetComponent<SoundSC>();
        mainThemeMN = GameObject.Find("OBJ_SoundControl").GetComponent<MainThemeSC>();
        themeAllow = PlayerPrefs.GetInt("soundState");
        sfxAllow = PlayerPrefs.GetInt("sfxState");
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
    }
    void Start()
    {    }
    public void CheckSound()
    {
        themeAllow = PlayerPrefs.GetInt("soundState");
        sfxAllow = PlayerPrefs.GetInt("sfxState");
        switch (themeAllow)
        {
            case 0:
                themeMute.gameObject.SetActive(true);
                themeLoud.gameObject.SetActive(false);
                mainThemeMN.MuteTheme();
                break;
            case 1:
                themeMute.gameObject.SetActive(false);
                themeLoud.gameObject.SetActive(true);
                mainThemeMN.PlayTheme();
                break;
        }

        switch(sfxAllow)
        {
            case 0:
                sfxMute.gameObject.SetActive(true);
                sfxLoud.gameObject.SetActive(false);
                sfxMNs.MuteSFX();
                break;
            case 1:
                sfxMute.gameObject.SetActive(false);
                sfxLoud.gameObject.SetActive(true);
                sfxMNs.PlaySFX();
                break;
        }
    }
    public void OnChangeThemState()
    {
        if(themeAllow == 1)
        {
            themeAllow = 0;
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            mainThemeMN.MuteTheme();

        }else if(themeAllow == 0)
        {
            themeAllow = 1;
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            mainThemeMN.PlayTheme();
        }
        PlayerPrefs.SetInt("soundState", themeAllow);
    }
    public void OnChangeSFXState()
    {
        if (sfxAllow == 1)
        {
            sfxAllow = 0;
            sfxMNs.MuteSFX();
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
        }
        else if (sfxAllow == 0)
        {
            sfxAllow = 1;
            sfxMNs.PlaySFX();
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
        }
        PlayerPrefs.SetInt("sfxState", sfxAllow);
    }
    public void ExitGame() => Application.Quit();
    public void ToPlayerInfo() => genCtrl.ShowInfor();
    public void ToGGPlayStore() => Application.OpenURL("https://play.google.com/store/apps/developer?id=Sadek+Games+Studio");
    public void ToPrivacyPolicy() => Application.OpenURL("https://sadekgame.wordpress.com/2025/07/13/privacy-policy-of-utopia-rise/");
}
