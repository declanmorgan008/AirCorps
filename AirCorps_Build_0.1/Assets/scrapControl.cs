using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrapControl : MonoBehaviour
{
    
    private bool die = false;
    private int cLife = 0;
    private int mLife = 180;

    void Start()
    {
        cLife = mLife;
    }

    
    void FixedUpdate()
    {
        cLife--;

        if(cLife == 0)
        {
            Destroy(this.gameObject, 30f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "CollisionMask")
        {
            Destroy(this.gameObject, 30f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }

    }

    public void setDie(bool d)
    {
        die = d;
    }


}
