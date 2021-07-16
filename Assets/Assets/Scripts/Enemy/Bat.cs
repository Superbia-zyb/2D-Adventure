using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bat : Enemy 
{
	public float speed;
	public float basetim;
	public float waitim;
	public Transform movePos;
	public Transform leftmovePos;
	public Transform rightmovePos;
	public float radius;
	private Transform playerTransform;
	public void Start()
	{
		base.Start();
		movePos.position = randomPos();
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
// Use this for initialization
// Update is called once per frame

	public void Update () 
	{
		if (this.heart <= 0) Destroy(gameObject);
		if (playerTransform != null)
		{
			Scene scene = SceneManager.GetActiveScene();
			float distance = (transform.position - playerTransform.position).sqrMagnitude;
			if (distance < radius && scene.name != "1-获得冲锋" )
			{
				transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
				if (Vector2.Distance(transform.position, movePos.position) < 0.2f)
				{
					if (waitim <= 0)
					{
						movePos.position = randomPos();
						waitim = basetim;
					}
					else waitim -= Time.deltaTime;
				}
			}
		}
	}
	public Vector2 randomPos()
	{
		Vector2 rndPos = new Vector2(Random.Range(leftmovePos.position.x, rightmovePos.position.x), Random.Range(leftmovePos.position.y, rightmovePos.position.y));
		return rndPos;
	}
}
