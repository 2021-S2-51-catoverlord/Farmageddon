using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    protected int maxHP = 100;

    [SerializeField]
    protected float speed; // Speed of movement.

    protected Vector2 direction; // Vector.
    private string entityName;
    private int healthPoints;
    private Animator entityAnimator;
    private Rigidbody2D entityRigidbody;
    private bool isJumping;
    private bool isAttacking;
    private bool isAlive;

    // Get and set methods for entity's attributes.
    public string EntityName { get; set; }
    public int HealthPoints { get; set; }
    public Animator EntityAnimator { get; set; }
    public Rigidbody2D EntityRigidbody { get; set; }
    public bool IsMoving => direction.x != 0 || direction.y != 0;
    public bool IsJumping { get; set; }
    public bool IsAttacking { get; set; }
    public bool IsAlive { get; set; }
    public float AttackTime { get; set; }
    public float AttackCounter { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the instance of the Rigidbody2D the GameObject is linked to and save it locally.
        EntityRigidbody = GetComponent<Rigidbody2D>();

        // Get the instance of the Animator the GameObject is linked to and save it locally.
        EntityAnimator = GetComponent<Animator>();

        // Initialise entity's hp.
        HealthPoints = this.maxHP;

        // Set the entity to be alive.
        IsAlive = true;
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
        else if(IsJumping)
        {
            // Change the main layer to the jumping layer.
            ActivateAnimLayer("Jump Layer");
        }
        else if(IsAttacking)
        {
            // Change the main layer to the attacking layer.
            ActivateAnimLayer("Attack Layer");
        }
        else if(!IsAlive)
        {
            ActivateAnimLayer("Death Layer");
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
        
        // If current health points is 0 or less....
        if(HealthPoints <= 0)
        {
            // Ensure entity stops moving when it is dead.
            direction = Vector2.zero;
            EntityRigidbody.velocity = direction;

            // Trigger the death layer.
            EntityAnimator.SetTrigger("Die");
            IsAlive = false;
        }
    }

    /// <summary>
    /// Method to increase entity's hp.
    /// </summary>
    /// <param name="healing"></param>
    public void Heal(int healing)
    {
        // If current health points is less than the maximum (100)...
        if (HealthPoints < maxHP)
        {
            // Increment hp.
            HealthPoints += healing;

            // Ensure HP is not over the maximum.
            HealthPoints = HealthPoints > maxHP ? maxHP : HealthPoints;
        }
    }

    public void StopJump()
    {
        IsJumping = false;
        EntityAnimator.SetBool("Jump", IsJumping);
    }

    public void Attack()
    {
        AttackCounter = AttackTime;

        // Set the entity's state to attacking.
        IsAttacking = true;

        // Set Attack in animator parameter to true.
        EntityAnimator.SetBool("Attack", IsAttacking);
    }

    public void StopAttack()
    {
        IsAttacking = false;
        EntityAnimator.SetBool("Attack", IsAttacking);
    }
}