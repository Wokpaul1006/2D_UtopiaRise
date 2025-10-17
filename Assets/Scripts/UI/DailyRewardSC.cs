using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GeneralContrlSC genCtr;
    [SerializeField] List<GameObject> rewardDailyLocker = new List<GameObject>();
    [HideInInspector] UtopiaManager menu;
    [SerializeField] List<GameObject> dailyGrid = new List<GameObject>();

    [SerializeField] Button claimDailyBtn;

    private const string LastPatrolTimeKey = "LastPatrolTime";
    private const string PatrolStreakKey = "PatrolStreak";
    public int baseReward = 10; // example reward, x2 for each time count
    public int rewardToGive;
    private bool isAllowDailyClaim;
    private int streakDaily;
    private string lastCollectDay;
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();

        isAllowDailyClaim = false;
        streakDaily = data.pDailyStreak;
        lastCollectDay = "";
        rewardToGive = 0;
        ShowRewardDaily();

        print("streak Daily = " + streakDaily);
        print("Today = " + genCtr.toDay);
        print("lastCollectDay = " + data.pLastDailyClaim);
    }
    public void OnClosePanel() => genCtr.UpdateUI();

    #region Handle Claim Daily
    void ShowRewardDaily()
    {
        if (genCtr.toDay != data.pLastDailyClaim)
        {
            print("in enable button");
            claimDailyBtn.GetComponent<Button>().interactable = true;
            if (streakDaily >= 1 && streakDaily < 8)
            {
                for (int i = 0; i <= streakDaily - 1; i++)
                {
                    rewardDailyLocker[i].gameObject.SetActive(false);
                }
                isAllowDailyClaim = true;
            }
        }
        else if (genCtr.toDay == data.pLastDailyClaim)
        {
            print("in disable button");
            isAllowDailyClaim = false;
            claimDailyBtn.GetComponent<Button>().interactable = false;
        }
    }
    public void OnClaimDaily()
    {
        int tempFinalScoreToOverride;
        SelectRewardDaily();
        print("baseReward = " + baseReward);
        tempFinalScoreToOverride = baseReward + data.pTotalScore;
        data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
        streakDaily++;
        data.UpdateStreak(1, streakDaily); //Update streak
        lastCollectDay = DateTime.Today.Day.ToString();
        data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
        isAllowDailyClaim = false;
        ShowRewardDaily();
    }
    private void SelectRewardDaily()
    {
        switch (streakDaily)
        {
            case 0:
                baseReward = 10;
                break;
            case 1:
                baseReward = 20;
                break;
            case 2:
                baseReward = 40;
                break;
            case 3:
                baseReward = 80;
                break;
            case 4:
                baseReward = 160;
                break;
            case 5:
                baseReward = 320;
                break;
            case 6:
                baseReward = 640;
                break;
            case 7:
                baseReward = 1280;
                break;
        }
    }
    #endregion
}
