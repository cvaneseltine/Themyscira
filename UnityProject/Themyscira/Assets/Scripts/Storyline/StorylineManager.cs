using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class StorylineManager : MonoBehaviour {

	public Stage[] stages;

	public Color originalStringColor;
	public Color translatedStringColor;

	public RectTransform rewardPanel;
	public Text rewardText;
	public Button continueButton;

	public RectTransform endGamePanel;

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
		rewardText.color = originalStringColor;
		rewardText.text = stage.originalString;
		rewardPanel.gameObject.SetActive(true);
	}

	public void RunStageReward() {
		Stage stage = stages[lastCompletedStage];

		rewardText.color = originalStringColor;
		rewardText.text = stage.translatedRewardString;
		continueButton.gameObject.SetActive(true);
	}

	public void HideStage() {
		Debug.Log("Hiding the stage.");
		rewardPanel.gameObject.SetActive(false);
		continueButton.gameObject.SetActive(false);
		PuzzleController.Instance.TakeDownPuzzle();
		InputManager.Instance.mode = InputManager.Mode.Navigation;
		if (lastCompletedStage == stages.Length - 1) {
			PuzzleCanvas.Instance.gameObject.SetActive(true);
			endGamePanel.gameObject.SetActive(true);
			InputManager.Instance.mode = InputManager.Mode.Menu;
		}
	}
}
