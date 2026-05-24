using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NoahSC : MonoBehaviour
{
    [HideInInspector] Joystick joystickCtr;
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [SerializeField] GameObject weapon, curTarget;
    //Gameplay Attributes
    private int deviceType;
    private bool isPause;

    //Player attribute
    private int noahDir;
    private float moveSpd;
    private float choppingDelay;
    private float axeMoveSpd;
    Vector3 curTegetPos, originPos;
    void Start()
    {
        moveSpd = 5f;
        axeMoveSpd = 5f;
        choppingDelay = 2f;
        noahDir = 0;
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        joystickCtr = GameObject.Find("IMG_JoystickHandle").GetComponent<Joystick>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        deviceType = genCtr.deviceType;
        curTegetPos = originPos = Vector3.zero;
        curTarget = null;
    }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            print("in hit tree");
            curTarget = collision.gameObject;
            curTegetPos = curTarget.transform.position;
            print("curTreePos = " + curTegetPos);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        curTarget = null;
        curTegetPos = Vector3.zero;
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
            if(noahDir == 0) { ChangeDir(); }
            transform.position += Vector3.left * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.D) == true)
        {
            if (noahDir == 1) { ChangeDir(); }
            transform.position += Vector3.right * Time.deltaTime * moveSpd;
        }
    }
    private void OnMoveByTouch()
    {
        float horizontal = joystickCtr.Horizontal();
        float vertical = joystickCtr.Vertical();

        Vector3 direction = new Vector3(horizontal, vertical,0).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpd);
    }
    public void ChangeDir()
    {
        if (noahDir == 0)
        {
            noahDir = 1;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(-prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
        else if (noahDir == 1)
        {
            noahDir = 0;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
    }
    public void OnAttack()
    {
        originPos = weapon.transform.position;
        weapon.transform.DOMove(curTegetPos, 2f).SetLoops(2,LoopType.Yoyo);
    }

}
