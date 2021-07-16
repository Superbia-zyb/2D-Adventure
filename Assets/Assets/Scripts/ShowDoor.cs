using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDoor : MonoBehaviour
{
    private Object[] left;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        left = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log(left.Length);
        if (left.Length == 0) door.SetActive(true);
    }
}
