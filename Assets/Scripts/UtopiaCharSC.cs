using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class UtopiaCharSC : MonoBehaviour
{
    [HideInInspector] Rigidbody2D rb;
    [SerializeField] GameObject parent;
    
    Vector3 lastPos;

    private UtopiaManager gameCtr;
    private bool isGrounded, isDestroyWhenTouchGround;
    private float jumpForce = 6.5f; //jump
    private float forwardSpd = 0.5f; //move forward
    private int arkDir, livesAmountTotal, liveAmoutnLeft;
    public int touchSwipeDir;

    private void Awake()
    {
        touchSwipeDir = 1;
    }
    void Start()
    {
        SettingStart();
        gameCtr = GameObject.Find("UtopiaManager").GetComponent<UtopiaManager>();
        parent = GameObject.Find("OBJ_CharParent");
        parent.transform.position = gameObject.transform.position;
        gameCtr.changeDir.onClick.AddListener(ChangDir);
        //if(gameCtr.changeDir == null)
        //    Debug.LogError("Button reference is NULL");
        //else
        //    Debug.Log("Button reference OK");
    }
    void Update()
    {
        parent.transform.position = gameObject.transform.position;
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ArkJumpByKey();
            if (Input.GetKeyDown(KeyCode.R)) ChangDir();
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer)
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
            lastPos = gameObject.transform.position;
            gameCtr.newestStepPos = gameCtr.nextStepPos;
            if (gameCtr.newestStepPos != lastPos)
            {
                gameCtr.IncreaseScore();
                if (gameCtr.stepListOnScreen == 1 || gameCtr.stepListOnScreen == 0)
                {
                    gameCtr.DecideStepSpawn(); //Caculating new step
                }
            }
            isGrounded = true;
            if (isDestroyWhenTouchGround == false) isDestroyWhenTouchGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if(isDestroyWhenTouchGround == false) { isGrounded = true; }
            else if(isDestroyWhenTouchGround == true && liveAmoutnLeft > 0) 
            {
                liveAmoutnLeft--;
                gameCtr.IsShowLive(liveAmoutnLeft);
                CharRevive();
            }else if(isDestroyWhenTouchGround == true && liveAmoutnLeft <= 0)
            {
                gameCtr.UpdateGameState(2);
                gameCtr.OnShowLoose();
                Destroy(gameObject);
                gameCtr.timeBeKill++;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tidal")
        {
            if (liveAmoutnLeft <= 0)
            {
                gameCtr.UpdateGameState(2);
                gameCtr.OnShowLoose();
                Destroy(gameObject);
                gameCtr.timeBeKill++;
            }
            else if(liveAmoutnLeft > 0 && liveAmoutnLeft <= livesAmountTotal)
            {
                liveAmoutnLeft--;
                gameCtr.IsShowLive(liveAmoutnLeft);
                CharRevive();
            }    
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tidal")
        {
            if (liveAmoutnLeft <= 0)
            {
                gameCtr.UpdateGameState(2);
                gameCtr.OnShowLoose();
                Destroy(gameObject);
                gameCtr.timeBeKill++;
            }
            else if (liveAmoutnLeft > 0 && liveAmoutnLeft <= livesAmountTotal)
            {
                liveAmoutnLeft--;
                gameCtr.IsShowLive(liveAmoutnLeft);
                CharRevive();
            }
        }
    }
    #endregion
    #region Gamelay Handler
    void SettingStart()
    {
        isDestroyWhenTouchGround = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        isGrounded = true;
        livesAmountTotal = 3;
        liveAmoutnLeft = livesAmountTotal;
        float prefabCurentScale = gameObject.transform.localScale.y;
        gameObject.transform.localScale = new Vector3(prefabCurentScale, prefabCurentScale, prefabCurentScale);
    }
    private void CharForward()
    {
        if (arkDir == 0) transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * forwardSpd);
        else if (arkDir == 1) transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * forwardSpd);
    }
    private void CharRevive()
    {
        //Call everytime Char hit Flood
        gameObject.transform.position = gameCtr.newestStepPos;
    }
    #endregion
    #region PC control
    private void ArkJumpByKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            //gameCtr.DecideStepSpawn(); //Caculating new step
        }
    }
    #endregion
    #region Mobile Control
    private void ArkJumpByTouch()
    {
        print("in jump");
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

