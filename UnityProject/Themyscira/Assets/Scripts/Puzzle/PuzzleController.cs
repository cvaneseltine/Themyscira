using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PuzzleController : MonoBehaviour {

	Solution solution;
	List<PuzzleSquare> squares = new List<PuzzleSquare>();

	public Puzzle puzzle;

	public PuzzleCanvas puzzleCanvas;
	public RectTransform puzzlePanel;
	public RectTransform attemptPanel;
	public RectTransform successPanel;

	public Transform squarePrefab;

	public Sprite originalSprite;
	public Sprite correctPlacementSprite;

	static PuzzleController singleton;

	bool puzzleComplete = false;

	static public PuzzleController Instance {
		get {
			return singleton;
		}
	}

	void Start () {
		if (singleton == null) {
			singleton = this;
		}
		puzzleCanvas.gameObject.SetActive(false);
	}

	public void ProcessInput() {
		if (!puzzleCanvas.gameObject.activeSelf) {
			puzzleCanvas.gameObject.SetActive(true);
		}
		puzzleCanvas.ProcessInput();
	}

	public void SetUpPuzzle (Puzzle nextPuzzle) {
		puzzleCanvas.gameObject.SetActive(true);

		puzzle = nextPuzzle;
		solution = puzzle.solution;

		for (int i = 0; i < solution.wordStrings.Length; i++) {
			PuzzleSquare square = Instantiate(squarePrefab).GetComponent<PuzzleSquare>();
			squares.Add(square);
			square.translation = solution.translationStrings[i];
			if (i > 0) {
				Debug.Log("Square #" + i + " gets left square string for square #" + (i - 1) + "."); 
				square.leftSquareString = solution.wordStrings[i - 1];
			}
			if (i < solution.wordStrings.Length - 1) {
				Debug.Log("Square #" + i + " gets right square string for square #" + (i + 1) + ".");
				square.rightSquareString = solution.wordStrings[i + 1];
			}
			square.myString = solution.wordStrings[i];
			square.GetComponentInChildren<Text>().text = square.myString;
			square.name = solution.wordStrings[i] + " square";
		}

		squares = Randomizer.RandomizeList(squares);

		foreach (PuzzleSquare square in squares) {
			square.transform.SetParent(puzzlePanel);
		}

		puzzlePanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();

		InputManager.Instance.mode = InputManager.Mode.Puzzle;
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

	public void PuzzleSquareHintCheck(List<PuzzleSquare> squares) {
		Debug.Log("Starting puzzle square hint check.");
		
		if (squares.Count == 1) {
			return;
		}

		for (int i = 0; i < squares.Count; i++) {
			PuzzleSquare square = squares[i];
			bool isCorrectPosition = true;

			//Check the left and right neighbors.
			if (i == 0 && square.leftSquareString != null) { //Leftmost square needs null left
				isCorrectPosition = false;
				}
			else if (square.leftSquareString != squares[i - 1].myString) { //Doesn't match correct left neighbor
				isCorrectPosition = false;
				}

			if (i == squares.Count - 1 && square.rightSquareString != null) { //Rightmost square needs null right
				isCorrectPosition = false;
				}
			if (square.rightSquareString != squares[i + 1].myString) { //Doesn't match correct right neighbor
				isCorrectPosition = false;
				}

			if (isCorrectPosition) {
				if (square.GetComponent<Image>().sprite != correctPlacementSprite) {
					MorphingImageHorror.MorphImage(square.GetComponent<Image>(), correctPlacementSprite);
				}
			}
			else {
				if (square.GetComponent<Image>().sprite != originalSprite) {
					MorphingImageHorror.MorphImage(square.GetComponent<Image>(), originalSprite);
				}
			}
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
