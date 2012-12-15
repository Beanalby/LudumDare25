using UnityEngine;
using System.Collections;

public class MuffinController : MonoBehaviour {

    private Vector3 gravity = Physics.gravity * 2;

    private float jumpSpeed = 12;
    private float moveSpeed = 10;
    private Vector3 camOffset = new Vector3(6, 5, -9);
    private float cameraSnap = .2f;
    private float distToGround = 3f;

    private Rigidbody rb;
    private Camera cam;
    private Transform swipeSphere;

    private bool doJump;
    private Vector3 velocity = Vector3.zero;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.mainCamera;
        swipeSphere = transform.FindChild("SwipeSphere");
	}

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Debug.Log("Setting doJump");
            doJump = true;
        }

        if (Input.GetButtonDown("Fire1"))
            doSwipe();
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        // figure out where our new position will be
        Vector3 newPos;
        velocity.x = moveSpeed * Input.GetAxis("Horizontal");
        if (doJump)
        {
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
            newPos.y = distToGround;
            velocity.y = 0;
        }

        rb.MovePosition(newPos);

        // have the camera follow muffin horizontally
        Vector3 camDestination = camOffset + new Vector3(rb.position.x, 0, 0);
        cam.transform.position = Vector3.Lerp(cam.transform.position, camDestination, cameraSnap);
	}

    void doSwipe()
    {
        // find out everything within swipeSphere.  Its collider isn't used,
        // just helps for visualization.
        Debug.Log("Swiping at " + swipeSphere.position + " by " + swipeSphere.localScale.x / 2f);
        Collider[] objs = Physics.OverlapSphere(swipeSphere.position, swipeSphere.localScale.x / 2f);
        foreach (Collider obj in objs)
            obj.SendMessage("Swiped", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -Vector3.up, distToGround + .1f);
    }
}