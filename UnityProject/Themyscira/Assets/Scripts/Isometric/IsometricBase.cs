using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IsometricBase : MonoBehaviour {

    public float floorHeight = 0;

    Sprite sprite;

    //static float isoAngle = 45;

    private void Init()
    {
        if (sprite)
            return;
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Use this for initialization
    void Start ()
    {
        Init();   
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SetZ()
    {
        // todo: use tangent of isoAngle instead?
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position.Set(pos.x, pos.y, GetFloor());
    }

    private float GetFloor()
    {
        return gameObject.transform.position.y + floorHeight;
    }

    void OnDrawGizmos()
    {
        Init();
        // draw line to denote floor
        Gizmos.color = Color.cyan;
        float lineY = GetFloor();
        float lineLength = 2 * sprite.bounds.size.x / 3;
        float lineX1 = gameObject.transform.position.x - lineLength / 2;
        float lineX2 = gameObject.transform.position.x + lineLength / 2;
        Vector3 lineStart = new Vector3(lineX1, lineY, gameObject.transform.position.z);
        Vector3 lineEnd = new Vector3(lineX2, lineY, gameObject.transform.position.z);
        Debug.Log(lineStart.ToString() + " -> " + lineEnd.ToString());
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
