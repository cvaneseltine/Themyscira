using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class IsometricBase : MonoBehaviour {

    public float floorHeight = 0;

    Sprite sprite;

    static float isoAngle = 45;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<Sprite>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetZ()
    {
        
    }

    private float GetFloor()
    {
        return gameObject.transform.position.y + floorHeight;
    }

    void OnDrawGizmos()
    {
        // draw line to denote floor
        Gizmos.color = Color.magenta;
        float lineY = GetFloor();
        float lineWidth = sprite.bounds.size.x;
        float lineX1 = gameObject.transform.position.x - lineWidth / 2;
        float lineX2 = gameObject.transform.position.x + lineWidth / 2;
        Vector3 lineStart = new Vector3(lineX1, lineY, gameObject.transform.position.z);
        Vector3 lineEnd = new Vector3(lineX2, lineY, gameObject.transform.position.z);
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
