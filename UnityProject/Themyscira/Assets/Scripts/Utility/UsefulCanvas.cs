using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UsefulCanvas : MonoBehaviour {
	GraphicRaycaster m_Raycaster;
	PointerEventData m_PointerEventData;
	EventSystem m_EventSystem;

	protected void PrepRaycaster () { //***THIS MUST BE CALLED IN CHILD CLASS START
		//Fetch the Raycaster from the GameObject (the Canvas)
		m_Raycaster = GetComponent<GraphicRaycaster>();
		//Fetch the Event System from the Scene
		m_EventSystem = GetComponent<EventSystem>();
	}

	protected List<GameObject> GetAllObjectsUnderMouse () {
		List<RaycastResult> raycastResults = new List<RaycastResult>();
		List<GameObject> results = new List<GameObject>();

		//Raycast using the Graphics Raycaster and mouse click position
		m_Raycaster.Raycast(m_PointerEventData, raycastResults);
		
		foreach (RaycastResult result in raycastResults) {
			results.Add(result.gameObject);
		}
		return results;
	}

	protected T GetObjectUnderMouse<T>() {
		//Define the kind of object you want based on its pointer.

		m_PointerEventData = new PointerEventData(m_EventSystem);
		m_PointerEventData.position = Input.mousePosition;
		
		List<RaycastResult> results = new List<RaycastResult>();

		m_Raycaster.Raycast(m_PointerEventData, results);
		
		foreach (RaycastResult result in results) {
			if (result.gameObject.GetComponent<T>() != null) {
				return result.gameObject.GetComponent<T>();
			}
		}
		return default(T);
	}
}