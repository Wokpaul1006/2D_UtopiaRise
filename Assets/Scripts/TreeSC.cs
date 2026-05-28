using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TreeSC : MonoBehaviour
{
    //Do check bonus
    [SerializeField] NoahSC noah;
    [SerializeField] GameObject wood, fruits;
    [HideInInspector] ArkMakingMNSC genCtr;
    int hitCount, randWoodDrop;
    int chanceDropFruits;
    void Start()
    {
        genCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        hitCount = 0;
        randWoodDrop = Random.Range(1, 4);
        chanceDropFruits = Random.Range(1, 100);
        noah = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            OnHandleChoping();
        }
    }
    private void OnSpawnWood()
    {
        for (int i = 1; i < randWoodDrop; i++)
        {
            Instantiate(wood, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
        }
    }
    private void OnSpawnFruist()
    {
        Instantiate(fruits, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
    }
    private void OnHandleChoping()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            OnSpawnWood();
            if (chanceDropFruits > 20) OnSpawnFruist();
            genCtr.curTreeOnScreen--;
            Destroy(gameObject);
        }
    }
}
