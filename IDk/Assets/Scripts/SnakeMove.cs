using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour
{

    private Vector2 direction;
    public bool goingUp;
    public bool goingDown;
    public bool goingLeft;
    public bool goingRight;

    List<Transform> segments; // stores all parts of the body of the snake
    public Transform bodyPrefab; // variable to store the body

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>(); // create new list
        segments.Add(transform); // add head of snake to the list
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && goingDown != true)
        {
            direction = Vector2.up;
            goingUp = true;
            goingDown = false;
            goingLeft = false;
            goingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && goingUp != true)
        {
            direction = Vector2.down;
            goingUp = false;
            goingDown = true;
            goingLeft = false;
            goingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.A) && goingRight != true)
        {
            direction = Vector2.left;
            goingUp = false;
            goingDown = false;
            goingLeft = true;
            goingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && goingLeft != true)
        {
            direction = Vector2.right;
            goingUp = false;
            goingDown = false;
            goingLeft = false;
            goingRight = true;
        }
    }

    //fixedUpdate is called at a fixed interval
    void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--) // for each segment
        {
            segments[i].position = segments[i - 1].position; // move the body
        }

        //move snake
        this.transform.position = new Vector2(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y
            );
    }

    //fxn to make the snake grow
    void Grow()
    {
        Transform segment = Instantiate(this.bodyPrefab); // create new body part
        segment.position = segments[segments.Count - 1].position; // position it to the back of snake
        segments.Add(segment); // add it to the list
    }

    //function for collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food") //if snake touches food
        {
            Grow(); // adds body part to snake
            Time.fixedDeltaTime -= 0.001f; //gets faster
        }
        else if (other.tag == "Obstacle") //if it touches obstacle
        {
            Debug.Log("Hit"); // shows up in console
            SceneManager.LoadScene("EndScene"); //change to end scene
            SceneManager.LoadScene("SampleScene"); // go back to start screen
        }
    }
}
