using UnityEngine;
using System.Collections;

public class MuffinAnimation : MonoBehaviour {

    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = transform.FindChild("MuffinModel").gameObject.animation;
        anim.wrapMode = WrapMode.Loop;
        anim["Jumping"].layer = 5;
        anim["Jumping"].wrapMode = WrapMode.ClampForever;

        anim["JumpLand"].layer = 5;
        anim["JumpLand"].wrapMode = WrapMode.Once;

        // swipe layer
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidbody.velocity.x > 0)
        {
            Debug.Log("CrossFade Walk");
            anim["Walk"].speed = 1;
            anim.CrossFade("Walk");
        }
        else if (rigidbody.velocity.x < 0)
        {
            Debug.Log("Crossfade WalkBackwards");
            anim["Walk"].speed = -1;
            anim.CrossFade("Walk");
        }
        else
        {
            Debug.Log("CrossFade Idle");
            anim.CrossFade("Idle");
        }
	}

    public void DidJump()
    {
        anim["Jumping"].time = 0;
        anim.CrossFade("Jumping");
    }
    public void DidLand()
    {
        anim.CrossFade("JumpLand");
    }
}