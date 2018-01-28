using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PuzzlePanel : MonoBehaviour {

	RectTransform rectangle {
		get { return (RectTransform)transform;}
	}

	float width {
		get { return rectangle.rect.width; }
	}

	public PuzzleSquare[] MySquaresInOrder {
		get {
			return GetComponentsInChildren<PuzzleSquare>().OrderBy(a => a.transform.position.x).ToArray<PuzzleSquare>();
		}
	}

	public void UpdateSquareHomes () {
		float priorX = 0;

		foreach (PuzzleSquare square in MySquaresInOrder) {
			priorX = square.transform.position.x;
		}

		float spacing = width / MySquaresInOrder.Length;
		
		for (int i = 0; i < MySquaresInOrder.Length; i++) {
			PuzzleSquare square = MySquaresInOrder[i];
			Vector3 home = new Vector3(rectangle.position.x - (width / 2) + (spacing / 2) + (spacing * i), rectangle.position.y, rectangle.position.z);
			square.ChangeHome(home);
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
