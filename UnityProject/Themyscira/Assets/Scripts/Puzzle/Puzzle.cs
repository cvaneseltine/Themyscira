using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Puzzle : MonoBehaviour {
	public Solution solution;

	public List<string> ScrambleWords () {
		List<string> words = new List<string>();

		words.AddRange(solution.wordStrings);

		words.OrderBy(a => Random.Range(0, 100));

		return words;
	}
}
