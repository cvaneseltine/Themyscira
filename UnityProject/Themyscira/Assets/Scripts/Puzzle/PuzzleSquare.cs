using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSquare : MonoBehaviour {

	float lerpTime = 1f;
	float currentLerpTime;
	Vector3 lerpStart;
	
	public Vector3 home;

	public bool isFollowingMouse = false;
	public bool isGoingHome = false;

	public string translation;

	public string leftSquareString = ""; //What should be on the square to the left, in the correct position?
	public string myString = ""; //What text do I have?
	public string rightSquareString = ""; //What should be on the square to the right, in the correct position?

	void Update () {
		if (isFollowingMouse) {
			transform.position = Input.mousePosition;
		}
		else if (isGoingHome) {
			//Are we there yet?
			if (Mathf.Approximately (transform.position.x, home.x) && Mathf.Approximately(transform.position.y, home.y)) {
				isGoingHome = false;
				PuzzleController.Instance.TestPuzzleSolution();
				PuzzleController.Instance.DarkenSquareIfUnhappy(this);
				//Debug.Log(name + " here, and I have gotten to my new home. Did we win?");
			}
			else {  //If not, head in that direction.
					//increment timer once per frame
				currentLerpTime += Time.deltaTime;
				if (currentLerpTime > lerpTime) {
					currentLerpTime = lerpTime;
				}

				//lerp!
				float perc = currentLerpTime / lerpTime;
				transform.position = Vector3.Lerp(lerpStart, home, perc);
			}
		}
	}

	public void StartFollowingMouse () {
		//Debug.Log(name + " here, following the mouse.");
		isGoingHome = false;
		isFollowingMouse = true;
	}

	public void ChangeHome (Vector3 newHome) {
		home = newHome;
	}

	public void GoHome () {
		//Debug.Log(name + " here, and I'm going home.");
		isFollowingMouse = false;
		isGoingHome = true;
		lerpStart = transform.position;
		currentLerpTime = 0;
	}
}
