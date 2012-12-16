using UnityEngine;
using System.Collections;

public class PounceEffect : MonoBehaviour {

    private float expandRate = 1f;
    private float duration = .5f;

    private float startTime;
    private Material matFlat, matAngled, matVertical;
    private Color startColor, endColor;
    private Transform transFlat, transAngled, transVertical;
	// Use this for initialization
	void Start () {
        startTime = Time.time;

        GameObject obj;
        obj = (GameObject)transform.Find("PounceEffectAngled").gameObject;
        matAngled = obj.GetComponent<MeshRenderer>().material;
        transAngled = obj.transform;

        obj = (GameObject)transform.Find("PounceEffectFlat").gameObject;
        matFlat = obj.GetComponent<MeshRenderer>().material;
        transFlat = obj.transform;

        obj = (GameObject)transform.Find("PounceEffectVertical").gameObject;
        matVertical = obj.GetComponent<MeshRenderer>().material;
        transVertical = obj.transform;

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
        Color newColor = Color.Lerp(startColor, endColor, percent);
        matFlat.color = newColor;
        matAngled.color = newColor;
        matVertical.color = newColor;

        float amount = 1 + (expandRate * Time.deltaTime);
        float halfAmount = 1 + (expandRate * Time.deltaTime / 2);
        Vector3 tmp;
        // vertical goes straight up, no x/z
        tmp = transVertical.localScale;
        transVertical.localScale = new Vector3(tmp.x, tmp.y, tmp.z * amount);
        // angled does x & z at reduced amounts
        transAngled.localScale *= halfAmount;

        // flat does full x & z (doesn't have any y anyway)
        transFlat.localScale *= amount;
	}
}
