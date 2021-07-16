using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	public SpriteRenderer sr;
	public Color oricolor;
	public int heart;
	public int demage;
	public float flashtime;
	public GameObject blood;
	public void Start()
	{
		sr = GetComponent<SpriteRenderer>();
	}
	public void Attack()
	{

	}
	public void Damage(int x)
	{
		heart -= x;
		flashcolor(flashtime);
		Instantiate(blood, transform.position,Quaternion.identity);
	}
	public void flashcolor(float time)
	{
		sr.color = Color.red;
		Invoke("Resetcolor", time);
	}
	public void Resetcolor()
	{
		sr.color = oricolor;
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
		{
			Debug.Log("damage");
			switch (collision.tag)
			{ 
				case "Player" :
					if (Player.isDashing) Damage(1);
					else
					{
						int tmp = 0;
						if (PlayerManager.use && PlayerManager.PassiveSkill == 4)
						{
							int rand = Random.Range(1, 101);
							if (rand <= PlayerManager.x4) tmp = 1;
						}

						if (Player.myAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Bash"))
						{
							collision.SendMessage("PlayerDamage", 2 * demage);
							if (tmp == 1) Damage(2 * demage);
						}
						else
						{
							collision.SendMessage("PlayerDamage", demage);
							if (tmp == 1) Damage(demage);
						}
					}
					break;
				default:
					break;
			}
		}
	}
}
