using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharge : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playSprite;
    private Color color;
    public float activetime;
    public float activestart;
    private float tmp;
    public float tmpst;
    public float chan;
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Boss").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playSprite = player.GetComponent<SpriteRenderer>();
        tmp = tmpst;
        thisSprite.sprite = playSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;
        activestart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        color = new Color((float) 37 / 255, (float) 183/255, 1, tmp);
        thisSprite.color = color; 
        tmp *= chan;
        if (Time.time >= activetime+activestart)
        {
            BossChargePool.instance.ReturnPool(this.gameObject);
        }
    }
}
