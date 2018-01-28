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
        TowerManager.Instance.RegisterTower(this);
	}

    public void SetState(State newState)
    {
        state = newState;

        // i think this works??
        hoverThing.SetActive(state == State.Broadcasting);
        activeSprite.gameObject.SetActive(state == State.Broadcasting || state == State.Solved);
        inactiveSprite.gameObject.SetActive(state == State.Inactive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Broadcasting)
        {
			StorylineManager.Instance.AdvanceStory();
            TowerManager.Instance.FinishCurrentTower();
        }
    }
}
