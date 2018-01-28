using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class MorphingImageHorror {

	public static void MorphImage (Image original, Sprite newSprite) {
		GameObject morpher = GameObject.Instantiate(original.gameObject);
		morpher.name = (original.gameObject.name + " imageMorpher");
		morpher.transform.SetParent(original.transform);
		morpher.transform.localPosition = Vector3.zero;
		morpher.GetComponent<Image>().sprite = newSprite;
		morpher.GetComponent<Image>().color = new Color(255, 255, 255, 0);
		Component.Destroy(morpher.GetComponent<PuzzleSquare>());
		ImageMorpher morphling = morpher.AddComponent<ImageMorpher>();
		morphling.StartMorph(original, morpher.GetComponent<Image>());
	}
}
