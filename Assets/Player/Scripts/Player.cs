using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the instance of the Animator the GameObject is linked to and save it locally.
        base.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
       
        // Call the parent's Update which calls the parent's Move method. 
        base.Update();
    }

    /// <summary>
    /// Get input from user to compute a direction for the player's 
    /// character's movement.
    /// </summary>
    private void GetInput()
    {
        // Reset the vector at every loop/call.
        direction = Vector2.zero;

        /// Code for single direction.
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move up.
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left.
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move down.
            direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right.
            direction += Vector2.right;
        }
        

        /*
        /// Code for diagonal movements.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move up.
            direction += Vector2.up;
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left.
            direction += Vector2.left;
        }
        
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move down.
            direction += Vector2.down;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right.
            direction += Vector2.right;
        }
        */
    }
}
