using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StorylineManager : MonoBehaviour {

	public Stage[] stages;
	int lastCompletedStage = -1;

	static StorylineManager singleton;

	static public StorylineManager Instance {
		get {
			return singleton;
			}
		}

	void Start() {
		if (singleton == null) {
			singleton = this;
		}
	}

	public void AdvanceStory () {
		lastCompletedStage++;
		Debug.Log("Advancing to stage " + lastCompletedStage + ".");
		PrepareStage();
	}

	void PrepareStage() {
		Stage stage = stages[lastCompletedStage];
		PuzzleController.Instance.SetUpPuzzle(stage.puzzle);
	}
}
