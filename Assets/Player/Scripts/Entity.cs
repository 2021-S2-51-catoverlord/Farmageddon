using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected static int MAX_HP = 100;
    
    private string entityName;
    private int healthPoints;

    [SerializeField]
    protected float speed; // Speed of movement.
    protected Vector2 direction; // Vector.
    protected Animator animator;

    // Get and set methods for name and hp.
    public string EntityName { get; set; }
    public int HealthPoints { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    /// <summary>
    /// Moves the character based on the speed and direction.
    /// </summary>
    public void Move()
    {
        // Move the sprite on the scene.
        transform.Translate(direction * speed * Time.deltaTime);

        // If the character is moving...
        if(direction.x != 0 || direction.y != 0)
        {
            // Configure settings so that the sprite can be animated appropriately.
            AnimateMovement(direction);
        }
        else // Otherwise...
        {
            // Set the idle layer to be the main layer.
            animator.SetLayerWeight(1, 0);
        }
    }

    /// <summary>
    /// Determines the state of which the Sprite should be in depending 
    /// on its direction it is heading towards.
    /// </summary>
    /// <param name="direction"></param>
    public void AnimateMovement(Vector2 direction)
    {
        // Change the main layer to thje walking layer.
        animator.SetLayerWeight(1, 1); 

        // Specify the direction so that the sprite can face the appropriate direction.
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
