using UnityEngine;
using System.Collections;

public class MuffinController : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private float speed = 10;


    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float factor = speed * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector3(factor * Input.GetAxis("Horizontal"), 0, 0));
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.normal == Vector3.up)
            return;
        Debug.Log("OnControllerColliderHit with " + hit.gameObject.name);
    }
    void OnCollisionEnter(Collision info)
    {
        Debug.Log("OnCollisionEnter with " + info.gameObject.name);
    }
}
