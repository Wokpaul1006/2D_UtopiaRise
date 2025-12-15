using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalFinderSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtrl;
    [SerializeField] GameObject goal01Img, goal02Img, goal03Img;
    [SerializeField] Text goalTxt01, goalTxt02, goalTxt03;
    [SerializeField] Text curLvTxt, curPointTxt, countdownTxt;
    [SerializeField] List<Sprite> animalImage = new List<Sprite>();
    [SerializeField] List<AnimalSC> animalList = new List<AnimalSC>();
    //Collect Item Mode

    public bool isGameStart;
    int randAnimalToSpawn;
    int curPoint, curLv, countdown;
    int curGoal01, curGoal02, curGoal03;
    void Start()
    {
        isGameStart = false;
        curPoint = 0;
        countdown = 60;
        curLv = 1;
        genCtrl = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        SelectGoal();
        genCtrl.AssitsGamemode(4);
        InvokeRepeating(nameof(Countdown), 5f, 1f);
        InvokeRepeating(nameof(ChooseAnimalToSpawn), 5f, 1f);
        isGameStart = true;
    }


    #region Handle Interface Events
    private void HandleUIs()
    {
        //call each time have change on infor panel
        curLvTxt.text = curLv.ToString();
        curPointTxt.text = curPoint.ToString();
        countdownTxt.text = countdown.ToString()+"s";
    }
    public void OnPause()
    {
        //Handle pause game
        isGameStart = false;
        genCtrl.ShowPause(true);
    }
    private void OnLooseGame()
    {
        isGameStart = false;
        OnUpdatePlayerScore();
        genCtrl.ShowLose(true);
    }
    #endregion

    #region Gameplay Handler
    public void OnUpdatePlayerScore()
    {
        genCtrl.OnUpdatePlayerInformationAfterGames(curPoint);
    }
    private void CaculatingNewCountdown()
    {

    }
    private void Countdown()
    {
        if(isGameStart == true)
        {
            countdown--;
            if (countdown <= 0)
            {
                countdown = 0;
                isGameStart = false;
                OnLooseGame();
            }
        }
        HandleUIs();
    }
    private void SpawnAnimal(int a)
    {

        float tempXA, tempYA, tempXB, tempYB;
        Vector3 randPosToSpawn;
        tempXA = Random.Range(-10, 10);
        tempXB = Random.Range(-10, 10);
        tempYA = Random.Range(-10, 10);
        tempYB = Random.Range(-10, 10);
        for (int i = 0; i<2; i++)
        {  
            if(i == 0)
            {
                randPosToSpawn = new Vector3(tempXA, tempYA, 0);
                Debug.Log("randPosToSpawn A = " + randPosToSpawn);
                Instantiate(animalList[a], randPosToSpawn, Quaternion.identity);
            }else if(i != 0)
            {
                randPosToSpawn = new Vector3(tempXB, tempYB, 0);
                Debug.Log("randPosToSpawn B = " + randPosToSpawn);
                Instantiate(animalList[a], randPosToSpawn, Quaternion.identity);
            }
        }
    }
    private void ChooseAnimalToSpawn()
    {
        if(isGameStart == true)
        {
            randAnimalToSpawn = Random.Range(-1, animalList.Count);
            SpawnAnimal(randAnimalToSpawn);
            randAnimalToSpawn = -1;
        }
       
    }
    private void SelectGoal()
    {
        int randAnimalToGoal1, randAnimalToGoal2, randAnimalToGoal3;
        if(curLv < 10)
        {
            //1 Goal
            goal02Img.gameObject.SetActive(false);
            goalTxt02.gameObject.SetActive(false);
            goal03Img.gameObject.SetActive(false);
            goalTxt03.gameObject.SetActive(false);

            randAnimalToGoal1 = Random.Range(0, animalImage.Count);

            goal01Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal1];
            curGoal01 = curLv * 2;
            goalTxt01.text = curGoal01.ToString();
        }
        else if(curLv >= 10 && curLv <= 50)
        {
            //2 Goal
            goal03Img.gameObject.SetActive(false);
            goalTxt03.gameObject.SetActive(false);
            randAnimalToGoal1 = Random.Range(-1, animalImage.Count);
            randAnimalToGoal2 = Random.Range(-1, animalImage.Count);

            goal01Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal1];
            curGoal01 = curLv * 2;
            goalTxt01.text = curGoal01.ToString();

            goal02Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal2];
            curGoal02 = curLv * 2;
            goalTxt02.text = curGoal02.ToString();
        }
        else if(curLv > 50)
        {
            //3 Goal
            randAnimalToGoal1 = Random.Range(-1, animalImage.Count);
            randAnimalToGoal2 = Random.Range(-1, animalImage.Count);
            randAnimalToGoal3 = Random.Range(-1, animalImage.Count);

            goal01Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal1];
            curGoal01 = curLv * 2;
            goalTxt01.text = curGoal01.ToString();

            goal02Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal2];
            curGoal02 = curLv * 2;
            goalTxt02.text = curGoal01.ToString();

            goal03Img.gameObject.GetComponent<Image>().sprite = animalImage[randAnimalToGoal3];
            curGoal03 = curLv * 2;
            goalTxt03.text = curGoal01.ToString();
        }
    }
    public void OnIncreasePoint(string a)
    {
        print("animal name = " + a);
        if(curLv < 10)
        {

        }else if (curLv >= 10 && curLv <= 50)
        {

        }else if(curLv > 50)
        {

        }
        //CaculatingNewCountdown();
    }
    #endregion
}
