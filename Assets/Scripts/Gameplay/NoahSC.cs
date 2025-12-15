using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahSC : MonoBehaviour
{
    [HideInInspector] Joystick joystickCtr;
    [HideInInspector] GeneralContrlSC genCtr;
    [HideInInspector] ArkMakingMNSC cutwoodMn;
    [SerializeField] Transform axe;
    //Gameplay Attributes
    private int deviceType;
    private bool isPause;

    //Player attribute
    private int noahDir;
    private float moveSpd;
    private float choppingDelay;
    private float axeMoveSpd;
    void Start()
    {
        moveSpd = 5f;
        axeMoveSpd = 5f;
        choppingDelay = 2f;
        noahDir = 0;
        genCtr = GameObject.Find("CAN_GenControl").GetComponent<GeneralContrlSC>();
        joystickCtr = GameObject.Find("IMG_JoystickHandle").GetComponent<Joystick>();
        cutwoodMn = GameObject.Find("CAN_ArkMaking").GetComponent<ArkMakingMNSC>();
        axe = transform.Find("OBJ_Axe");
        deviceType = genCtr.deviceType;
        //axe.transform.SetParent(gameObject.transform);
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
            if(noahDir == 0) { ChangeDir(); }
            transform.position += Vector3.left * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.D) == true)
        {
            if (noahDir == 1) { ChangeDir(); }
            transform.position += Vector3.right * Time.deltaTime * moveSpd;
        }
        else if(Input.GetKey(KeyCode.Space) == true && !isSpinning)
        {
            ///Choppings
            StartCoroutine(SpinOnce());
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tidal") { cutwoodMn.OnLoose(); }
    }

    #region Axe rotation
    private bool isSpinning = false;
    private float spinDuration = 0.3f;
    IEnumerator SpinOnce()
    {
        isSpinning = true;

        float elapsed = 0f;
        float totalRotation = 360f;

        Quaternion startRot = axe.localRotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, 0f, totalRotation);

        print("in rotate axxe");

        while (elapsed < spinDuration)
        {
            print("in elapse < spinDuration");
            elapsed += Time.deltaTime;
            float t = elapsed / spinDuration;
            axe.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        axe.localRotation = endRot;
        isSpinning = false;
        print("spin complete");
    }
    #endregion
}
