using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSC : AnimalSC
{
    // Start is called before the first frame update
    void Start()
    {
        animalName = this.name;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
