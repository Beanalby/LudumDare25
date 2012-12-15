using UnityEngine;
using System.Collections;

public enum ThingType { Person, House }

public class DestroyableThing : MonoBehaviour {

    public ThingType type;
    float swipedAmount = 5;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Swiped(Transform SwipeSphere)
    {
        switch (type)
        {
            case ThingType.Person:
                Debug.Log("Person got swiped!");
                // launch us away, kinda randomly
                rb.velocity = new Vector3(swipedAmount, swipedAmount, 0);
                break;
        }
    }
}
