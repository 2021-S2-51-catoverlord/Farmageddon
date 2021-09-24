using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    protected static int MAX_STAMINA = 30;

    [SerializeField]
    protected StatBarController healthBar;

    [SerializeField]
    protected StatBarController staminaBar;

    [SerializeField]
    public int Damage;

    private int staminaPoints;
    private int experiencePoints;
    private Coroutine regen;
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    // Get and set methods.
    public int StaminaPoints { get; set; }
    public int ExperiencePoints { get; set; }

    // Start is called before the first frame update

    //Implementation for locking clicking status
    public bool isInventoryActive;

    protected override void Start()
    {
        if(healthBar == null || staminaBar == null)
        {
            healthBar = GameObject.Find("Health Bar").GetComponent<StatBarController>();
            staminaBar = GameObject.Find("Stamina Bar").GetComponent<StatBarController>();
        }

        base.Start();

        InitStats();

        // Get the instance of the Animator the GameObject is linked to and save it locally.
        //base.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        healthBar.SetCurrentValue(HealthPoints);

        // Call the parent's Update method which will call the parent's Move method. 
        base.Update();
    }

    /// <summary>
    /// Initialise the player's statistics (name, hp, stamina, exp) and 
    /// stats bar (health and stamina bar).
    /// </summary>
    private void InitStats()
    {
        // Initialise entity name, stamina, and exp.
        EntityName = "Player";
        StaminaPoints = PlayerController.MAX_STAMINA;
        ExperiencePoints = 0;
        Damage = 5;

        // Initilise the sliders' max values.
        healthBar.SetMaxValue(EntityController.MAX_HP);
        staminaBar.SetMaxValue(PlayerController.MAX_STAMINA);
    }

    /// <summary>
    /// Get input from user to compute a direction for the player's 
    /// character's movement.
    /// </summary>
    private void GetInput()
    {
        // Reset the vector at every loop/call.
        this.direction = Vector2.zero;
        this.IsJumping = false;
        this.IsAttacking = false;

        /// Code for single direction.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Move up.
            this.direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Move left.
            this.direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            // Move down.
            this.direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Move right.
            this.direction += Vector2.right;
        }

        // Input for jump.
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        if(IsAttacking)
        {
            AttackCounter -= Time.deltaTime;
            if(AttackCounter <= 0)
            {
                StopAttack();
            }
        }

        // Input for attack (left mouse-click)
        if(!isInventoryActive && Input.GetMouseButton(0))
        {
            Attack();
        }

        // Test health bar.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(3);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Heal(3);
        }

        // Test stamina bar.
        if (Input.GetKeyDown(KeyCode.C))
        {
            UseStamina(2);
        }
    }

    /// <summary>
    /// Method to decrease player's stamina and update UI (stamina bar).
    /// </summary>
    /// <param name="amount"></param>
    public void UseStamina(int amount)
    {
        // If there is still some stamina...
        if (StaminaPoints > 0)
        {
            // Decrement stamina.
            StaminaPoints -= amount;

            // Ensure stamina is not a negative.
            StaminaPoints = StaminaPoints < 0 ? 0 : StaminaPoints;
        }
        else // Otherwise...
        {
            Debug.Log("Not enough stamina!!");
        }

        // If stamina is already regenerating...
        if(regen != null)
        {
            // Resets (Does not allow player to regenerate and use stamina at the same time).
            StopCoroutine(regen);
        }

        // Starts the coroutine of regenerating.
        regen = StartCoroutine(RegenStamina());

        // Update the stamina bar slider.
        staminaBar.SetCurrentValue(StaminaPoints);
    }

    /// <summary>
    /// Method to auto-regenerate player's stamina when player is idling (not using any stamina).
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegenStamina()
    {
        // Create a 1-second delay.
        yield return new WaitForSeconds(1.5f);

        // While current stamina is less than the max stamina...
        while(StaminaPoints < PlayerController.MAX_STAMINA)
        {
            // Increment stamina.
            StaminaPoints += 1;

            // Update the stamina bar slider.
            staminaBar.SetCurrentValue(StaminaPoints);

            // Create a 10 ms delay.
            yield return regenTick; 
        }
    }

    private void Jump()
    {
        // Set the entity's state to jumping.
        IsJumping = true;

        // Set Attack in animator parameter to true.
        EntityAnimator.SetBool("Jump", IsJumping);
    }    
}