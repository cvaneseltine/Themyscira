using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleCanvas : UsefulCanvas {

	PuzzleSquare mouseFollower;

	static PuzzleCanvas singleton;

	static public PuzzleCanvas Instance {
		get {
			return singleton;
			}
		}

	void Start() {
		PrepRaycaster();
		if (singleton == null) {
			singleton = this;
		}
	}

	public void ProcessInput() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			mouseFollower = GetObjectUnderMouse<PuzzleSquare>();
			if (mouseFollower != null) {
				mouseFollower.StartFollowingMouse();
			}
		}

		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			if (mouseFollower != null) {
				PuzzlePanel newPanel = GetObjectUnderMouse<PuzzlePanel>();
				PuzzlePanel oldPanel = mouseFollower.transform.parent.GetComponent<PuzzlePanel>();

				if (newPanel != null) {
					mouseFollower.transform.SetParent(newPanel.transform);
					newPanel.UpdateSquareHomes();
				}
				oldPanel.UpdateSquareHomes();
			}
		}
	}
}