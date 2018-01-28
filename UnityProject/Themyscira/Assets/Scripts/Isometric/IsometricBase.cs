using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IsometricBase : MonoBehaviour {

    public float floorHeight = 0;

    public SpriteRenderer spriteRenderer;

    Sprite sprite;

    //static float isoAngle = 45;

    private void Init()
    {
        if (sprite)
        {
            return;
        }

        if (!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        sprite = spriteRenderer.sprite;
    }

    // Use this for initialization
    void Start ()
    {
        Init();   
	}

    protected void SetZ()
    {
        // todo: use tangent of isoAngle instead?
        Vector3 pos = gameObject.transform.position;
        Quaternion rot = gameObject.transform.rotation;
        //Debug.Log(gameObject.name + GetFloor());
        gameObject.transform.SetPositionAndRotation(new Vector3(pos.x, pos.y, GetFloor()), rot);
        //Debug.Log("     z: " + gameObject.transform.position.z);
    }

    public float GetFloor()
    {
        return gameObject.transform.position.y + floorHeight;
    }

    void OnDrawGizmos()
    {
        Init();
        if (!sprite)
        {
            return;
        }
        // draw line to denote floor
        Gizmos.color = Color.cyan;
        float lineY = GetFloor();
        float lineLength = sprite.bounds.size.x;
        float lineX1 = gameObject.transform.position.x - lineLength / 2;
        float lineX2 = gameObject.transform.position.x + lineLength / 2;
        Vector3 lineStart = new Vector3(lineX1, lineY, gameObject.transform.position.z);
        Vector3 lineEnd = new Vector3(lineX2, lineY, gameObject.transform.position.z);
        Gizmos.DrawLine(lineStart, lineEnd);
    }
}
