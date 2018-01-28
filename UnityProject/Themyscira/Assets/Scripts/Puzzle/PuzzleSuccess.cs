using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSuccess : MonoBehaviour, IWannaKnowAboutLerping {
	RectTransform rectangle;

	bool decodingActive;
	Vector3 decodingStart;
	Vector3 decodingTurnaround;

	public RectTransform decodingBar;

	public void Activate () {
		gameObject.SetActive(true);
		decodingBar.gameObject.SetActive(true);
		rectangle = (RectTransform)transform;
		ActivateDecodingBar();
	}

	void ActivateDecodingBar () {
		decodingStart = decodingBar.position;
		float turnaroundX = decodingStart.x + rectangle.rect.width;
		decodingTurnaround = new Vector3(turnaroundX, decodingStart.y, decodingStart.z);
		LerpingHorror.StartLerping(decodingBar, decodingTurnaround, this);
		InputManager.Instance.mode = InputManager.Mode.Delay;
	}

	public void ReportLerpFinished (Lerper lerper) {
		if (Mathf.Approximately(lerper.transform.position.x, decodingTurnaround.x) && Mathf.Approximately(lerper.transform.position.y, decodingTurnaround.y)) {
			LerpingHorror.StartLerping(decodingBar, decodingStart, this);
		}
		else {
			lerper.gameObject.SetActive(false);
			gameObject.SetActive(false);
			PuzzleController.Instance.TranslateSquares();
			StorylineManager.Instance.RunStageReward();
		}
		InputManager.Instance.mode = InputManager.Mode.Menu;
	}
}
