using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss : MonoBehaviour
{
    public static int heart ;
    public static int startheart ;
    // Use this for initialization
    public GameObject PlayerManager_obj;
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
    public Transform playerTransform;

    public void Start()
    {
        Application.targetFrameRate = 200;
        heart = PlayerManager.heart;
        startheart = PlayerManager.heart;

        heart = (int)(heart * (1 + PlayerManager.x1/100) );
        startheart = heart;
        
        movespeed = PlayerManager.movespeed;
        jumpspeed = PlayerManager.jumpspeed;
        climbspeed = PlayerManager.climbspeed;
        doublejumpspeed = PlayerManager.doublejumpspeed;
        dashSpeed = PlayerManager.dashSpeed;
        dashCoolDown = 1f;
        dashTime = PlayerManager.dashTime;
        is_died = false;
        isdoublejump = true;
        mybody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myfeet = GetComponent<BoxCollider2D>();
        playerGravity = mybody.gravityScale;
        Scene scene= SceneManager.GetActiveScene();
        if (scene.name == "3-2 获得技能" || (scene.name == "3-2 获得技能"))
        {
            heart -= 10;
            movespeed -=2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CheckAirStatus();
        Flip();
        Check_ground();
        Run();
        Jump();
        SwitchAnim();
        Dash();
    }
    public void Dash()
    {
        if (Time.time >= (lastDash + dashCoolDown)) ReadyToDash();
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                mybody.velocity = new Vector2(dashSpeed * mybody.velocity.x, mybody.velocity.y);
                dashTimeLeft -= Time.deltaTime;
                BossChargePool.instance.GetFormPool();
            }
            if (dashTimeLeft <= 0) isDashing = false;
        }
    }
    public void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
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
            int rand = UnityEngine.Random.Range(0, 2);
            if (rand == 1) return;
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
        float h=0;
        if (transform.position.x - playerTransform.position.x > 3f) h = -1;
        else if(transform.position.x - playerTransform.position.x < -3f) h = 1;

        Vector2 playervel = new Vector2(h * 3f, mybody.velocity.y);
        mybody.velocity = playervel;
        bool tmp = Math.Abs(mybody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("run", tmp);       
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
    void CheckAirStatus()
    {
       //isJumping = myAnim.GetBool("jump");
        isFalling = myAnim.GetBool("fall");
        isDoubleFalling = myAnim.GetBool("doublefall");
        isClimbing = myAnim.GetBool("climbing");
    }
    private CapsuleCollider2D coll;
    public float delaytime;
    private bool is_hit;

    private float lasttim;
    private float timleft;
    private bool isbuffing = true;


    private Rigidbody2D body;
    private void Readytobuff()
    {
        isbuffing = true;
        timleft = (float)PlayerManager.y6;
        lasttim = Time.time;
    }
    public void Damage(int x)
    {
        if (Time.time >= (lasttim + 0.5)) Readytobuff();
        if (isbuffing)
        {
            if (timleft > 0)
            {
                mybody.velocity = new Vector2((float)(100 + PlayerManager.x6) / 100 * mybody.velocity.x, mybody.velocity.y);
                timleft -= Time.deltaTime;
            }
            if (timleft <= 0) isDashing = false;
        }
        if (true)
        {
            is_hit = true;
            heart -= x;
            if (heart <= 0)
            {
                heart = 0;
                body = GetComponent<Rigidbody2D>();
                is_died = true;
                Vector2 dead = new Vector2(0, body.velocity.y);
                body.velocity = dead;
                myAnim.SetTrigger("died");

                Destroy(gameObject);
            }
            else myAnim.SetTrigger("hit");
        }
    }
    private void Set_hit()
    {
        is_hit = false;
    }
}
