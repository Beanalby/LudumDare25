using UnityEngine;
using System.Collections;

public class MuffinController : MonoBehaviour {

    private Vector3 gravity = Physics.gravity * 2;

    private float jumpSpeed = 12;
    private float moveSpeed = 5;
    private float pounceThreshold = .66f;
    private float pounceSpeed = 20f;
    private float pounceEffectRadius = 6f;
    private float pounceEffectHeight = 2f;
    private float swipeCooldown = 1f;

    private Vector3 camOffset = new Vector3(6, 5, -9);
    private float cameraSnap = .2f;
    private float distToGround = 3f;

    private bool doJump;
    private bool isPouncing = false;
    private Vector3 velocity = Vector3.zero;
    private float lastSwipe = -500;

    private Rigidbody rb;
    private Camera cam;
    private Transform swipeSphere;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.mainCamera;
        swipeSphere = transform.FindChild("SwipeSphere");
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            tryJumpOrPounce();
        if (Input.GetButtonDown("Fire1"))
            trySwipe();
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        // figure out where our new position will be
        Vector3 newPos;
        velocity.x = moveSpeed * Input.GetAxis("Horizontal");
        if (doJump)
        {
            SendMessage("DidJump");
            velocity.y = jumpSpeed;
            doJump = false;
        }
        newPos = rb.position + (velocity * Time.deltaTime);

        // increase downward velocity by gravity
        if (!IsGrounded())
           velocity += Time.deltaTime * gravity;
        // don't go down through the ground
        if (newPos.y - distToGround < 0)
        {
            SendMessage("DidLand");
            newPos.y = distToGround;
            velocity.y = 0;
            if (isPouncing)
            {
                pounceLand();
                isPouncing = false;
            }
        }
        rb.MovePosition(newPos);

        // have the camera follow muffin horizontally
        Vector3 camDestination = camOffset + new Vector3(rb.position.x, 0, 0);
        cam.transform.position = Vector3.Lerp(cam.transform.position, camDestination, cameraSnap);
	}

    void tryJumpOrPounce()
    {
        if (IsGrounded())
        {
            Debug.Log("Setting doJump");
            doJump = true;
        }
        else
        {
            if (!isPouncing && rb.velocity.y < jumpSpeed * pounceThreshold)
            {
                Debug.Log("Pouncing!");
                isPouncing = true;
                // we want to travel downward at AT LEAST -pounceSpeed.
                // If we're already falling, increase by by that much.
                if(velocity.y > 0)
                    velocity.y = 0;
                velocity.y -= pounceSpeed;
            }
            else
            {
                Debug.Log("y too high (" + rb.velocity.y + ")");
            }
        }
    }
    void trySwipe()
    {
        if (Time.time < lastSwipe + swipeCooldown)
            return;
        // find out everything within swipeSphere.
        Collider[] objs = Physics.OverlapSphere(swipeSphere.position, swipeSphere.localScale.x / 2f);
        foreach (Collider obj in objs)
        {
            obj.SendMessageUpwards("Swiped", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    void pounceLand()
    {
        /* find everything within the sphere of pounceEffect,
         * but limit the height to give us the "cylinder" effect */
        Vector3 pouncePos = transform.position;
        pouncePos.y = 0;
        Debug.Log("Pounce exploding from " + pouncePos);
        Collider[] objs = Physics.OverlapSphere(pouncePos, pounceEffectRadius);
        foreach (Collider obj in objs)
        {
            if (obj.transform.position.y < pounceEffectHeight)
                obj.SendMessageUpwards("Pounced", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -Vector3.up, distToGround + .1f);
    }
}