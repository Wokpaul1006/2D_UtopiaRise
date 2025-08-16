using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepholderSC : MonoBehaviour
{
    private UtopiaManager manager;
    private int currentLvl;
    private int moveDir;
    private float moveSpd;
    private int countToDead;
    private int itselfIndex;
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        manager = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        currentLvl = manager.curLevel;
        moveSpd = 0f;
        if (manager.stepListOnScreen == 1)
        {
            itselfIndex = 1;
        }else if(manager.stepListOnScreen == 2)
        {
            itselfIndex = 2;
        }
        DecideTimeDead();
        DecideMovement();
    }
    private void DecideMovement()
    {
        if (currentLvl <= 10) { moveSpd = 0; }
        else if (currentLvl > 10 && currentLvl <= 20) { moveSpd = currentLvl + 0.25f *Time.deltaTime; }
        else if (currentLvl > 20 && currentLvl <= 40) { moveSpd = currentLvl + 0.5f * Time.deltaTime; }
        else if (currentLvl > 40 && currentLvl <= 80) { moveSpd = currentLvl + 0.75f * Time.deltaTime; }
        else if (currentLvl > 80 && currentLvl <= 100) { moveSpd = currentLvl + 1f * Time.deltaTime; }
        else if (currentLvl > 100) { moveSpd = currentLvl + currentLvl + 1.25f * Time.deltaTime; }
    }
    private void DecideTimeDead()
    {
        if(currentLvl <= 10) { countToDead = 8; }
        else if(currentLvl > 10 && currentLvl <= 20) { countToDead = 5; }
        else if (currentLvl > 20 && currentLvl <= 40) { countToDead = 4; }
        else if (currentLvl > 40 && currentLvl <= 80) { countToDead = 3; }
        else if (currentLvl > 80 && currentLvl <= 100) { countToDead = 2; }
        else if (currentLvl > 100) { countToDead = 1; }
    }
    private void Update()
    {
        if(currentLvl >= 20) { Movementation(); }
    }
    private void Movementation()
    {
        if (moveDir == 1 || moveDir == 0) transform.position += new Vector3(moveSpd * Time.deltaTime, 0, 0);
        else if (moveDir == -1) transform.position += new Vector3(-moveSpd * Time.deltaTime, 0, 0);

        if (transform.position.x <= -2) moveDir = 1;
        else if (transform.position.x >= 2) moveDir = -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(CountToDead());
            if (manager.stepListOnScreen == 1 || manager.stepListOnScreen == 2)
            {
                if (itselfIndex == 0)
                {
                    manager.step1InStepListToShow = null;
                    manager.stepListOnScreen = 1;
                }
                else if (itselfIndex == 1)
                {
                    manager.step2InStepListToShow = null;
                    manager.stepListOnScreen = 0;
                }
            }
        }
    }
    IEnumerator CountToDead()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
