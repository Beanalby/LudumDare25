using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour {

    private float swipedAmount = 5;
    private float moveSpeed = 1;
    private float zScale = .25f;
    private float bounceSpeed = 1f;
    private int moveFor = 2;
    private int moveSleep = 1;

    private bool doMovement = true;
    private Vector3 movement = Vector3.zero;
    private float startMovingAgain = 0;
    private float stopMoving = 0;
    private float distToGround = .5f;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        if (doMovement)
            move();
    }

    private void move()
    {
        if (startMovingAgain < Time.time)
        {
            float x, y, z;
            // move x in a random direction between .75speed & 1.25speed
            float randSpeed = moveSpeed + (moveSpeed * (Random.value - .5f) / 2f);
            if (Random.value > .5f)
                x = randSpeed;
            else
                x = -randSpeed;

            y=0;

            // make sure they stay on the platform - move them -z if they're +z,
            // & vice versa.
            z = randSpeed * zScale;
            if(transform.position.z > 0)
                z = -z;

            movement = new Vector3(x, y, z);
            transform.rotation = Quaternion.LookRotation(movement);
            stopMoving = Time.time + moveFor;
            startMovingAgain = Time.time + moveFor + moveSleep;
        }
        if (movement != Vector3.zero)
        {
            if (stopMoving < Time.time)
                movement = Vector3.zero;
            else
            {
                if (IsGrounded())
                    movement.y = bounceSpeed;
                else
                    movement.y = rb.velocity.y;
                rb.velocity = movement;
            }
        }
    }

    public void Swiped(GameObject killer)
    {
        Debug.Log("Person got swiped!");
        // we're no longer moving under our own power
        doMovement = false;
        // remove the rotational constraints, they're flyin!
        rb.constraints = RigidbodyConstraints.None;

        // We want to fly right at least as fast as muffin.
        // Set our x to muffin's as a baseline if we're less than it.
        float killerX = killer.rigidbody.velocity.x;
        if(killerX > 0 && killerX > rb.velocity.x)
            rb.velocity = new Vector3(killerX, rb.velocity.y, rb.velocity.z);

        // launch us away, kinda randomly.  
        float x = swipedAmount / 2 + Random.value * swipedAmount;
        rb.velocity += new Vector3(x, swipedAmount / 2 + Random.value * swipedAmount, 0);

    }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -Vector3.up, distToGround + .1f);
    }
}

