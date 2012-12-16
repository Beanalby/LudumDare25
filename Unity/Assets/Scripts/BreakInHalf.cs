using UnityEngine;
using System.Collections;

public class BreakInHalf : MonoBehaviour {

    public GameObject _base;
    public GameObject top;
    public GameObject full;

    private float destroyTopAfter = 3;
    private float breakSpeed = 10;
    private float breakSpin = 10;
    Collider fullCollider;

    public void Start()
    {
        fullCollider = GetComponentInChildren<Collider>();
    }

    public void Swiped(GameObject killer)
    {
        breakApart(killer, breakSpeed);
    }
    public void Pounced(GameObject killer)
    {
        breakApart(killer, 0);
    }

    private void breakApart(GameObject killer, float horizontal)
    {
        Transform target = full.transform;
        // make sure the new parts don't collide with what already exists
        fullCollider.enabled = false;
        Destroy(full);

        // bottom-half stays where the obj is
        Instantiate(_base,
            target.position, target.rotation);

        // top half goes flying off
        GameObject newTop = (GameObject)Instantiate(top,
            full.transform.position, Quaternion.identity);
        // oooo bad dependency!  don't care!
        PersonController.Fling(newTop, killer,
            horizontal, breakSpeed, breakSpin);
        
        // Destroy the top after it's flung around for a while
        Destroy(newTop, destroyTopAfter);
    }
}

