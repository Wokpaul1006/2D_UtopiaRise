using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
