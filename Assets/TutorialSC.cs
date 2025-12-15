using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSC : MonoBehaviour
{
    [SerializeField] GameObject pnlTutorial01, pnlTutorial02, pnlTutorial03;
    void Start()
    {
        pnlTutorial01.gameObject.SetActive(true);
        pnlTutorial02.gameObject.SetActive(false);
        pnlTutorial03.gameObject.SetActive(false);
    }
}
