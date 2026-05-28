using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AnimalSC : MonoBehaviour
{
    Vector3 previousPos, newPos;
    internal bool isPredators;
    internal int nutritionAmount;
    protected virtual void Start()
    {
        previousPos = gameObject.transform.position;
        InvokeRepeating(nameof(WanderAround), 0f, 2f);
    }

    void WanderAround()
    {
        float tempX, tempY;
        tempX = Random.Range((float)previousPos.x - 0.25f, (float)previousPos.x + 0.25f);
        tempY = Random.Range((float)previousPos.y - 0.25f, (float)previousPos.y + 0.25f);
        newPos = new Vector3(tempX, tempY, 0);
        transform.DOMove(newPos, 1f);
        previousPos = newPos;
    }
}
