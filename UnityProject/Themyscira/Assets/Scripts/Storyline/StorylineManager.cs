using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StorylineManager : MonoBehaviour {

	public Stage[] stages;
	int progress = 0;

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
		PrepareStage();
	}

	public void AdvanceStory () {
		Debug.Log("Advancing to stage " + progress + ".");
		progress++;
		PrepareStage();
	}

	public void PrepareStage() {
		Stage stage = stages[progress];
		PuzzleController.Instance.SetUpPuzzle(stage.puzzle);
	}

}
