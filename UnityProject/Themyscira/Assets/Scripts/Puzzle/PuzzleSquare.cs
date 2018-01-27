using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSquare : MonoBehaviour {

	public bool isFollowingMouse = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFollowingMouse) {
			//Debug.Log("I'm " + name + " and I wanna follow the mouse! We're going to " + Input.mousePosition + "!");
			transform.position = Input.mousePosition;
		}
	}
}
