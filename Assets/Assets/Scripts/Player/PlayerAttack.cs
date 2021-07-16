using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerAttack : MonoBehaviour {

	public int damage;
	public float time;
	public float startime;
	private Animator anim;
	private PolygonCollider2D coll;
	private bool is_attack;
	private bool is_attack_skill;
	public int ttmp=0;
	// Use this for initialization
	void Start()
	{
		ttmp = 0;
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
		coll = GetComponent<PolygonCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		Attack();
	}
	void Attack()
	{
		if (Player.is_died == true) return;
		if (Input.GetMouseButtonDown(0)|| Player.isDashing)
		{
			ttmp++;
			if (is_attack == false || Player.isDashing)
			{
				is_attack = true;
				Invoke("Set_attack", 0.2f);
				anim.SetTrigger("attack");
			}
		}
		
		if (PlayerManager.ActiveSkill == 0) return ;
		if (Input.GetMouseButtonDown(1))
		{
			if (is_attack_skill == false)
			{
				is_attack_skill = true;
				Invoke("Set_attack_skill", 0.5f);
				if (PlayerManager.ActiveSkill == 1) anim.SetTrigger("bash");
				else if (PlayerManager.ActiveSkill == 2) anim.SetTrigger("threeattack");
			}
		}
	}
	void Set_attack()
	{
		is_attack = false;
	}
	void Set_attack_skill()
	{
		is_attack_skill = false;
	}
	IEnumerator able()
	{
		yield return new WaitForSeconds(startime);
		coll.enabled = true;
	}
	IEnumerator disable()
	{
		yield return new WaitForSeconds(time);
		coll.enabled = false;
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (Player.is_died == true) return;
		if (collision.tag == "Boss")
		{
			if (PlayerManager.use && PlayerManager.PassiveSkill == 5)
			{
				if (ttmp % PlayerManager.x5 == 0)
				{
					int rand = Random.Range(0, 2);
					if (rand == 0) collision.SendMessage("Damage", damage * 5);
					else
					{
						if (Player.startheart > Player.heart + damage) Player.heart = Player.heart + damage;
						else if (Player.startheart <= Player.heart + damage) Player.heart = Player.startheart;
					}
				}
				else collision.SendMessage("Damage", damage);
			}
			else if (PlayerManager.use && PlayerManager.PassiveSkill == 2)
			{
				int rand = Random.Range(1, 101);
				if (rand <= PlayerManager.x2) collision.SendMessage("Damage", damage * 5);
				else collision.SendMessage("Damage", damage);
			}
			else if (PlayerManager.use && PlayerManager.PassiveSkill == 3)
			{
				int rand = Random.Range(1, 101);
				if (rand <= PlayerManager.x3)
				{
					if (Player.startheart > Player.heart + damage) Player.heart = Player.heart + damage;
					else if (Player.startheart <= Player.heart + damage) Player.heart = Player.startheart;
				}
				collision.SendMessage("Damage", damage);
			}
			else collision.SendMessage("Damage", damage);
			return ;
		}
		switch (collision.tag)
		{
			case "enemy":
				if (PlayerManager.use && PlayerManager.PassiveSkill == 5)
				{
					if (ttmp % PlayerManager.x5 == 0)
					{
						int rand = Random.Range(0, 2);
						if (rand == 0) collision.SendMessage("Damage", damage * 5);
						else
						{
							if (Player.startheart > Player.heart + damage) Player.heart = Player.heart + damage;
							else if (Player.startheart <= Player.heart + damage) Player.heart = Player.startheart;
						}
					}
					else collision.SendMessage("Damage", damage);
				}
				else if (PlayerManager.use && PlayerManager.PassiveSkill == 2)
				{
					int rand = Random.Range(1, 101);
					if (rand <= PlayerManager.x2) collision.SendMessage("Damage", damage * 5);
					else collision.SendMessage("Damage", damage);
				}
				else if (PlayerManager.use && PlayerManager.PassiveSkill == 3)
				{
					int rand = Random.Range(1, 101);
					if (rand <= PlayerManager.x3)
					{
						if (Player.startheart > Player.heart + damage) Player.heart = Player.heart + damage;
						else if (Player.startheart <= Player.heart + damage) Player.heart = Player.startheart;
					}
					collision.SendMessage("Damage", damage);
				}
				else collision.SendMessage("Damage", damage);
				break;
			
			default:
				break;
		}
	}
}

