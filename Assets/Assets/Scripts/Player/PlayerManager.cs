using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int PassiveSkill = 0;
    public static int ActiveSkill = 0;
    public static bool use = false;
    public static double x1 = 20, x2 = 20, x3 = 20, x4 = 20, x5 = 5, x6 = 20, y6 = 3;

    public static float movespeed = 4.5f;
    public static int heart = 20;
    public static int damage = 2;

    public static float jumpspeed = 8;
    public static float climbspeed = 3;
    public static float doublejumpspeed = 5;

    public static bool dash = false;
    public static float dashSpeed = 4f;
    public static float dashCooldown = 2f;
    public static float dashTime = 0.3f;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
    }
    public static void reset()
    {
        PassiveSkill = 0;
        ActiveSkill = 0;
        use = false;
        x1 = 20;
        x2 = 20;
        x3 = 20; 
        x4 = 20; 
        x5 = 5; 
        x6 = 20;
        y6 = 3;

        movespeed = 4.5f;
        heart = 20;
        damage = 2;

        jumpspeed = 9;
        climbspeed = 3;
        doublejumpspeed = 6;

        dash = false;
        dashSpeed = 4;
        dashCooldown = 2f;
        dashTime = 0.4f;
    }
}
