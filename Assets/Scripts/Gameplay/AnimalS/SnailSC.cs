using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
