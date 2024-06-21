using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

    bool isEnded = false;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "Player")
            return;
        EndItAll(col.gameObject);
    }

    private void EndItAll(GameObject player)
    {
        MuffinController mc = player.GetComponent<MuffinController>();
        mc.canControl = false;
        isEnded = true;
    }

    public void OnGUI()
    {
        if (isEnded)
        {
            GUI.Box(new Rect(10, 10, Screen.width - 10, 75), "Congradulations, you've destroyed the city!\n\nWhy did Muffins want to destroy the city?\nMuffin is a cat.  It's what they do.");
            if (GUI.Button(new Rect(Screen.width / 2 - 80, 85, 160, 25), "Destroy again!"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
 
    }
}
