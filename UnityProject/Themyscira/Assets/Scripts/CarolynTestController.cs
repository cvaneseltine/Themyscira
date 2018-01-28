using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarolynTestController : MonoBehaviour {

	bool nowDoTheThing = true;
	
	// Update is called once per frame
	void Update () {
		if (nowDoTheThing) {
			//StorylineManager.Instance.AdvanceStory();
			nowDoTheThing = false;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			StorylineManager.Instance.AdvanceStory();
			//PuzzleController.Instance.TranslateSquares();
		}		
	}
}
