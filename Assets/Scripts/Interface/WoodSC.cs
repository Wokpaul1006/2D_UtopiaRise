using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSC : MonoBehaviour
{
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    void Start()
    {
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {
            Destroy(gameObject);
        }
    }
}
