using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour
{

    public GameObject target;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    private int dieTime = 300;
    private int count = 0;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(count == 0 && dead)
        {
            target.GetComponent<Animator>().enabled = true;
            target.GetComponent<SpriteRenderer>().sprite = aliveSprite;
            dead = false;
        } else if (dead)
        {
            count--;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "obj_Bullet(Clone)" && !dead)
        {
            target.GetComponent<Animator>().enabled = false;
            target.GetComponent<SpriteRenderer>().sprite = deadSprite;
            count = dieTime;
            dead = true;
        }

    }
}
