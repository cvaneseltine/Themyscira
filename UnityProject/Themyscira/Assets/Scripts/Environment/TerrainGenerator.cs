using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public Rect spawnBounds;

    public int numRadioTowersToSpawn = 5;
    public float minDistanceBetweenTowers = 50;

    public int numSceneryBitsToSpawn = 10;
    public float minDistanceBetweenBits = 2;

    public Transform radioTower;
    public Transform[] scenery;

    private List<Vector3> usedPositions = new List<Vector3>();

	// Use this for initialization
	void Start ()
    {
        // radio towers
        // do these first because we want them far apart from each other
        for (int i = 0; i < numRadioTowersToSpawn; ++i)
        {
            Vector3 position = ChooseRandomPosition(minDistanceBetweenTowers);
            Instantiate(radioTower, position, Quaternion.identity);
        }

        // scenery bits
        for (int i = 0; i < numSceneryBitsToSpawn; ++i)
        {
            Vector3 position = ChooseRandomPosition(minDistanceBetweenBits);
            Transform prefab = scenery[Random.Range(0, scenery.Length)];
            Instantiate(prefab, position, Quaternion.identity);
        }
	}

    Vector3 ChooseRandomPosition(float minDistance, int recursions = 0)
    {
        Vector3 newPos = new Vector2(Random.Range(spawnBounds.xMin, spawnBounds.xMax),
                                     Random.Range(spawnBounds.yMin, spawnBounds.yMax));

        // avoid infinite loops
        if (recursions > 500)
        {
            // if we're too close to something else, pick a new position
            foreach (Vector3 usedPos in usedPositions)
            {
                if (Vector3.Distance(newPos, usedPos) < minDistance)
                {
                    return ChooseRandomPosition(minDistance, recursions + 1);
                }
            }
        }

        usedPositions.Add(newPos);
        return newPos;
    }

    void OnDrawGizmos()
    {
        // draw lines to show bounds
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
