using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepholderSC : MonoBehaviour
{
    private ArcadeJumpSC gameCtr;
    private int currentLvl;
    private int moveDir;
    private float moveSpd;
    private int countToDead;
    private bool isAllowToScore;
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameCtr = GameObject.Find("CAN_UtopiaJump").GetComponent<ArcadeJumpSC>();
        currentLvl = gameCtr.curLevel;
        moveSpd = 0f;
        isAllowToScore = true;
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
            Invoke(nameof(SelfDestruct), countToDead);
            if(isAllowToScore == true)
            {
                gameCtr.IncreaseScore();
                isAllowToScore = false;
                gameCtr.CheckPlayerHieght();
            }
        }
    }
    private void SelfDestruct() => Destroy(gameObject);
}
