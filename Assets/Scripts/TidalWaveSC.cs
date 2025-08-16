using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveSC : MonoBehaviour
{
    Rigidbody2D rb;
    public UtopiaManager utopiaMN;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        utopiaMN = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        DecideGravityScale();
    }
    public void DecideGravityScale()
    {
        if(utopiaMN.curLevel <= 20)
        {
            rb.gravityScale = -0.0001f;
        }else if(utopiaMN.curLevel > 20 && utopiaMN.curLevel<= 60)
        {
            rb.gravityScale = -0.01f;
        }
        else if(utopiaMN.curLevel > 60 && utopiaMN.curLevel < 80)
        {
            rb.gravityScale = -0.5f;
        }else if(utopiaMN.gameState == 0 || utopiaMN.gameState == 2)
        {
            rb.gravityScale = 0;
        }
    }
}
