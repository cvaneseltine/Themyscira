using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsefulCanvas : MonoBehaviour {
	GraphicRaycaster m_Raycaster;
	PointerEventData m_PointerEventData;
	EventSystem m_EventSystem;

	void Start() {
		m_Raycaster = GetComponent<GraphicRaycaster>();
		m_EventSystem = GetComponent<EventSystem>();
	}

	List<GameObject> GetAllObjectsUnderMouse () {
		List<RaycastResult> raycastResults = new List<RaycastResult>();
		List<GameObject> results = new List<GameObject>();

		//Raycast using the Graphics Raycaster and mouse click position
		m_Raycaster.Raycast(m_PointerEventData, raycastResults);
		
		foreach (RaycastResult result in raycastResults) {
			results.Add(result.gameObject);
		}
		return results;
	}

	T GetObjectUnderMouse<T>() {
		m_PointerEventData = new PointerEventData(m_EventSystem);
		m_PointerEventData.position = Input.mousePosition;

		//Create a list of Raycast Results
		List<RaycastResult> results = new List<RaycastResult>();

		//Raycast using the Graphics Raycaster and mouse click position
		m_Raycaster.Raycast(m_PointerEventData, results);


		return default(T);
	}
}