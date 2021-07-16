using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public static int heart ;
    public static int startheart ;
    // Use this for initialization

    public float movespeed;
    public float jumpspeed;
    public float climbspeed;
    public float doublejumpspeed;
    public float playerGravity;
    public static Animator myAnim;
    public Rigidbody2D mybody;
    private BoxCollider2D myfeet;
    static public bool isDashing;
    static public bool is_died;
    public float dashTime;//dash时长
    private float dashTimeLeft;//冲锋剩余时间
    private float lastDash = -10f;//上一次dash时间点
    public float dashCoolDown;
    public float dashSpeed;
    private bool isground;
    private bool isLadder;
    private bool isOneWayPlatform;
    private bool isdoublejump;
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleFalling;
    private bool isClimbing;
    public GameObject PlayerManager_obj;
    public void Start()
    {

        PlayerManager_obj=GameObject.Find("PlayerManager");
     
        Application.targetFrameRate = 200;
        heart = PlayerManager.heart;
        startheart = PlayerManager.heart;
        if (PlayerManager.use && PlayerManager.PassiveSkill == 1)
        {
            heart = (int)(heart * (1 + PlayerManager.x1 / 100));
            startheart = (int)(startheart * (1 + PlayerManager.x1 / 100));
        }
        movespeed = PlayerManager.movespeed;
        jumpspeed = PlayerManager.jumpspeed;
        climbspeed = PlayerManager.climbspeed;
        doublejumpspeed = PlayerManager.doublejumpspeed;
        dashSpeed = PlayerManager.dashSpeed;
        dashCoolDown = PlayerManager.dashCooldown;
        dashTime = PlayerManager.dashTime;
        is_died = false;
        isdoublejump = true;
        mybody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myfeet = GetComponent<BoxCollider2D>();
        playerGravity = mybody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (heart >= startheart) startheart = heart;
        if (transform.position.y <= -30) SceneManager.LoadScene("0-初始界面");
        CheckAirStatus();
        Flip();
        Check_ladder();
        Check_ground();
        Run();
        Jump();
        Climb();
        Descend();
        OneWayPlatFormCheck();
        SwitchAnim();
        Dash();
    }
    void Dash()
    {
        if (PlayerManager.dash == false) return ;
        if (Input.GetKeyDown(KeyCode.E))
            if (Time.time >= (lastDash + dashCoolDown)) ReadyToDash();
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                mybody.velocity = new Vector2(dashSpeed * mybody.velocity.x, mybody.velocity.y);
                dashTimeLeft -= Time.deltaTime;
                PlayerChargePool.instance.GetFormPool();
            }
            if (dashTimeLeft <= 0) isDashing = false;
        }
    }
    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }
    void Check_ladder()
    {
        isLadder = myfeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    void Check_ground()
    {
        if (myfeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"))) isOneWayPlatform = true;
        else isOneWayPlatform = false;
        if (myfeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || myfeet.IsTouchingLayers(LayerMask.GetMask("MovePlatform")) || myfeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform")))
            isground = true;
        else isground = false;
    }
    void Descend()
    {
        if (is_died == true) return;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (isground == false)
            {
                mybody.velocity = new Vector2(mybody.velocity.x, mybody.velocity.y - 0.4f);
            }
        }
    }
    void Flip()
    {
        if (is_died == true) return;
        bool tmp = Math.Abs(mybody.velocity.x) > Mathf.Epsilon;
        if (tmp)
        {
            if (mybody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (mybody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Jump()
    {
        if (is_died == true) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isground)
            {
                myAnim.SetBool("jump", true);
                mybody.velocity = Vector2.up * jumpspeed;
                isdoublejump = true;
                Debug.Log(isdoublejump);
            }
            else if (isground == false)
            {
                Debug.Log(isdoublejump);
                if (isdoublejump)
                {
                    myAnim.SetBool("fall", false);
                    myAnim.SetBool("jump", false);
                    myAnim.SetBool("doublejump", true);
                    Vector2 doublejumpvel = new Vector2(mybody.velocity.x, doublejumpspeed);
                    mybody.velocity = doublejumpvel;
                    isdoublejump = false;
                }
            }
        }
    }
    void Run()
    {
        if (is_died == true) return;
        float h = Input.GetAxis("Horizontal");
        Debug.Log(movespeed);
        Vector2 playervel = new Vector2(h * movespeed, mybody.velocity.y);
        mybody.velocity = playervel;
        bool tmp = Math.Abs(mybody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("run", tmp);
    }
    void OneWayPlatFormCheck()
    {
        if (isOneWayPlatform && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("Resetlayer", 0.47f);
        }
    }
    void Resetlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    void SwitchAnim()
    {
        if (is_died == true) return;
        myAnim.SetBool("stand", false);
        if (myAnim.GetBool("jump"))
        {
            if (mybody.velocity.y < 0.0f)
            {
                myAnim.SetBool("jump", false);
                myAnim.SetBool("fall", true);
            }
        }
        else if (isground)
        {
            myAnim.SetBool("fall", false);
            myAnim.SetBool("stand", true);
        }

        if (myAnim.GetBool("doublejump"))
        {
            if (mybody.velocity.y < 0.0f)
            {
                myAnim.SetBool("doublejump", false);
                myAnim.SetBool("doublefall", true);
            }
        }
        else if (isground)
        {
            myAnim.SetBool("doublefall", false);
            myAnim.SetBool("stand", true);
        }

        if (!isground) myAnim.SetBool("squat", false);
        if (isground && (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))) myAnim.SetBool("squat", false);
        if (isground && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))) myAnim.SetBool("squat", true);
    }
    void Climb()
    {
        float moveY = Input.GetAxis("Vertical");
        if (isClimbing)
        {
            mybody.velocity = new Vector2(mybody.velocity.x, moveY * climbspeed);
            isdoublejump = false;
        }
        if (isLadder)
        {
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("jump", false);
                myAnim.SetBool("doublejump", false);
                myAnim.SetBool("climbing", true);
                mybody.velocity = new Vector2(mybody.velocity.x, moveY * climbspeed);
                mybody.gravityScale = 0.0f;
            }
            else
            {
                if (isJumping || isFalling || isdoublejump || isDoubleFalling) myAnim.SetBool("climbing", false);
                else
                {
                    myAnim.SetBool("climbing", false);
                    mybody.velocity = new Vector2(mybody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("climbing", false);
            mybody.gravityScale = playerGravity;
        }
        if (isLadder && isground) mybody.gravityScale = playerGravity;
    }
    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("jump");
        isFalling = myAnim.GetBool("fall");
        isDoubleFalling = myAnim.GetBool("doublefall");
        isClimbing = myAnim.GetBool("climbing");
    }
}
