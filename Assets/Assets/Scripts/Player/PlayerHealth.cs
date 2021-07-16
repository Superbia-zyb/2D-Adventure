using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : Player
{

	// Use this for initialization
	// Update is called once per frame
	private CapsuleCollider2D coll;
	public float delaytime;
	private bool is_hit;
	
	private float lasttim;
	private float timleft;
	private bool isbuffing = true;

	void Update()
	{
	}
	private Rigidbody2D body;
	private void Readytobuff()
	{
		isbuffing = true;
		timleft = (float) PlayerManager.y6;
		lasttim = Time.time;
	}
	private Object manager;
	void PlayerDamage(int x)
	{
		if (PlayerManager.PassiveSkill == 6)
		{
			if (Time.time >= (lasttim + 0.5)) Readytobuff();
			if (isbuffing)
			{
				if (timleft > 0)
				{
					mybody.velocity = new Vector2( (float)(100 + PlayerManager.x6)/100 * mybody.velocity.x, mybody.velocity.y);
					timleft -= Time.deltaTime;
				}
				if (timleft <= 0) isDashing = false;
			}
		}

		if (is_hit == false)
		{
			if (Player.isDashing) return;
			is_hit = true;
			Invoke("Set_hit", 0.4f);
			heart -= x;
			if (heart <= 0)
			{
				heart = 0;
				body = GetComponent<Rigidbody2D>();
				is_died = true;
				Vector2 dead = new Vector2(0, body.velocity.y);
				body.velocity = dead;

				manager = GameObject.Find("PlayerManager");
				PlayerManager.reset();
				GameObject.Destroy(manager);

				myAnim.SetTrigger("died");
				Invoke("died", delaytime);
			}
			else myAnim.SetTrigger("hit");
		}
	}
	private void Set_hit()
	{
		is_hit = false;
	}
	private void died()
	{
		// 摧毁
		SceneManager.LoadScene("0-初始界面");
	}
}