using UnityEngine;
using System.Collections;

public class PounceEffect : MonoBehaviour {

    private float expandRate = 1f;
    private float duration = .5f;

    private float startTime;
    private Material matFlat, matAngled;
    private Color startColor, endColor;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        matAngled = transform.Find("PounceEffectAngled").GetComponent<MeshRenderer>().material;
        matFlat = transform.Find("PounceEffectFlat").GetComponent<MeshRenderer>().material;
        startColor = matFlat.color;
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
        matFlat.color = Color.Lerp(startColor, endColor, percent);
        matAngled.color = Color.Lerp(startColor, endColor, percent);

        float amount = 1 + (expandRate * Time.deltaTime);
        transform.localScale *= amount;
	}
}
