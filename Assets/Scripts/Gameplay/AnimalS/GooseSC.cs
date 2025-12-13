using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseSC : AnimalSC
{
    void Start()
    {
        animalName = this.name;
        base.Start();
    }
}
