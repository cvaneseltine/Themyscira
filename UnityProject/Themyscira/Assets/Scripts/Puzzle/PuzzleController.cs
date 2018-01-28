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
		InputManager.Instance.mode = InputManager.Mode.Puzzle;

		puzzle = nextPuzzle;
		solution = puzzle.solution;
		puzzleComplete = false;

		squares.Clear();

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

		puzzleCanvas.gameObject.SetActive(true);

		foreach (PuzzleSquare square in squares) {
			square.transform.SetParent(puzzlePanel);
			square.transform.position = new Vector3(puzzlePanel.transform.position.x + Random.Range(0f, 1f), puzzlePanel.transform.position.y, puzzlePanel.transform.position.z);
			Debug.Log(square.name + " is now at " + square.transform.position + ".");
		}

		puzzlePanel.GetComponent<PuzzlePanel>().UpdateSquareHomes();
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

	public bool IsSquareHappyWithItsNeighbors (PuzzleSquare square) {
		bool isLeftCorrect = false;
		bool isRightCorrect = false;
		PuzzleSquare[] squares = attemptPanel.GetComponent<PuzzlePanel>().MySquaresInOrder;

		//if (squares.Length == 1) {
		//	Debug.Log(square.name + " has no neighbors and is therefore unhappy.");
		//	return false; //Can't be happy alone.
		//}

		for (int i = 0; i < squares.Length; i++) {
			//Find the correct square
			if (squares[i] != square) {
				continue; //keep looping
			}

			Debug.Log(square.name + "(#" + i + ") has the following requirements: a left neighbor who says '" + square.leftSquareString + "' and a right neighbor who says '" + square.rightSquareString + "'.");

			string compareLeft = "";
			string compareRight = "";

			if (i != 0) {
				compareLeft = squares[i - 1].myString;
			}
			if (i != squares.Length - 1) {
				compareRight = squares[i + 1].myString;
			}

			if (square.leftSquareString.Equals(compareLeft) || square.leftSquareString == compareLeft) {
				Debug.Log("Left side: '" + compareLeft + "'. Happy on the left!");
				isLeftCorrect = true;
			}
			else {
				Debug.Log("Left side: '" + compareLeft + "'. Sad on the left.");
				isLeftCorrect = false;
			}

			if (square.rightSquareString.Equals(compareRight) || square.rightSquareString == compareRight) {
				Debug.Log("Right side: '" + compareRight + "'. Happy on the right!");
				isRightCorrect = true;
			}
			else {
				Debug.Log("Right side: '" + compareRight + "'. Sad on the right.");
				isRightCorrect = false;
			}
		}
		
		if (isLeftCorrect && isRightCorrect) {
			Debug.Log(square.name + " is a happy square!");
			return true;
		}
		Debug.Log(square.name + " is not a happy square.");
		return false;
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

	public void MorphSquareToHappy (PuzzleSquare square) {
		MorphingImageHorror.MorphImage(square.GetComponent<Image>(), correctPlacementSprite);
	}

	public void MorphSquareToUnhappy (PuzzleSquare square) {
		MorphingImageHorror.MorphImage(square.GetComponent<Image>(), originalSprite);
	}

	public void DarkenSquareIfUnhappy (PuzzleSquare square) {
		if (square.transform.parent != attemptPanel) {
			MorphSquareToUnhappy(square);
		}
	}

	public void TakeDownPuzzle () {
		PuzzleSquare[] squares = attemptPanel.GetComponent<PuzzlePanel>().MySquaresInOrder;

		for (int i = 0; i < squares.Length; i++) {
			Debug.Log("Destroying " + squares[i].name + ".");
			squares[i].transform.SetParent(null);
			Destroy(squares[i].gameObject);
		}
		puzzleCanvas.gameObject.SetActive(false);
	}
}
