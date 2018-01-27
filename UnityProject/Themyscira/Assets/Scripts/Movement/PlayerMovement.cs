using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed;

    // what the heck
    const int NORTH = 0;
    const int SOUTH = 1;
    const int EAST = 2;
    const int WEST = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // hack - update to use inputmanager
        int[] directions = new int[4];
        directions[NORTH] = Input.GetKey("W") ? 1 : 0;
        directions[SOUTH] = Input.GetKey("S") ? 1 : 0;
        directions[EAST] = Input.GetKey("D") ? 1 : 0;
        directions[WEST] = Input.GetKey("A") ? 1 : 0;

        // oh god fix these names
        // swap subtraction if going backwards
        Vector2 direction = new Vector2(directions[NORTH] - directions[SOUTH],
                                        directions[EAST] - directions[WEST]).normalized;
        direction.Scale(speed * Time.deltaTime);
    }
}
