using UnityEngine;
using System.Collections;

public class TutorialSend : MonoBehaviour {
    public void OnTriggerEnter(Collider col)
    {
        GameObject.Find("TutorialDriver").SendMessage("OnChange", name);
    }
}
