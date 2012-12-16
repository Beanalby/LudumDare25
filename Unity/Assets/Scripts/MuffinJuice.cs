using UnityEngine;
using System.Collections;

public class MuffinJuice : MonoBehaviour {

    public GameObject swipeEffect;
    public GameObject pounceEffect;

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
    public void DidPounce()
    {
        GameObject effect = (GameObject)GameObject.Instantiate(pounceEffect);
        /* the location of the effect prefab has the proper offsets from
         * our location. */
        effect.transform.position += transform.position;
        /* don't assign the parent; we want the pounce effects to stay
         * where they started */
    }
}
