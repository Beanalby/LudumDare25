using UnityEngine;
using System.Collections;

public class MuffinController : MonoBehaviour {

    public bool canControl = true;

    private Vector3 gravity = Physics.gravity * 2;

    private float jumpSpeed = 12;
    private float moveSpeed = 5;
    private float pounceThreshold = .66f;
    private float pounceSpeed = 20f;
    private float pounceEffectRadius = 8f;
    private float pounceEffectHeight = 2f;
    private float swipeCooldown = 1f;
    public string welcomeMessage = "";
    private float welcomeMessageDuration = 3f;

    private float distToGround = 3f;

    private float playerStart;
    private bool doJump;
    private bool isPouncing = false;
    private Vector3 velocity = Vector3.zero;
    private float lastSwipe = -500;

    private Rigidbody rb;
    private Transform swipeSphere;

	void Start ()
    {
        playerStart = Time.time;
        rb = GetComponent<Rigidbody>();
        swipeSphere = transform.FindChild("SwipeSphere");
        // if we can't control at start, we're pouncing
        if (!canControl)
        {
            isPouncing = true;
        }
	}

    void Update()
    {
        if (!canControl)
            return;
        if (Input.GetButtonDown("Jump"))
            tryJumpOrPounce();
        if (Input.GetButtonDown("Fire1"))
            trySwipe();
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        // figure out where our new position will be
        Vector3 newPos = rb.position;
        velocity.x = 0;
        if (canControl)
        {
            if(newPos.x > 0 || Input.GetAxis("Horizontal") > 0)
                velocity.x = moveSpeed * Input.GetAxis("Horizontal");
            if (doJump)
            {
                SendMessage("DidJump");
                velocity.y = jumpSpeed;
                doJump = false;
            }
        }
        newPos += (velocity * Time.deltaTime);

        // increase downward velocity by gravity
        if (!IsGrounded())
           velocity += Time.deltaTime * gravity;
        // don't go down through the ground
        if (newPos.y - distToGround < 0)
        {
            SendMessage("DidLand");
            canControl = true;
            newPos.y = distToGround;
            velocity.y = 0;
            if (isPouncing)
            {
                pounceLand();
                isPouncing = false;
            }
        }
        rb.MovePosition(newPos);

	}

    void tryJumpOrPounce()
    {
        if (IsGrounded())
        {
            doJump = true;
        }
        else
        {
            if (!isPouncing && rb.velocity.y < jumpSpeed * pounceThreshold)
            {
                isPouncing = true;
                // we want to travel downward at AT LEAST -pounceSpeed.
                // If we're already falling, increase by by that much.
                if(velocity.y > 0)
                    velocity.y = 0;
                velocity.y -= pounceSpeed;
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
        lastSwipe = Time.time;
        SendMessage("DidSwipe");
    }

    void pounceLand()
    {
        /* find everything within the sphere of pounceEffect,
         * but limit the height to give us the "cylinder" effect */
        Vector3 pouncePos = transform.position;
        pouncePos.y = 0;
        Collider[] objs = Physics.OverlapSphere(pouncePos, pounceEffectRadius);
        foreach (Collider obj in objs)
        {
            if (obj.transform.position.y < pounceEffectHeight)
                obj.SendMessageUpwards("Pounced", gameObject, SendMessageOptions.DontRequireReceiver);
        }
        SendMessage("DidPounce");
    }

    bool IsGrounded()
    {
        return Physics.Raycast(rb.position, -Vector3.up, distToGround + .1f);
    }

    public void OnGUI()
    {
         if (playerStart + welcomeMessageDuration > Time.time && welcomeMessage != "")
        {
            GUI.Box(new Rect(10, 10, Screen.width - 10, 75), welcomeMessage);
        }
    }
}