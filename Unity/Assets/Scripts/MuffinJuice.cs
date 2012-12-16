using UnityEngine;
using System.Collections;

public class MuffinJuice : MonoBehaviour {

    public GameObject swipeEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DidSwipe()
    {
        GameObject swipe = (GameObject)GameObject.Instantiate(swipeEffect);
        /* the location of the swipe prefab has the proper offsets from
         * our location. */
        swipe.transform.position += transform.position;
        swipe.transform.parent = transform;
    }
}
