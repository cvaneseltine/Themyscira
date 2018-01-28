using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLocator : MonoBehaviour {

    private float radius;

	// Use this for initialization
	void Start () {
        radius = gameObject.transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 towerPos = TowerManager.Instance.GetBroadcastingTowerPosition();
        Vector3 playerPos = gameObject.transform.parent.position;
        Vector3 direction = towerPos - playerPos;
        gameObject.transform.localPosition = direction.normalized * radius;
	}
}
