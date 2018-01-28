using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    static TowerManager instance;

    static public TowerManager Instance
    {
        get {
            return instance;
        }
    }

    private List<RadioTower> towers = new List<RadioTower>();
    private int currentTowerIndex = 0;

	// Use this for initialization
	void Start () {
		if (instance != null)
        {
            Debug.LogWarning("more than one tower manager!!! BAD");
        }
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetBroadcastingTowerPosition()
    {
        return towers[currentTowerIndex].gameObject.transform.position;
    }

    public void RegisterTower(RadioTower tower)
    {
        // first tower should start out broadcasting
        if (towers.Count == 0)
        {
            tower.SetState(RadioTower.State.Broadcasting);
        }
        towers.Add(tower);
    }

    public void FinishCurrentTower()
    {
        towers[currentTowerIndex].SetState(RadioTower.State.Solved);
		Debug.Log("Tower #" + currentTowerIndex + " at " + towers[currentTowerIndex].transform.position + " is complete!");
        if (currentTowerIndex >= towers.Count - 1)
        {
            // GAME DONE, YOU WIN
            return;
        }

        ++currentTowerIndex;
        towers[currentTowerIndex].SetState(RadioTower.State.Broadcasting);
		Debug.Log("Tower #" + currentTowerIndex + " is now broadcasting. Location: " + towers[currentTowerIndex].transform.position);
		}
}
