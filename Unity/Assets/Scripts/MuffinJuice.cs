using UnityEngine;
using System.Collections;

public class MuffinJuice : MonoBehaviour {

    public bool doJuice = true;

    public GameObject swipeEffect;
    public GameObject pounceEffect;

    public void DidSwipe()
    {
        if (!doJuice)
            return;
        GameObject swipe = (GameObject)GameObject.Instantiate(swipeEffect);
        /* the location of the swipe prefab has the proper offsets from
         * our location. */
        swipe.transform.position += transform.position;
        swipe.transform.parent = transform;
    }
    public void DidPounce()
    {
        if (!doJuice)
            return;
        GameObject effect = (GameObject)GameObject.Instantiate(pounceEffect);
        /* the location of the effect prefab has the proper offsets from
         * our location. */
        effect.transform.position += transform.position;
        /* don't assign the parent; we want the pounce effects to stay
         * where they started */
    }
}
