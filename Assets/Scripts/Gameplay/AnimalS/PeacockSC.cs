using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeacockSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
