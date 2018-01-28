using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum Mode {
		Navigation,
		Puzzle
	}

	public Mode mode;

	static InputManager singleton;

	static public InputManager Instance {
		get {
			return singleton;
		}
	}

	void Start() {
		if (singleton == null) {
			singleton = this;
		}
	}

	void Update () {
		if (mode == Mode.Navigation) {
			// processing done in PlayerMovement::Update
			}
		else if (mode == Mode.Puzzle) {
			//Do puzzle mechanics
			PuzzleController.Instance.ProcessInput();
		}
	}
}