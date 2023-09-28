using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider2D grid;

    // Start is called before the first frame update
    void Start()
    {
        RandomPos(); // randomize position of food 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // fxn to randomize the position of the food
    private void RandomPos()
    {
        Bounds bounds = grid.bounds; // declare limits of the space

        float x = Random.Range(bounds.min.x, bounds.max.x); // random x in limit
        float y = Random.Range(bounds.min.y, bounds.max.y); // random y in limit

        transform.position = new Vector2(Mathf.Round(x), Mathf.Round(y)); // round values and change position
    }

    //function for collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") // the red
        {
            RandomPos();
        }
    }
}
