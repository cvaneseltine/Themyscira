﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


	void Update () {
	if (Input.GetMouseButtonDown(0))
		Debug.Log("Pressed left click.");
	}
}