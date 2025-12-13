using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrexSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
