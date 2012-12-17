using UnityEngine;
using System.Collections;

public class OneShotMessage : MonoBehaviour {

    private bool didMessage = false;
    private float messageStart = -1;
    private float messageDuration = 3;
    public string message;

    public void OnTriggerEnter(Collider col)
    {
        if(didMessage)
            return;
        if (col.gameObject.name != "Player")
            return;
        didMessage = true;
        messageStart = Time.time;
    }

    public void OnGUI()
    {
        if (messageStart == -1)
            return;
        if (messageStart + messageDuration < Time.time)
        {
            messageStart = -1;
            return;
        }
        GUI.Box(new Rect(10, 10, Screen.width - 10, 75), message);
    }
}
