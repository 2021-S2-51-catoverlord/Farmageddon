using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EntityController : MonoBehaviour
{
    protected static int MAX_HP = 100;

    [SerializeField]
    protected float speed; // Speed of movement.

    protected Vector2 direction; // Vector.
    private string entityName;
    private int healthPoints;
    private Animator entityAnimator;
    private Rigidbody2D entityRigidbody;

    // Get and set methods for entity's attributes.
    public string EntityName { get; set; }
    public int HealthPoints { get; set; }
    public Animator EntityAnimator { get; set; }
    public Rigidbody2D EntityRigidbody { get; set; }
    public bool IsMoving => direction.x != 0 || direction.y != 0;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the instance of the Rigidbody2D the GameObject is linked to and save it locally.
        EntityRigidbody = GetComponent<Rigidbody2D>();

        // Get the instance of the Animator the GameObject is linked to and save it locally.
        EntityAnimator = GetComponent<Animator>();

        // Initialise entity's hp.
        HealthPoints = EntityController.MAX_HP;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAnimLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Moves the character based on the speed and direction.
    /// </summary>
    public void Move()
    {
        // Move the sprite on the scene based on the calculated vector and magnitude.
        EntityRigidbody.velocity = direction.normalized * speed;
    }

    /// <summary>
    /// Method to determine which animation state based
    /// on the entity's change in movement.
    /// </summary>
    public void HandleAnimLayers()
    {
        // If the character is moving...
        if (IsMoving)
        {
            // Change the main layer to the walking layer.
            ActivateAnimLayer("Walk Layer");

            // Specify the direction so that the sprite can face the appropriate direction.
            EntityAnimator.SetFloat("x", direction.x);
            EntityAnimator.SetFloat("y", direction.y);
        }
        else // Otherwise...
        {
            // Set the idle layer to be the main layer.
            EntityAnimator.SetLayerWeight(1, 0);
            ActivateAnimLayer("Idle Layer");
        }
    }

    /// <summary>
    /// Method to disable all animation layers and activate the one the  
    /// entity is currently in (using the layer's name).
    /// </summary>
    /// <param name="layerName"></param>
    public void ActivateAnimLayer(string layerName)
    {
        // For each animation layer of the EntityController...
        for (int i = 0; i < EntityAnimator.layerCount; i++)
        {
            // Disable all the layers.
            EntityAnimator.SetLayerWeight(i, 0);
        }

        // Activate the specific layer using the passed in name.
        EntityAnimator.SetLayerWeight(EntityAnimator.GetLayerIndex(layerName), 1);
    }

    /// <summary>
    /// Method to decrease entity's hp.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        // If current health points is greater than 0...
        if (HealthPoints > 0)
        {
            // Decrement hp.
            HealthPoints -= damage;

            // Ensure HP is not a negative.
            HealthPoints = HealthPoints < 0 ? 0 : HealthPoints;
        }
    }

    /// <summary>
    /// Method to increase entity's hp.
    /// </summary>
    /// <param name="healing"></param>
    public void Heal(int healing)
    {
        // If current health points is less than the maximum (100)...
        if (HealthPoints < EntityController.MAX_HP)
        {
            // Increment hp.
            HealthPoints += healing;

            // Ensure HP is not over the maximum.
            HealthPoints = HealthPoints > EntityController.MAX_HP ? EntityController.MAX_HP : HealthPoints;
        }
    }
}