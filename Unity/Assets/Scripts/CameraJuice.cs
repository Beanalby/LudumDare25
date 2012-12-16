using UnityEngine;
using System.Collections;

public class CameraJuice : MonoBehaviour {

    public bool doJuice = true;
    public Vector3 camOffset = new Vector3(6, 5, -9);

    private Camera cam;
    private Vector3 camBase;

    private float cameraSnap = .2f;

    private float swipeStart;
    private float swipeDuration = .05f;
    private float swipeDist = .1f;

    private float pounceStart;
    private float pounceDuration = .25f;
    private float pounceDist = .5f;

	void Start() {
        cam = Camera.mainCamera;
        camBase = cam.transform.position;
        swipeStart = -1;
        pounceStart = -1;
	}

    void FixedUpdate()
    {
        // have the camera follow muffin horizontally
        Vector3 camDestination = camOffset + new Vector3(rigidbody.position.x, 0, 0);
        camBase = Vector3.Lerp(camBase, camDestination, cameraSnap);

        Vector3 newPos = camBase;

        if (swipeStart != -1)
        {
            float dur = Time.time - swipeStart;
            if (dur > (swipeDuration))
            {
                swipeStart = -1;
            }
            else
            {
                // make the camera dip by a full sin wave
                float percent = (dur / swipeDuration);
                float offset = swipeDist * makeWave(percent);
                newPos.y += offset;
            }
        }
        if (pounceStart != -1)
        {
            float dur = Time.time - pounceStart;
            if (dur > (pounceDuration))
            {
                pounceStart = -1;
            }
            else
            {
                // make the camera dip by a full sin wave
                float percent = (dur / pounceDuration);
                float offset = pounceDist * makeWave(percent);
                newPos.y += offset;
            }
        }

        cam.transform.position = newPos;
    }

    /* returns a sin wave from 0 to 1 to 0 with value between 0-2
     * for the domain 0-1 */
    private float makeWave(float percent)
    {
        return 1 + Mathf.Sin((percent * 2 * Mathf.PI) - (Mathf.PI / 2));
    }

    public void DidSwipe()
    {
        if(doJuice)
            swipeStart = Time.time;
    }

    public void DidPounce()
    {
        if(doJuice)
            pounceStart = Time.time;
    }
}
