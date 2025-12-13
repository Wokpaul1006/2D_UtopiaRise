using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
