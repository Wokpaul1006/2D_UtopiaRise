using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
