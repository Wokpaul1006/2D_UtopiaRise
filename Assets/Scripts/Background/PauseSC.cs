using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : Singleton<PauseSC>
{
    private void Start()
    {    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        //Resume All Corotine
    }
}
