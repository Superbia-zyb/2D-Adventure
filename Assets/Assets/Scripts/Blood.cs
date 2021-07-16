using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Invoke("Dest", 5f);	
	}
	void Dest()
	{
		Destroy(gameObject);
	}
	// Update is called once per frame
}
