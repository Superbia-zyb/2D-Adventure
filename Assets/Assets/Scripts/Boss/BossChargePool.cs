using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargePool : MonoBehaviour
{
    // Start is called before the first frame update
    public static BossChargePool instance;
    public GameObject shadowPrefab;
    public int shadowCount;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    void Awake()
    {
        instance = this;
        FillPool();
    }

    // Update is called once per frame
    public void FillPool()
    {
        for (int i = 0; i < shadowCount; i++)
        {
            var newShadow = Instantiate(shadowPrefab);
            newShadow.transform.SetParent(transform);
            ReturnPool(newShadow);
        }
    }
    public GameObject GetFormPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outShadow = availableObjects.Dequeue();
        outShadow.SetActive(true);
        return outShadow;
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
    }
}
