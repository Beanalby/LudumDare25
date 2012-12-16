using UnityEngine;
using System.Collections;

public class MuffinSounds : MonoBehaviour {

    public bool doJuice = true;

    public AudioClip jumpSound;
    public AudioClip pounceSound;
    public AudioClip swipeSound;

    void doPlay(AudioClip clip)
    {
        if (doJuice)
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    public void DidSwipe()
    {
        doPlay(swipeSound);
    }
    public void DidJump()
    {
        doPlay(jumpSound);
    }
    public void DidPounce()
    {
        doPlay(pounceSound);
    }
}
