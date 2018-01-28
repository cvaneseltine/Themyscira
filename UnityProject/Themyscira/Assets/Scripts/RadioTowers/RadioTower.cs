using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTower : MonoBehaviour {

    public Hover hoverThing;
    public SpriteRenderer inactiveSprite;
    public SpriteRenderer activeSprite;

    public enum State
    {
        Inactive,
        Broadcasting,
        Solved
    }
    private State state;

	// Use this for initialization
	void Start () {
        SetState(State.Inactive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetState(State newState)
    {
        state = newState;

        // i think this works??
        hoverThing.SetActive(state == State.Broadcasting);
        activeSprite.gameObject.SetActive(state == State.Broadcasting || state == State.Solved);
        inactiveSprite.gameObject.SetActive(state == State.Inactive);
    }
}
