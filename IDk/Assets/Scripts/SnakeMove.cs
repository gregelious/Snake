using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{

    private Vector2 direction;

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
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
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
        if (other.tag == "Food") // the red
        {
            Grow();
        }
        else if (other.tag == "Obstacle")
        {
            Debug.Log("Hit");
            Destroy(segments);
            segments = new List<Transform>(); // create new list
            segments.Add(transform);
        }
    }
}
