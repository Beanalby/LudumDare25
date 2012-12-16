using UnityEngine;
using System.Collections;

public class SwipeEffect : MonoBehaviour {

    private float startTime;
    private float expandRate = .5f;
    private float duration = 1f;

    private Material mat;
    private Color startColor;
    private Color endColor;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        mat = GetComponent<MeshRenderer>().material;
        startColor = mat.color;
        endColor = startColor;
        endColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > duration)
        {
            Destroy(gameObject);
            return;
        }

        float percent = (Time.time - startTime) / duration;
        mat.color = Color.Lerp(startColor, endColor, percent);

        float amount = 1 + (expandRate * Time.deltaTime);
        transform.localScale *= amount;
	}
}
