using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public float hoverPeriod = 1;
    public float hoverAmplitude = 0.5f;

    private float timer = 0;

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if (timer > hoverPeriod)
        {
            timer = 0;
        }

        float radians = 2 * Mathf.PI * timer / hoverPeriod;
        float hover = hoverAmplitude * Mathf.Sin(radians);
        Debug.Log(hover);
        gameObject.transform.localPosition = new Vector3(0, hover, 0);
	}
}
