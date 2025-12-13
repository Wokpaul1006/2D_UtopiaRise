using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSC : MonoBehaviour
{
    Vector3 previousPos, newPos;
    internal string animalName;
    protected virtual void Start()
    {
        previousPos = gameObject.transform.position;
        InvokeRepeating(nameof(WanderAround), 0f, 2f);
        Invoke(nameof(SelfDestruct), 20f);
    }

    void WanderAround()
    {
        float tempX, tempY;
        tempX = Random.Range((float)previousPos.x - 0.5f, (float)previousPos.x + 0.5f);
        tempY = Random.Range((float)previousPos.y - 0.5f, (float)previousPos.y + 0.5f);
        newPos = new Vector3(tempX, tempY, 0);
        transform.position = newPos;
        previousPos = newPos;
    }
    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
