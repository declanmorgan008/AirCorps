using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private float moveSpeed = 2.3f;
    private float accel = 5f;
    private float dCoef = 4f;
    private float maxAngle = 5f;
    private float dMax = 0.1f;
    private float rotationSpeed = 3f;
    private float angle;
    private float targetAngle;
    private float weight;
    private float deltaAngle = 0;
    private float boostingTrigger = 0;
    private bool boostingBumber = false;
    float gravity = 0.3f;

    private Vector2 inputDirection = Vector2.zero;
    private Vector2 inputDirectionLast = Vector2.zero;

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

        angle = transform.eulerAngles.z;
        targetAngle = Vector2.Angle(Vector2.right, inputDirection);

        if (inputDirection.y < 0)
        {
            targetAngle = 360 - targetAngle;
        }

        if (targetAngle > 90 && targetAngle < 270 && (angle <= 90 || angle >= 270))
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        } else if ((targetAngle <= 90 || targetAngle >= 270) && angle > 90 && angle < 270)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }

        if (boostingTrigger == 0 && !boostingBumber)
        {
            deltaAngle = Mathf.Min(Mathf.Abs(targetAngle - angle), 360 - Mathf.Abs(targetAngle - angle));

            
            deltaAngle = Mathf.Min(deltaAngle, maxAngle);

            
            if(angle > targetAngle && !(angle >= 270 && targetAngle <= 90) 
                && !(angle > 180 && targetAngle < 90 && angle - targetAngle > 180)
                || (targetAngle >= 270 && angle < 90))
            {
                deltaAngle = -deltaAngle;
            }

            //deltaAngle = Mathf.Sign(Vector2.SignedAngle(rb2d.velocity.normalized, inputDirection.normalized))*deltaAngle;

            Debug.Log("Angle: " + angle + ", Target: " + targetAngle + ", Delta: " + deltaAngle);

            Rotate(transform, deltaAngle);
            angle = transform.eulerAngles.z;

            velocity.x = Mathf.Cos(Mathf.Deg2Rad * angle) * accel;
            velocity.y = Mathf.Sin(Mathf.Deg2Rad * angle) * accel;

            ThrustForward(velocity);
            ApplyDrag();
            ClampVelocity();
        }
        else
        {
            Rotate(transform, targetAngle - angle);
            angle = transform.eulerAngles.z;
        }
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

        mag = inputDirection.magnitude;
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
}
