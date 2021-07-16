using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Camerafollow_调试关卡 : MonoBehaviour {
	public Transform target;
	public float smoothing;
	// Use this for initialization
	public float lx, ly, rx, ry,z;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (target != null)
		{
			if (transform.position != target.position)
			{
				Vector3 targetpos = target.position;
				if (ly <= target.position.y && target.position.y <= ry) targetpos.y = ry;
				if (lx <= target.position.x && target.position.x <= rx) targetpos.x = rx;
				targetpos.z = z;
				Scene scene = SceneManager.GetActiveScene();
				if (scene.name == "2-获得被动") targetpos.y = targetpos.y - 10;
				else if (scene.name == "5-1 BOSS") targetpos.y = targetpos.y + 0.5f;
				transform.position = Vector3.Lerp(transform.position, targetpos, smoothing);
			}
		}
	}
}
