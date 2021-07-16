using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBash : MonoBehaviour
{
	public int damage;
	private PolygonCollider2D coll;
	void Start()
	{
		coll = GetComponent<PolygonCollider2D>();
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (Player.is_died == true) return;
		switch (collision.tag)
		{
			case "enemy":
				collision.SendMessage("Damage", damage);
				break;
			case "Player":
				collision.SendMessage("PlayerDamage", damage);
				break;
			case "Boss":
				collision.SendMessage("Damage", damage);
				break;
			default:
				break;
		}
	}
}

