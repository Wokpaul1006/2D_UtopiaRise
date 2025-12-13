using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahSC : MonoBehaviour
{
    [HideInInspector] Joystick joystickCtr;
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [SerializeField] GameObject axe;
    //Gameplay Attributes
    private int deviceType;
    private bool isPause;

    //Player attribute
    private float moveSpd;
    private float choppingDelay;
    private float axeMoveSpd;
    void Start()
    {
        moveSpd = 5f;
        axeMoveSpd = 5f;
        choppingDelay = 2f;
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        joystickCtr = GameObject.Find("IMG_JoystickHandle").GetComponent<Joystick>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        deviceType = genCtr.deviceType;
        axe.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(deviceType == 1)
        {
            OnMoveActionKey();
        }else if(deviceType == 2)
        {
            OnMoveByTouch();
        }
    }

    private void OnMoveActionKey()
    {
        if(Input.GetKey(KeyCode.W) == true)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.S) == true)
        {
            transform.position += Vector3.down * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.A) == true)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.D) == true)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.Space) == true)
        {
            ///Choppings
            ChoppingWood();
        }
    }
    private void OnMoveByTouch()
    {
        float horizontal = joystickCtr.Horizontal();
        float vertical = joystickCtr.Vertical();

        Vector3 direction = new Vector3(horizontal, vertical,0).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpd);
    }
    public void ChoppingWood()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tidal") { cutwoodMn.OnLoose(); }
    }
}
