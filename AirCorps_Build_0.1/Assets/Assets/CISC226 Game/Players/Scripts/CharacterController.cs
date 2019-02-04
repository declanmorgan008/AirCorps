using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private float moveSpeed = 16f;
    private float accel = 32f;
    private float dCoef = 4.2f;
    private float maxAngle = 6f;
    private float dMax = 0.1f;
    private float rotationSpeed = 3f;
    private float angle;
    private float targetAngle;
    private float weight;
    private float deltaAngle = 0;

    private float boostingTrigger = 0;
    private bool boostingBumber = false;
    private bool respawnButton = false;

    private Vector2 gravity = new Vector2(0f, -20f);
    private Vector2 initPos;

    private enum states {flying, gliding, dead };
    private int state = 0;

    private int mHP = 3;
    private int cHP = 3;

    private Vector2 inputDirection = Vector2.zero;
    private Vector2 inputDirectionLast = Vector2.zero;

    private Vector2 velocity;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public GameObject arrow;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        velocity.x = moveSpeed;
        velocity.y = 0;
        initPos = new Vector2(rb2d.transform.position.x, rb2d.transform.position.y);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        inputDirectionLast = inputDirection;
        inputDirection = Vector2.zero;
        inputDirection.x = Input.GetAxis("Horizontal");
        inputDirection.y = Input.GetAxis("Vertical");
        if (inputDirection == Vector2.zero)
        {
            inputDirection = inputDirectionLast;
        }

        boostingTrigger = Input.GetAxis("BoosterTrigger");
        boostingBumber = Input.GetButton("BoosterBumper");

        targetAngle = Vector2.Angle(Vector2.right, inputDirection);

        if (inputDirection.y < 0)
        {
            targetAngle = 360 - targetAngle;
        }

        switch (state)
        {
            case (int)states.flying:

                if(boostingBumber || boostingTrigger != 0)
                {
                    state = (int)states.gliding;
                    goto case 1;
                }

                angle = transform.eulerAngles.z;

                deltaAngle = Mathf.Min(Mathf.Min(Mathf.Abs(targetAngle - angle), 360 - Mathf.Abs(targetAngle - angle)), maxAngle);

                if (angle > targetAngle && !(angle >= 270 && targetAngle <= 90)
                    && !(angle > 180 && targetAngle < 90 && angle - targetAngle > 180)
                    || (targetAngle >= 270 && angle < 90))
                {
                    deltaAngle = -deltaAngle;
                }

                Rotate(transform, deltaAngle);
                angle = transform.eulerAngles.z;

                velocity.x = Mathf.Cos(Mathf.Deg2Rad * angle) * accel;
                velocity.y = Mathf.Sin(Mathf.Deg2Rad * angle) * accel;

                ThrustForward(velocity);
                ApplyDrag();
                ClampVelocity();

                break;

            case (int)states.gliding:

                if (!boostingBumber && boostingTrigger == 0)
                {
                    state = (int)states.flying;
                    goto case 0;
                }

                angle = transform.eulerAngles.z;

                Rotate(transform, targetAngle - angle);
                angle = transform.eulerAngles.z;

                break;

            case (int)states.dead:

                ThrustForward(gravity);

                respawnButton = Input.GetButton("RespawnButton");

                if ((boostingBumber || boostingTrigger != 0) && respawnButton)
                {
                    Respawn();
                }

                break;

            default:

                break;

        }

        arrow.transform.Rotate(0, 0, targetAngle - arrow.transform.eulerAngles.z);
        arrow.transform.position = transform.position;
    }

    void Movement(Vector2 move)
    {
        rb2d.position = rb2d.position + move;
    }

    void ClampVelocity()
    {
        float x = rb2d.velocity.x;
        float y = rb2d.velocity.y;
        float mag = rb2d.velocity.magnitude;

        if(mag > moveSpeed)
        {
            x = (x / mag) * moveSpeed;
            y = (y / mag) * moveSpeed;
        }

        rb2d.velocity = new Vector2(x, y);
    }

    void ThrustForward(Vector2 force)
    {
        rb2d.AddForce(force);
    }

    void ApplyDrag()
    {
        float x = rb2d.velocity.x;
        float y = rb2d.velocity.y;
        float targetX = velocity.x;
        float targetY = velocity.y;
        float mag = rb2d.velocity.magnitude;
        float speed = mag;
        Vector2 delta = Vector2.zero;

        mag = velocity.magnitude;
        targetX = (targetX / mag) * speed;
        targetY = (targetY / mag) * speed;

        if(Mathf.Abs(targetX) < Mathf.Abs(x))
        {
            delta.x = (targetX - x) * dCoef;
        }

        if (Mathf.Abs(targetY) < Mathf.Abs(y))
        {
            delta.y = (targetY - y) * dCoef;
        }

        ThrustForward(delta);
    }

    void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(state != (int)states.dead)
        {
            //rb2d.AddTorque(6f);
            state = (int)states.dead;
        }
        
    }

    void Respawn()
    {
        rb2d.transform.position = initPos;
        transform.rotation = Quaternion.identity;
        state = (int)states.gliding;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        inputDirectionLast = Vector2.zero;
        inputDirection = Vector2.zero;
    }
}
