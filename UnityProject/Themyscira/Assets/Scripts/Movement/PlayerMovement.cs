using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float movementForce = 100;

    public SpriteRenderer leftSprite;
    public SpriteRenderer rightSprite;

    private Rigidbody2D rigidbody;

    // oh my god make it an enum
    private const int NORTH = 0;
    private const int SOUTH = 1;
    private const int EAST = 2;
    private const int WEST = 3;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // make sure we're allowed to move
        if (InputManager.Instance.mode != InputManager.Mode.Navigation)
        {
            return;
        }

        // hack - update to use inputmanager
        int[] buttons = new int[4];
        buttons[NORTH] = Input.GetKey(KeyCode.W) ? 1 : 0;
        buttons[SOUTH] = Input.GetKey(KeyCode.S) ? 1 : 0;
        buttons[EAST] = Input.GetKey(KeyCode.D) ? 1 : 0;
        buttons[WEST] = Input.GetKey(KeyCode.A) ? 1 : 0;

        //Debug.Log("[" + directions[NORTH] + "," + directions[SOUTH] + ","
        //          + directions[EAST] + "," + directions[WEST] + "]");

        // oh god fix these names
        // swap subtraction if going backwards
        Vector2 inputDirection = new Vector2(buttons[EAST] - buttons[WEST],
                                        buttons[NORTH] - buttons[SOUTH]);

        //Debug.Log("dir: " + direction.normalized);
        //Debug.Log("mag: " + speed * Time.deltaTime);

        Vector2 force = inputDirection.normalized * movementForce * Time.deltaTime;

        if (inputDirection.normalized.x < 0)
        {
            leftSprite.gameObject.SetActive(true);
            rightSprite.gameObject.SetActive(false);
        }
        else
        {
            leftSprite.gameObject.SetActive(false);
            rightSprite.gameObject.SetActive(true);
        }

        //currentVelocity = (currentVelocity + desiredVelocity).normalized * maxSpeed * Time.deltaTime;

        //Debug.Log("mov: " + currentVelocity);
        rigidbody.AddForce(force);

        // leaning?
        //gameObject.transform.RotateAround(new Vector3(0, GetComponent<IsometricBase>().GetFloor(), 0), Vector3.forward, -rigidbody.velocity.x * 3 * Time.deltaTime);
    }
}
