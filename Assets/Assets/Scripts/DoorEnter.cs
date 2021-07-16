using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Transform backDoor;
    // Start is called before the first frame update
    private bool is_door;
    public Transform PlayerTransform;
    public float smoothing;
    void Start()
    {
        
    }
    private bool is_entering = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(gameObject.name);
            if (is_door && is_entering == false)
            {
                is_entering = true;
                Invoke("empt", 0.3f);
                PlayerTransform.position = Vector3.Lerp(PlayerTransform.position, backDoor.position, smoothing);
            }   
        }
    }
    void empt() 
    {
        is_entering=false;
    }
    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if(Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString()=="UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("indoor");
            is_door = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            Debug.Log("outdoor");
            is_door = false;
        }
    }
}
