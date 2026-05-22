using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TreeSC : MonoBehaviour
{
    [SerializeField] NoahSC noah;
    [SerializeField] GameObject wood;
    [HideInInspector] ArkMakingMNSC genCtr;
    int hitCount, hitDelay, hitTargets, randWoodDrop;
    Vector3 noahPos;
    void Start()
    {
        genCtr = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        hitCount = 0;
        randWoodDrop = Random.Range(1, 4);
        noah = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
        noahPos = noah.transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            print("in hit axe");
            OnHandleChoping();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Axe")
        {
            print("in exist axe");
            hitCount = 0;
            hitDelay = 0;
        }
    }
    private void OnSpawnWood()
    {
        for (int i = 1; i < randWoodDrop; i++)
        {
            Instantiate(wood, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
        }
    }
    private void OnHandleChoping()
    {
        hitDelay++;
        if (hitDelay == 2)
        {
            OnSpawnWood();
            genCtr.curTreeOnScreen--;
            Destroy(gameObject);
        }
    }
}
