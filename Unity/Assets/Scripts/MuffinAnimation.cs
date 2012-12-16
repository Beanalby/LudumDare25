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

        anim["Swipe"].layer = 3;
        anim["Swipe"].wrapMode = WrapMode.Once;
        // swipe layer
	}
	
	// Update is called once per frame
	void Update () {
        if (rigidbody.velocity.x > 0)
        {
            anim["Walk"].speed = 1;
            anim.CrossFade("Walk");
        }
        else if (rigidbody.velocity.x < 0)
        {
            anim["Walk"].speed = -1;
            anim.CrossFade("Walk");
        }
        else
        {
            anim.CrossFade("Idle");
        }
	}

    public void DidJump()
    {
        anim.Play("Jumping");
    }
    public void DidLand()
    {
        anim.Play("JumpLand");
    }
    public void DidSwipe()
    {
        anim["Swipe"].time = 0;
        anim.Play("Swipe");
    }
}
