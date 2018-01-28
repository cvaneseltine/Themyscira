using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Rect spawnBounds;
    public int numSceneryBitsToSpawn = 10;

    public Transform[] scenery;

	// Use this for initialization
	void Start ()
    {
        // scenery bits
        for (int i = 0; i < numSceneryBitsToSpawn; ++i)
        {
            Vector3 position = ChooseRandomPosition();
            Transform prefab = scenery[Random.Range(0, scenery.Length)];
            Instantiate(prefab, position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    Vector2 ChooseRandomPosition()
    {
        return new Vector2(Random.Range(spawnBounds.xMin, spawnBounds.xMax), Random.Range(spawnBounds.yMin, spawnBounds.yMax));
    }

    void OnDrawGizmos()
    {
        // draw line show bounds
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector2(spawnBounds.xMin, spawnBounds.yMin), 
                        new Vector2(spawnBounds.xMin, spawnBounds.yMax));
        Gizmos.DrawLine(new Vector2(spawnBounds.xMin, spawnBounds.yMin),
                        new Vector2(spawnBounds.xMax, spawnBounds.yMin));
        Gizmos.DrawLine(new Vector2(spawnBounds.xMin, spawnBounds.yMax),
                        new Vector2(spawnBounds.xMax, spawnBounds.yMax));
        Gizmos.DrawLine(new Vector2(spawnBounds.xMax, spawnBounds.yMin),
                        new Vector2(spawnBounds.xMax, spawnBounds.yMax));
    }
}
