using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSC : MonoBehaviour
{
    [SerializeField] NoahSC noah;
    [SerializeField] GameObject wood;
    int hitCount, hitTargets, randWoodDrop;
    Vector3 noahPos;
    void Start()
    {
        hitCount = 0;
        randWoodDrop = Random.Range(0, 4);
        hitTargets = Random.Range(3, 10);
        noah = GameObject.Find("OBJ_Noah(Clone)").GetComponent<NoahSC>();
        noahPos = noah.transform.position;
        InvokeRepeating(nameof(CheckDistant), 0, 1f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            hitCount++;
            if (hitCount >= hitTargets)
            {
                OnSpawnWood();
                Destroy(gameObject);
            }
        }else if(collision.gameObject.tag == "Tidal")
        {
            Destroy(gameObject);
        }
    }
    private void OnSpawnWood()
    {
        for (int i = 1; i < randWoodDrop; i++)
        {
            Instantiate(wood, new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0), Quaternion.identity);
        }
    }
    private void CheckDistant()
    {
        float distance = Vector3.Distance(transform.position, noahPos);
        if (distance >= 5)
        {
            Destroy(gameObject);
        }
    }
}
