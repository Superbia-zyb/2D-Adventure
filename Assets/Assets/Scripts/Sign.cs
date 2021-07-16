using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inSign;
    private bool readSign;
    public GameObject tishi;
    public GameObject xinxi;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inSign == true && Input.GetKeyDown(KeyCode.M))
        {
            if (!readSign)
            {
                xinxi.SetActive(true); readSign = true;
            }
            else
            {
                xinxi.SetActive(false); readSign = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (inSign == false)
            {
                tishi.SetActive(true);
            }
            inSign = true;

        }
    }
    private void OnTriggerExit2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Player") && Coll.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            inSign = false;
            tishi.SetActive(false);
            xinxi.SetActive(false);
        }
    }
}
