using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleCanvas : UsefulCanvas {
    GraphicRaycaster m_Raycaster;
	PointerEventData m_PointerEventData;
	EventSystem m_EventSystem;

	PuzzleSquare followingMouse = null;

	static PuzzleCanvas singleton;

	static public PuzzleCanvas Instance {
		get {
			return singleton;
			}
		}

	void Start() {
		if (singleton == null) {
			singleton = this;
		}
	}

	public void ProcessInput() {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			m_PointerEventData = new PointerEventData(m_EventSystem);
			m_PointerEventData.position = Input.mousePosition;

			//Create a list of Raycast Results
			List<RaycastResult> results = new List<RaycastResult>();

			//Raycast using the Graphics Raycaster and mouse click position
			m_Raycaster.Raycast(m_PointerEventData, results);

			//For every result returned, output the name of the GameObject on the Canvas hit by the Ray
			foreach (RaycastResult result in results) {
				Debug.Log("Hit " + result.gameObject.name);
				if (result.gameObject.GetComponent<PuzzleSquare>() != null) {
					Debug.Log("Follow the mouse, " + result.gameObject.name + "!");
					followingMouse = result.gameObject.GetComponent<PuzzleSquare>();
					followingMouse.isFollowingMouse = true;
					break;
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			Debug.Log("Stop following the mouse, " + followingMouse.name + "!");
			followingMouse.isFollowingMouse = false;
			followingMouse = null;

			Debug.Log("Now to attach you to your new home.");
			m_PointerEventData = new PointerEventData(m_EventSystem);
			m_PointerEventData.position = Input.mousePosition;

			List<RaycastResult> results = new List<RaycastResult>();


		}
	}
}