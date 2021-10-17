using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    [Range(1f, 30f)] public float Speed; // Speed of movement.
    protected Vector2 Direction; // Velocity (Vectored speed).
    public Animator EntityAnimator;
    public Rigidbody2D EntityRigidbody;

    // Animator's variables.
    public bool IsMoving => Direction.x != 0 || Direction.y != 0;
    public bool IsAlive;
    public bool IsJumping;
    public bool IsAttacking;
    public float AttackCounter;

    // Entity's base attributes.
    public int MaxHP;
    public int HealthPoints;

    //private string entityName;
    //private int healthPoints;
    //private Animator entityAnimator;
    //private Rigidbody2D entityRigidbody;
    //private bool isJumping;
    //private bool isAttacking;
    //private bool isAlive;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(EntityRigidbody == null || EntityAnimator == null)
        {
            EntityRigidbody = GetComponent<Rigidbody2D>();
            EntityAnimator = GetComponent<Animator>();
        }

        InitStats();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAnimLayers();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected virtual void InitStats()
    {
        MaxHP = 100;
        HealthPoints = MaxHP;
        IsAlive = true;
        Speed = (Speed > 0f ? Speed : 2f);
    }

    /// <summary>
    /// Moves the entity based on the computed vector and magnitude.
    /// </summary>
    public void Move()
    {
        EntityRigidbody.velocity = Direction.normalized * Speed;
    }

    /// <summary>
    /// Method to determine which animation state to 
    /// activate, based on the entity's movements.
    /// </summary>
    public void HandleAnimLayers()
    {
        if(IsMoving) // If the character is moving...
        {
            ActivateAnimLayer("Walk Layer");

            // Specify the Direction so that the sprite can face the appropriate Direction.
            EntityAnimator.SetFloat("x", Direction.x);
            EntityAnimator.SetFloat("y", Direction.y);
        }
        else if(IsJumping)
        {
            ActivateAnimLayer("Jump Layer");
        }
        else if(IsAttacking)
        {
            ActivateAnimLayer("Attack Layer");
        }
        else if(!IsAlive)
        {
            ActivateAnimLayer("Death Layer");
        }
        else // Default layer.
        {
            EntityAnimator.SetLayerWeight(1, 0);
            ActivateAnimLayer("Idle Layer");
        }
    }

    /// <summary>
    /// Method to activate the appropriate layer of animation.
    /// </summary>
    /// <param name="layerName"></param>
    public void ActivateAnimLayer(string layerName)
    {
        for(int i = 0; i < EntityAnimator.layerCount; i++)
        {
            EntityAnimator.SetLayerWeight(i, 0); // Disable all layers.
        }

        // Activate the specific layer using the passed in name.
        EntityAnimator.SetLayerWeight(EntityAnimator.GetLayerIndex(layerName), 1);
    }

    /// <summary>
    /// Method to decrease the health of the entity.
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        if(HealthPoints > 0)
        {
            HealthPoints -= damage; // Decrement hp.
            HealthPoints = (HealthPoints < 0 ? 0 : HealthPoints); // Ensure hp is not negative.
        }

        if(HealthPoints <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Method to perform healing.
    /// </summary>
    /// <param name="healing"></param>
    public void Heal(int healing)
    {
        // If current health points is less than the maximum allowed..
        if(HealthPoints < MaxHP)
        {
            HealthPoints += healing; // Heal.
            HealthPoints = HealthPoints > MaxHP ? MaxHP : HealthPoints; // Ensure HP is not over the maximum.
        }
    }

    public void Jump()
    {
        IsJumping = true;
        EntityAnimator.SetBool("Jump", IsJumping);
    }

    public void StopJump()
    {
        IsJumping = false;
        EntityAnimator.SetBool("Jump", IsJumping);
    }

    public virtual void Attack()
    {
        IsAttacking = true;
        EntityAnimator.SetBool("Attack", IsAttacking);
    }

    public void StopAttack()
    {
        IsAttacking = false;
        EntityAnimator.SetBool("Attack", IsAttacking);
    }

    public void Die()
    {
        // Ensure entity stops moving when it is dead.
        Direction = Vector2.zero;
        EntityRigidbody.velocity = Direction;

        IsAlive = false;
        EntityAnimator.SetBool("Die", !IsAlive);
    }
}