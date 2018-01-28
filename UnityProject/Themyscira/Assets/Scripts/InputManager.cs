using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum Mode {
		Delay,
		Menu,
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
        mode = Mode.Navigation;
	}

	void Update () {
		if (mode == Mode.Delay) {
			//Reject player input for the moment (it won't last)
		}
		else if (mode == Mode.Menu) {
			//Use buttons and stuff
		}
		else if (mode == Mode.Navigation) {
			// processing done in PlayerMovement::Update
		}
		else if (mode == Mode.Puzzle) {
			//Do puzzle mechanics
			PuzzleController.Instance.ProcessInput();
		}
	}
}