using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageMorpher : MonoBehaviour {

	public Image originalImage;
	public Image replacementImage;

	public Color originalImageColor;
	public Color replacementImageColor;

	float duration = 1; //duration in seconds
	float t = 0; //lerp control variable

	void Update () {
		LerpAlpha();
	}
	
	void LerpAlpha() {
		originalImage.color = Color.Lerp(originalImageColor, replacementImageColor, t);
		replacementImage.color = Color.Lerp(replacementImageColor, originalImageColor, t);
		if (t < 1) {
			t += Time.deltaTime / duration;
		}
		else {
			originalImage.sprite = replacementImage.sprite;
			originalImage.color = new Color(replacementImage.color.r, replacementImage.color.g, replacementImage.color.b, 1);
			Debug.Log(name + " has finished morphing.");
			Destroy(this);
			Destroy(replacementImage.gameObject);
		}
	}

	public void StartMorph (Image original, Image replacement) {
		originalImage = original;
		originalImageColor = new Color(originalImage.color.r, originalImage.color.g, originalImage.color.b, 1);
		replacementImage = replacement;
		replacementImageColor = new Color(originalImage.color.r, originalImage.color.g, originalImage.color.b, 0);
	}
}