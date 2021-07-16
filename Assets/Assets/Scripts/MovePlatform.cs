using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform leftmovePos;
    public Transform rightmovePos;
    private bool is_L;
	void Start()
    {
		is_L = true;
	}

	// Update is called once per frame
	public void Update()
	{
		if (Vector2.Distance(transform.position, rightmovePos.position) < 0.2f) is_L = false;
		if (Vector2.Distance(transform.position, leftmovePos.position) < 0.2f) is_L = true;

		if (is_L) transform.position = Vector2.MoveTowards(transform.position, rightmovePos.position, 2 * Time.deltaTime); 
		if (!is_L) transform.position = Vector2.MoveTowards(transform.position, leftmovePos.position, 2 * Time.deltaTime);
	}
}
