using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSC : MonoBehaviour
{
    [SerializeField] GameObject wood;
    int hitCount, hitTargets;
    void Start()
    {
        hitTargets = 5;
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Axe")
        {
            hitCount++;
            if(hitCount >= hitTargets)
            {
                OnSpawnWood();
                Destroy(gameObject);
            }
        }
    }
    private void OnSpawnWood()
    {
        Instantiate(wood, gameObject.transform.position, Quaternion.identity);
    }
}
