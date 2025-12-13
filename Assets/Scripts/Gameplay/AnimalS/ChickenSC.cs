using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
