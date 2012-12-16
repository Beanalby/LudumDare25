using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour {

    private float moveSpeed = 1;
    private float zScale = .25f;
    private float bounceSpeed = 1f;
    private int moveFor = 2;
    private int moveSleep = 1;

    private float swipedAmount = 2;
    private float spinAmount = 5;

    private bool doMovement = true;
    private Vector3 movement = Vector3.zero;
    private float startMovingAgain = 0;
    private float stopMoving = 0;
    private float distToGround = .5f;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        startMovingAgain = (Random.value -.5f) * moveSleep;
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
            stopMoving = Time.time + moveFor + (Random.value - .5f) * moveFor;
            startMovingAgain = stopMoving + moveSleep + (Random.value - .5f) * moveSleep;
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
        // we're no longer moving under our own power
        doMovement = false;
        PersonController.Fling(gameObject, killer,
            swipedAmount, swipedAmount, spinAmount);
    }

    public void Pounced(GameObject killer)
    {
        // we're no longer moving under our own power
        doMovement = false;
        PersonController.Fling(gameObject, killer,
            0, swipedAmount, spinAmount);
    }

    static public void Fling(GameObject obj, GameObject killer,
        float horizontal, float vertical, float spinAmount)
    {
        Rigidbody rb = obj.rigidbody;
        // remove the rotational constraints (if any), they're flyin!
        rb.constraints = RigidbodyConstraints.None;
        float x = 0, y = 0;

        if (horizontal != 0)
        {
            // We want to fly right at least as fast as muffin.
            // Set our x to muffin's as a baseline if we're less than it.
            float killerX = killer.rigidbody.velocity.x;
            if (killerX > 0 && killerX > rb.velocity.x)
                x = killerX - rb.velocity.x;
            // launch us away, kinda randomly.  
            x += horizontal / 2 + Random.value * horizontal;
        }

        if (vertical != 0)
        {
            /* we definitely want to be moving up by amount, but if
             * we're already going up, increase our speed by that much */
            if (rb.velocity.y < 0)
                y = -rb.velocity.y;
            y += vertical / 2 + Random.value * vertical;
        }
        rb.velocity += new Vector3(x, y, rb.velocity.z);

        // give them a little random spin too
        rb.angularVelocity = new Vector3((Random.value - .5f) * spinAmount,
            (Random.value - .5f) * spinAmount,
            (Random.value - .5f) * spinAmount);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -Vector3.up, distToGround + .1f);
    }
}