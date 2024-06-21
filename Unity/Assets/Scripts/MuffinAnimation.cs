using UnityEngine;
using System.Collections;

public class MuffinAnimation : MonoBehaviour {

    private Animation anim;

	void Start () {
        anim = transform.Find("MuffinModel").gameObject.GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;
        anim["Jumping"].layer = 5;
        anim["Jumping"].wrapMode = WrapMode.ClampForever;

        anim["JumpLand"].layer = 5;
        anim["JumpLand"].wrapMode = WrapMode.Once;

        anim["Swipe"].layer = 3;
        anim["Swipe"].wrapMode = WrapMode.Once;

        anim.Play("Idle");
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Rigidbody>().velocity.x > 0)
        {
            anim["Walk"].speed = 1;
            anim.CrossFade("Walk");
        }
        else if (GetComponent<Rigidbody>().velocity.x < 0)
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
