using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool is_door;
    private bool is_entering;
    public int skill;
    public int activeskill;
    Scene scene;
    void Start()
    {
        is_entering = false;
        is_door = false;
        scene = SceneManager.GetActiveScene();
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            if (is_door && is_entering == false)
            {
                if (scene.name != "0-初始界面") 
                        PlayerManager.heart = PlayerManager.heart + 3;
                
                PlayerManager.movespeed += 0.02f;
                PlayerManager.jumpspeed += 0.02f;
                PlayerManager.damage += 2;
                if (scene.name != "0-初始界面" || scene.name != "1-获得冲锋")
                {
                    PlayerManager.dashSpeed += 0.01f;
                    if (scene.name != "1-转场" || scene.name != "2-获得被动")
                    {
                        PlayerManager.x1 += 2;
                        PlayerManager.x2 += 2;
                        PlayerManager.x3 += 2;
                        PlayerManager.x4 += 2;
                        PlayerManager.x6 += 0.8;
                        PlayerManager.y6++;
                    }
                }
                Debug.Log(skill);
                is_entering = true;
                Invoke("empt", 0.3f);
                if (scene.name == "0-初始界面") SceneManager.LoadScene("1-获得冲锋");
                if (scene.name == "1-获得冲锋")
                {
                    PlayerManager.dash = true;
                    SceneManager.LoadScene("1-转场");
                }
                if (scene.name == "1-转场")
                {
                    PlayerManager.PassiveSkill = skill;
                    SceneManager.LoadScene("2-获得被动");
                }
                if (scene.name == "2-获得被动")
                {
                    PlayerManager.use = true;
                    SceneManager.LoadScene("3-转场");
                }
                if (scene.name == "3-转场")
                {
                    PlayerManager.ActiveSkill = activeskill;
                    if (activeskill == 1) SceneManager.LoadScene("3-1 获得技能");
                    else SceneManager.LoadScene("3-2 获得技能");
                }
                if (scene.name == "3-1 获得技能") SceneManager.LoadScene("4-1 闯关");
                if (scene.name == "3-2 获得技能") SceneManager.LoadScene("4-1 闯关");
                if (scene.name == "4-1 闯关") SceneManager.LoadScene("4-2 闯关");
                if (scene.name == "4-2 闯关") SceneManager.LoadScene("4-3 闯关");
                if (scene.name == "4-3 闯关") SceneManager.LoadScene("4-4 闯关");
                if (scene.name == "4-4 闯关") SceneManager.LoadScene("5-1 BOSS");
                if (scene.name == "5-1 BOSS") SceneManager.LoadScene("6-结束界面");
            }
    }
    void empt()
    {
        is_entering = false;
    }
    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString() == "UnityEngine.CapsuleCollider2D") is_door = true; 
    }
    private void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString() == "UnityEngine.CapsuleCollider2D") is_door = false;
    }
}
