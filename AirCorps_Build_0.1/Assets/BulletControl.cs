using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private float bulletSpeed = 40f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(rb2d.velocity.x + transform.right.x * bulletSpeed, rb2d.velocity.y + transform.right.y * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CollisionMask")
        {
            Destroy(this.gameObject, 30f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
}
