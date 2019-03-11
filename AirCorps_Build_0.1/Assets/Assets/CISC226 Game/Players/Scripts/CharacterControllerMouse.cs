using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerMouse : MonoBehaviour
{

    private float moveSpeed = 1f;
    private float angle = 0;
    private float angleRate = 0;
    private float restRate = -24f;
    private float turnRate = -20f;
    private float maxRate = 6f;
    private char dir = 'r';
    private bool alive = true;
    private bool hasTurn = false;

    private Vector2 velocity;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        velocity.x = moveSpeed;
        velocity.y = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction = new

        //angle = Vector2.Angle(rb2d.position, pz);
        //float deltaAngle = angle;

        //Debug.Log("Angle: " + (deltaAngle));

        /*
        if (Input.GetButton("Jump"))
        {

            hasTurn = false;

            if (dir == 'r')
            {
                if (Mathf.Sign(angleRate) == 1f) { angleRate = 0f; }
                angleRate += turnRate * Time.deltaTime;
                if (angleRate < -maxRate) { angleRate = -maxRate; }
            }
            else
            {
                if (Mathf.Sign(angleRate) == -1f) { angleRate = 0f; }
                angleRate -= turnRate * Time.deltaTime;
                if (angleRate > maxRate) { angleRate = maxRate; }
            }
        }
        else
        {

            if (dir == 'r' && !hasTurn)
            {
                if (angle <= 270 && angle > 90 && !hasTurn)
                {
                    if (angle <= 270 && angle >= 263) { deltaAngle = 270; hasTurn = true; angleRate = 0; }
                    dir = 'l';
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                else
                {
                    angleRate -= turnRate * Time.deltaTime;
                    if (angleRate > 4f) { angleRate = 4f; }
                }

            }
            else if (dir == 'l' && !hasTurn)
            {
                if ((angle > 270 || angle <= 90) && !hasTurn)
                {
                    if (angle > 270 && angle <= 277) { deltaAngle = 270; hasTurn = true; angleRate = 0; }
                    dir = 'r';
                    spriteRenderer.flipY = !spriteRenderer.flipY;
                }
                else
                {
                    angleRate += turnRate * Time.deltaTime;
                    if (angleRate < -4f) { angleRate = -4f; }
                }
            }
        }

        deltaAngle += angleRate;

        Debug.Log("Angle: " + (deltaAngle));

        transform.Rotate(Vector3.forward * (angle - deltaAngle));

        
        velocity.x = Mathf.Cos(Mathf.Deg2Rad * deltaAngle) * moveSpeed;
        velocity.y = Mathf.Sin(Mathf.Deg2Rad * deltaAngle) * moveSpeed;

        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = Vector2.right * deltaPosition.x;
        rb2d.position = rb2d.position + move;
        move = Vector2.up * deltaPosition.y;
        rb2d.position = rb2d.position + move;*/

        /* This is just a reference to how the movement works
        
        velocity.x = moveSpeed;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.right * deltaPosition.x;

        Movement(move);*/

    }

    void Movement(Vector2 move)
    {
        rb2d.position = rb2d.position + move;
    }
}

