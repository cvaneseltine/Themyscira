﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

	Solution solution;
	List<PuzzleSquare> squares = new List<PuzzleSquare>();

	public Puzzle puzzle;

	public RectTransform puzzlePanel;
	public RectTransform attemptPanel;

	public Transform squarePrefab;

	static PuzzleController singleton;

	static public PuzzleController Instance {
		get {
			return singleton;
		}
	}

	void Start () {
		if (singleton == null) {
			singleton = this;
		}
	}

	public void SetUpPuzzle () {
		solution = puzzle.solution;
		List<string> words = puzzle.ScrambleWords();

		for (int i = 0; i < words.Count; i++) {
			PuzzleSquare square = Instantiate(squarePrefab).GetComponent<PuzzleSquare>();
			square.GetComponentInChildren<Text>().text = words[i];
			square.name = words[i] + " square";
			square.transform.SetParent(puzzlePanel);
			puzzlePanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();
		}
	}

	public void TestPuzzleSolution() {
		foreach (PuzzleSquare square in squares) {
			if (square.isGoingHome) {
				return; //Someone's still traveling, not ready yet
			} 
		}

		attemptPanel.GetComponent<PuzzlePanel>().TestPuzzleSolution(solution);
	}
}
