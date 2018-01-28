using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour {
	//aka LerpingHorror

	float lerpTime = 1f;
	float currentLerpTime;
	Vector3 lerpStart;
	Vector3 lerpEnd;
	IWannaKnowAboutLerping lerpReport;
	
	// Update is called once per frame
	void Update () {
		//Are we there yet?
		if (Mathf.Approximately(transform.position.x, lerpEnd.x) && Mathf.Approximately(transform.position.y, lerpEnd.y)) {
			Debug.Log("LerpingHorror " + name + " has reached its destination. End of lerping.");
			lerpReport.ReportLerpFinished(this);
		}
		else {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}

			//lerp!
			float perc = currentLerpTime / lerpTime;
			transform.position = Vector3.Lerp(lerpStart, lerpEnd, perc);
		}
	}

	public void UpdateDestination (Vector3 destination, IWannaKnowAboutLerping reportLerpHere) {
		lerpStart = transform.position;
		lerpEnd = destination;
		lerpReport = reportLerpHere;
		currentLerpTime = 0;
	}
}
