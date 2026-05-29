using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewardSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [HideInInspector] GeneralContrlSC genCtr;
    [SerializeField] List<Button> butnList = new List<Button>();

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
        data = GameObject.Find("CAN_GenControl").GetComponent<DataSC>();

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
        void ShowRewardDaily()
        {
            if (data.pLastDailyClaim == "")
            {
                //First day of play
                isAllowDailyClaim = true;
                for (int i = 0; i < butnList.Count; i++)
                {
                    butnList[i].GetComponent<Button>().interactable = false;
                }
                butnList[0].GetComponent<Button>().interactable = true;
            }
            else
            {
                if (genCtr.toDay != data.pLastDailyClaim)
                {
                    //New day access + unclaimed
                    for (int i = 0; i < butnList.Count; i++)
                    {
                        butnList[streakDaily].GetComponent<Button>().interactable = false;
                    }

                    if (streakDaily >= 1 && streakDaily < 8)
                    {
                        //Lock previous day claim buttons
                        for (int i = 0; i < streakDaily; i++)
                        {
                            butnList[i].GetComponent<Button>().interactable = false;
                        }

                        for (int j = streakDaily + 1; j > butnList.Count; j++)
                        {
                            butnList[j].GetComponent<Button>().interactable = false;
                        }
                        butnList[streakDaily].GetComponent<Button>().interactable = true;

                        isAllowDailyClaim = false;
                    }
                }
                else if (genCtr.toDay == data.pLastDailyClaim)
                {
                    if (data.pAllowClaimDaily == 1)
                    {
                        //Same day access + claimed
                        isAllowDailyClaim = false;
                        for (int i = 0; i < butnList.Count; i++)
                        {
                            butnList[i].GetComponent<Button>().interactable = false;
                        }
                    }
                    else if (data.pAllowClaimDaily == 0)
                    {
                        //Same day, unclaimed
                        isAllowDailyClaim = true;
                        for (int i = 0; i < butnList.Count; i++)
                        {
                            butnList[i].GetComponent<Button>().interactable = false;
                        }
                        butnList[streakDaily].GetComponent<Button>().interactable = true; //Enable only able-to-claim button
                    }
                }
            }
        }
    }
    public void OnClaimDaily()
    {
        int tempFinalScoreToOverride;
        SelectRewardDaily();
        butnList[streakDaily].GetComponent<Button>().interactable = false;
        lastCollectDay = DateTime.Today.Day.ToString();
        isAllowDailyClaim = false;
        tempFinalScoreToOverride = rewardToGive;
        streakDaily++;
        data.UpdateAllowClaimDaily(1);

        print("tempFinalScoreToOverride = " + tempFinalScoreToOverride);

        data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
        data.UpdateStreak( streakDaily); //Update streak
        data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
        ShowRewardDaily();
        genCtr.UpdateUI();
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
