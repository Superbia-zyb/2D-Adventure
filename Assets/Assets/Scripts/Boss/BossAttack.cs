using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossAttack : Boss 
{
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
		is_attack_skill = false;
		ttmp = 0;
		anim = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
		coll = GetComponent<PolygonCollider2D>();
	}
	Scene scene;
	// Update is called once per frame
	void Update()
	{
		scene = SceneManager.GetActiveScene();
		Attack();
	}
	void Attack()
	{
		if (Boss.is_died == true) return;
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		float distancex = (transform.position.x - playerTransform.position.x);
		float distancey = (transform.position.y - playerTransform.position.y);
		if (distancex < 0) distancex = -1 * distancex;
		if (distancey < 0) distancey = -1 * distancey;
		if (distancex < 3 && distancey < 1)
        {
			if (is_attack_skill == false)
			{
				is_attack_skill = true;
				Invoke("Set_attack_skill", 1f);
				int rand = Random.Range(0, 2);
				if (scene.name == "3-1 获得技能")
				{
					anim.SetTrigger("bash");
					return;
				}
				if (scene.name == "3-2 获得技能")
				{
					anim.SetTrigger("threeattack");
					return;
				}
				if (rand == 1) anim.SetTrigger("bash");
				if (rand == 0) anim.SetTrigger("threeattack");
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
		if (Boss.is_died == true) return;
		int rand;
		switch (collision.tag)
		{
			case "Player":
				if (Player.isDashing)
				{
					heart -= 2;
					return ;
				}
				if (ttmp % PlayerManager.x5 == 0)
				{
					rand = Random.Range(0, 2);
					if (rand == 0) collision.SendMessage("PlayerDamage", damage * 5);
					else
					{
						if (Boss.startheart > Boss.heart + damage) Boss.heart = Boss.heart + damage;
						else if (Boss.startheart <= Boss.heart + damage) Boss.heart = Boss.startheart;
					}
				}
				else
			 	{
			 		collision.SendMessage("PlayerDamage", damage);
					return ;
				}

				rand = Random.Range(1, 101);
				if (rand <= PlayerManager.x2) collision.SendMessage("PlayerDamage", damage * 5);
				else if (PlayerManager.x2 <= rand && rand <= PlayerManager.x3+PlayerManager.x2)
				{
					if (Boss.startheart > Boss.heart + damage) Boss.heart = Boss.heart + damage;
					else if (Boss.startheart <= Boss.heart + damage) Boss.heart = Boss.startheart;
				}
				collision.SendMessage("PlayerDamage", damage);

				break;
			default:
				break;
		}
	}
}

