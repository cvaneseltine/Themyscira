using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour {

	PuzzleController singleton;

	public Puzzle CurrentPuzzle;

	public PuzzleController Instance {
		get {
			if (singleton == null) {
				singleton = this;
			}
			return this;
		}
	}



}
