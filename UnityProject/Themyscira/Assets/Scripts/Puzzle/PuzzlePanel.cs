using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PuzzlePanel : MonoBehaviour {

	public PuzzleSquare[] MySquaresInOrder {
		get {
			return GetComponentsInChildren<PuzzleSquare>().OrderBy(a => a.transform.position.x).ToArray<PuzzleSquare>();
		}
	}

	public void UpdateSquareHomes () {
		PuzzleSquare[] squares = MySquaresInOrder;

		if (squares.Length == 0) {
			Debug.Log(name + " is not updating its squares' homes because it has no squares to update.");
		}

		//foreach (PuzzleSquare square in squares) {
		//	square.transform.localScale = Vector3.one;
		//	square.transform.position = Vector3.zero;
		//}

		RectTransform rectangle = (RectTransform)transform;
		float width = rectangle.rect.width;

		float spacing = width / squares.Length;

		Debug.Log("Spacing is " + spacing + ". Width is " + width + ". Squares length is " + squares.Length + ".");
		
		for (int i = 0; i < squares.Length; i++) {
			PuzzleSquare square = squares[i];
			Vector3 home = new Vector3(rectangle.position.x - (width / 2) + (spacing / 2) + (spacing * i), rectangle.position.y, rectangle.position.z);
			square.ChangeHome(home);
			Debug.Log(square.name + " is now going home to " + square.home + ".");
			square.GoHome();
		}
	}

	public bool TestPuzzleSolution (Solution solution) {
		bool isCorrectSolution = true;

		PuzzleSquare[] squares = MySquaresInOrder;

		for (int i = 0; i < squares.Length; i++) {
			if (PuzzleController.Instance.IsSquareHappyWithItsNeighbors(squares[i])) {
				PuzzleController.Instance.MorphSquareToHappy(squares[i]);
			}
			else {
				isCorrectSolution = false;
				PuzzleController.Instance.MorphSquareToUnhappy(squares[i]);
			}
		}

		if (squares.Length != solution.wordStrings.Length) {
			isCorrectSolution = false;
		}

		if (isCorrectSolution) {
			Debug.Log("CONGRATULATIONS! You have solved the puzzle!");
			return true;
		}
		return false;
	}
}
