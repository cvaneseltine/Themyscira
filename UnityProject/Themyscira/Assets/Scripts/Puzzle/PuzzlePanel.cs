using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PuzzlePanel : MonoBehaviour {

	RectTransform rectangle;
	float width;

	void Start() {
		rectangle = (RectTransform)transform;
		width = rectangle.rect.width;
	}

	public void UpdateSquareHomes () {
		List<PuzzleSquare> squares = new List<PuzzleSquare>();
		
		foreach (Transform child in transform) {
			if (child.GetComponent<PuzzleSquare>() != null) {
				squares.Add(child.GetComponent<PuzzleSquare>());
			}
		}

		squares = squares.OrderBy(a => a.transform.position.x).ToList<PuzzleSquare>();

		float priorX = 0;

		foreach (PuzzleSquare square in squares) {
			priorX = square.transform.position.x;
		}

		float spacing = width / squares.Count;
		
		for (int i = 0; i < squares.Count; i++) {
			PuzzleSquare square = squares[i];
			Vector3 home = new Vector3(rectangle.position.x - (width / 2) + (spacing / 2) + (spacing * i), rectangle.position.y, rectangle.position.z);
			square.ChangeHome(home);
			square.GoHome();
		}
	}

	public void TestPuzzleSolution (Solution solution) {
		List<PuzzleSquare> squares = new List<PuzzleSquare>();

		foreach (Transform child in transform) {
			if (child.GetComponent<PuzzleSquare>() != null) {
				squares.Add(child.GetComponent<PuzzleSquare>());
			}
		}

		if (squares.Count != solution.wordStrings.Length) {
			Debug.Log("Sorry, square, but your solution requires another arrangement. ("
						+ squares.Count + " squares in place versus " + solution.wordStrings.Length + " needed)");
			return;
		}

		squares = squares.OrderBy(a => a.transform.position.x).ToList<PuzzleSquare>();

		for (int i = 0; i < solution.wordStrings.Length; i++) {
			PuzzleSquare square = squares[i];

			if (!square.GetComponentInChildren<Text>().text.Equals(solution.wordStrings[i])) {
				Debug.Log("Sorry, square, but your solution requires another arrangement. ("
						+ square.GetComponentInChildren<Text>().text + " does not match " + solution.wordStrings[i] + " needed)");
				return;
			}
		}

		Debug.Log("CONGRATULATIONS! You have solved the puzzle!");
	}
}
