using UnityEngine;
using System.Collections;
public class Creatures_Character_Controller : Enemy
{
	Animator anim;
	private SpriteRenderer rend;
	private bool faceright;
	private float maxspeed;
	private bool shield_mode=false;
	private bool twinkled=false;
	private Transform playerTransform;
	void Start () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		maxspeed = 3f;//Set walk speed
		faceright = true;
		anim = this.gameObject.GetComponent<Animator> ();
		rend = this.gameObject.GetComponent<SpriteRenderer> ();
		anim.SetBool("walk",false);
		anim.SetBool("jump",false);
		anim.SetBool("attack",false);
		anim.SetBool("shield_mode",false);
		anim.SetBool("dead",false);
	}
	public float radius;
	void OnCollisionEnter2D(Collision2D coll) 
	{
		anim.SetBool("jump",false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(this.heart <= 0 || transform.position.y < -80)
		{
			//anim.SetBool ("dead", true);
			Invoke("On_Destroy",2f);
		}
		if(anim.GetBool("dead")==false)
		{
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) anim.SetBool("jump",false);
			anim.SetBool("attack", false);
			if ((transform.position - playerTransform.position).sqrMagnitude <= 3) PlayAttack();
			else if (shield_mode == false)
			{
				if (Input.GetKeyDown(KeyCode.W)) PlayJump(); 
				PlayMove();
			}
		}
		else
		{
			if(twinkled==false)
			{
				twinkled=true;
				Invoke("Twinkle_",0.1f);
			}
		}
	}

	void PlayShieldMode(bool aux_)
	{
		shield_mode=aux_;
		anim.SetBool ("shield_mode", aux_);
		anim.SetBool ("walk", false);
	}

	void PlayAttack() { anim.SetBool ("attack", true); }

	void PlayJump()
	{
		if(anim.GetBool("jump")==false)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,300));
			anim.SetBool("jump",true);
		}
	}

	void PlayMove()
	{
		float move;
		float distance = (transform.position - playerTransform.position).sqrMagnitude;
		if (distance < radius && distance > 3)
		{
			if (transform.position.x > playerTransform.position.x) move = -1;
			else move = 1;
		}
		else move = 0;
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxspeed, GetComponent<Rigidbody2D>().velocity.y);
		if(move>0)
		{
			anim.SetBool ("walk", true);
			if(faceright==false) Flip ();
		}
		if(move==0) { anim.SetBool ("walk", false); }
		if((move<0))
		{
			anim.SetBool ("walk", true);
			if(faceright==true) Flip();
		}
	}
	void Flip()
	{
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void On_Destroy()
	{
		Destroy (this.gameObject);
	}
}
