using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower : MonoBehaviour {

    public Hover hoverThing;
    public SpriteRenderer inactiveSprite;
    public SpriteRenderer activeSprite;

    private bool isActive = true;

	// Use this for initialization
	void Start () {
        SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActive(bool active)
    {
        isActive = active;

        // i think this works??
        hoverThing.SetActive(isActive);
        activeSprite.gameObject.SetActive(isActive);
        inactiveSprite.gameObject.SetActive(!isActive);
    }
}
