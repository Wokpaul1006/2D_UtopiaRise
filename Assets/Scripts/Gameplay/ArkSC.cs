using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class ArkSC : MonoBehaviour
{
    [HideInInspector] ArcadeJumpSC gameCtr;
    [HideInInspector] Rigidbody2D rb;
    [SerializeField] GameObject parent;

    Vector3 lastPos;
    Vector3 originPos;

    private bool isGrounded, isStartGame;   
    public int touchSwipeDir, arkDir;
    private int pHeightJump;
    public int pDeviceType;

    //Player Static Properties
    private float jumpForce = 6.5f; //jump
    private float forwardSpd = 0.5f; //move forward
    private int   livesAmountTotal, liveAmoutnLeft;
    private void Awake()
    {
        gameCtr = GameObject.Find("CAN_UtopiaJump").GetComponent<ArcadeJumpSC>();
        touchSwipeDir = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        isStartGame = false;
        isGrounded = true;
        livesAmountTotal = 3;
        liveAmoutnLeft = livesAmountTotal;
        pHeightJump = gameCtr.pHeightJumped;
        arkDir = 0;
        pDeviceType = gameCtr.deviceType;
    }
    void Start()
    {
        parent = GameObject.Find("OBJ_CharParent");
        originPos = gameObject.transform.position;
    }
    void Update()
    {
        if (pDeviceType == 1)
        {
            ArkJumpByKey();
            if (Input.GetKeyDown(KeyCode.R)) ChangDir();
        }
        else if (pDeviceType == 2)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) == false)
                    {
                        ArkJumpByTouch();
                    }
                }
            }
        }
        if (gameCtr.gameState == 1) CharForward();
        if (gameObject.transform.position.x <= -3 || gameObject.transform.position.x >= 3) ChangDir();
    }
    #region collision detect
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
            if (isStartGame != true) isStartGame = true;
            lastPos = gameObject.transform.position;
            gameCtr.newestStepPos = gameCtr.nextStepPos;
            if (gameCtr.newestStepPos != lastPos)
            {
                //Anti increase score of repeating jump in one pos

                pHeightJump++;
                gameCtr.pHeightJumped = pHeightJump;
            }
        }
        else if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            if(isGrounded == true && isStartGame == true || isGrounded == false && isStartGame == true)
            {
                //Case of decrease life
                CaculatingCharDeath();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tidal") { CaculatingCharDeath(); }
    }
    #endregion

    #region Gamelay Handler
    private void CaculatingCharDeath()
    {
        if (liveAmoutnLeft > 0 && liveAmoutnLeft <= livesAmountTotal)
        {
            liveAmoutnLeft--;
            gameCtr.IsShowLive(liveAmoutnLeft);
            Invoke(nameof(CharRevive), 3f);
        }
        else if (liveAmoutnLeft <= 0)
        {
            gameCtr.UpdateGameState(2);
            gameCtr.OnShowLoose();
            Destroy(gameObject);
        }
    }
    private void CharForward()
    {
        if (arkDir == 0) transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * forwardSpd);
        else if (arkDir == 1) transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * forwardSpd);
    }
    private void CharRevive()
    {
        //Call everytime Char hit Flood or Ground
        //Find closed platform
        gameObject.transform.position = new Vector3(gameCtr.newestStepPos.x, gameCtr.newestStepPos.y + 0.5f, 0);
    }
    #endregion

    #region PC control
    private void ArkJumpByKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            print("1. in press jump");
            print("1.1. isGrounded = " + isGrounded);
            if (isGrounded == true)
            {
                print("2. in jump complete");
                isGrounded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //gameCtr.DecideStepSpawn(); //Caculating new step
            }
        }
    }
    #endregion

    #region Mobile Control
    private void ArkJumpByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (isGrounded == true && touch.phase == TouchPhase.Began)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
                //gameCtr.DecideStepSpawn(); //Caculating new step
            }
        }
    }
    public void ChangDir()
    {
        if (arkDir == 0)
        {
            arkDir = 1;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(-prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
        else if (arkDir == 1)
        {
            arkDir = 0;
            float prefabCurentScale = gameObject.transform.localScale.y;
            gameObject.transform.localScale = new Vector3(prefabCurentScale, prefabCurentScale, prefabCurentScale);
        }
    }
    #endregion
}
