using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public float hoverPeriod = 1;
    public float hoverAmplitude = 0.5f;

    private float timer = 0;
    private Vector3 startingLocalPos = Vector3.zero;

    private bool isActive = true;

    private void Start()
    {
        startingLocalPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isActive)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > hoverPeriod)
        {
            timer = 0;
        }

        float radians = 2 * Mathf.PI * timer / hoverPeriod;
        float hover = hoverAmplitude * Mathf.Sin(radians);
        //Debug.Log(hover);
        gameObject.transform.localPosition = new Vector3(startingLocalPos.x, startingLocalPos.y + hover, startingLocalPos.z);
	}

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
