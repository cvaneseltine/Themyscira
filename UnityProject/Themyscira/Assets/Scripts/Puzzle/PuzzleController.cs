using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour {

	Solution solution;
	List<PuzzleSquare> squares = new List<PuzzleSquare>();

	public Puzzle puzzle;

	public RectTransform puzzlePanel;
	public RectTransform attemptPanel;
	public RectTransform successPanel;

	public Transform squarePrefab;

	static PuzzleController singleton;

	bool puzzleComplete = false;

	static public PuzzleController Instance {
		get {
			return singleton;
		}
	}

	void Start () {
		gameObject.SetActive(false);
		if (singleton == null) {
			singleton = this;
		}
	}

	public void SetUpPuzzle (Puzzle nextPuzzle) {
		puzzle = nextPuzzle;
		solution = puzzle.solution;

		for (int i = 0; i < solution.wordStrings.Length; i++) {
			PuzzleSquare square = Instantiate(squarePrefab).GetComponent<PuzzleSquare>();
			squares.Add(square);
			square.translation = solution.translationStrings[i];
			square.GetComponentInChildren<Text>().text = solution.wordStrings[i];
			square.name = solution.wordStrings[i] + " square";
		}

		squares = Randomizer.RandomizeList(squares);

		foreach (PuzzleSquare square in squares) {
			square.transform.SetParent(puzzlePanel);
		}

		puzzlePanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();

		//for (int i = 0; i < words.Count; i++) {
		//	PuzzleSquare square = Instantiate(squarePrefab).GetComponent<PuzzleSquare>();
		//	square.GetComponentInChildren<Text>().text = words[i];
		//	square.name = words[i] + " square";
		//	Debug.Log(square.name + " translates to " + square.translation + ".");
		//	square.transform.SetParent(puzzlePanel);
		//	squares.Add(square);
		//	puzzlePanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();
		//}

		InputManager.Instance.mode = InputManager.Mode.Puzzle;
		gameObject.SetActive(true);
	}

	public void TestPuzzleSolution() {
		if (puzzleComplete) { //Already finished the puzzle, no testing it again.
			return;
		}
		foreach (PuzzleSquare square in squares) {
			if (square.isGoingHome) {
				return; //A square's still traveling, not ready yet
			} 
		}
		
		if (attemptPanel.GetComponent<PuzzlePanel>().TestPuzzleSolution(solution)) {
			CelebrateSuccess();
		}
	}

	public void TranslateSquares () {
		Debug.Log("I have " + squares.Count + " squares to translate.");
		foreach (PuzzleSquare square in squares) {
			square.GetComponentInChildren<Text>().text = square.translation;
		}
		attemptPanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();
	}

	public void CelebrateSuccess() {
		//You did it!
		//Hooray, good job, make it splashy!
		puzzleComplete = true;
		successPanel.GetComponent<PuzzleSuccess>().Activate();
	}
}
