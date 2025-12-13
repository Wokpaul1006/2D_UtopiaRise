using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
