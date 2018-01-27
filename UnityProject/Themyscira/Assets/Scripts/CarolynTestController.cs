using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolynTestController : MonoBehaviour {

	bool nowDoTheThing = true;
	
	// Update is called once per frame
	void Update () {
		if (nowDoTheThing) {
			PuzzleController.Instance.SetUpPuzzle();
			nowDoTheThing = false;
		}		
	}
}
