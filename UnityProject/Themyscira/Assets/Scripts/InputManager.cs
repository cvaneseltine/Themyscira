using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum Mode {
		Navigation,
		Puzzle
	}

	public Mode mode;

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (mode == Mode.Navigation) {
				//Do navigation mechanics
				}
			else if (mode == Mode.Puzzle) {
				//Do puzzle mechanics

			}
		}
	}
}