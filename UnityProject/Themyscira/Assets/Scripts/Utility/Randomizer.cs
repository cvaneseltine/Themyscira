using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Randomizer {
//Static class for stuff you need randomized.

	static public List<T> RandomizeList<T>(List<T> list) {
		List<T> startingList = new List<T>();
		startingList.AddRange(list);

		List<T> randomizedList = new List<T>();

		while (startingList.Count > 0) {
			T next = startingList[Random.Range(0, startingList.Count)];
			startingList.Remove(next);
			randomizedList.Add(next);
		}

		return randomizedList;
	}
}
