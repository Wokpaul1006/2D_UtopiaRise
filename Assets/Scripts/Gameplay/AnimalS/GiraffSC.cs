using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
