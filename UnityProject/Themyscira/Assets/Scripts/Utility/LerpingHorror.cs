using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LerpingHorror {

	public static void StartLerping (Transform lerper, Vector3 destination, IWannaKnowAboutLerping lerpReport) {
		if (lerper.gameObject.GetComponent<Lerper>() == null) {
			lerper.gameObject.AddComponent<Lerper>();
		}
		lerper.gameObject.GetComponent<Lerper>().UpdateDestination(destination, lerpReport);
	}
}
