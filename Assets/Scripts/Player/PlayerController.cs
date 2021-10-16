using System.Collections;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField] protected StatBarController HealthBar;
    [SerializeField] protected StatBarController StaminaBar;
    [SerializeField] public LevelSystem Level; // Player's subsystems.
    [SerializeField] public MoneySystem Money; // Player's subsystems.
    [SerializeField] [Range(1, 500)] public int Damage;

    // Player attributes.
    public int MaxStamina;
    public int StaminaPoints;
    
    public bool IsInventoryActive; // Implementation for locking clicking status.

    private Coroutine _regen;
    private readonly WaitForSeconds _regenTick = new WaitForSeconds(0.1f);

    public void Awake()
    {
        if(HealthBar == null || StaminaBar == null || Level == null || Money == null)
        {
            HealthBar = GameObject.Find("Health Bar").GetComponent<StatBarController>();
            StaminaBar = GameObject.Find("Stamina Bar").GetComponent<StatBarController>();
            Level = GameObject.Find("EXP Bar").GetComponent<LevelSystem>();
            Money = GameObject.Find("Gold Bar").GetComponent<MoneySystem>();
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        InitStats();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(IsAlive)
        {
            GetInput();
        }

        HealthBar.SetCurrentValue(HealthPoints);

        base.Update(); // Call the parent's Update method which will call the parent's Move method.
    }

    /// <summary>
    /// Initialise the player's statistics (name, stamina, exp, speed, damage) 
    /// and stats bar (health and stamina bar).
    /// </summary>
    private void InitStats()
    {
        // Initialise entity name, stamina, and exp.
        EntityName = "Player";
        MaxStamina = 30;
        StaminaPoints = MaxStamina;
        Speed = (Speed < 6f ? 6f : Speed);
        Damage = (Damage < 10 ? 10 : Damage);

        // Initilise the sliders' max values.
        HealthBar.SetMaxValue(MaxHP);
        StaminaBar.SetMaxValue(MaxStamina);
    }

    /// <summary>
    /// Poll input from user to compute a Direction for the player's character's movement.
    /// </summary>
    private void GetInput()
    {
        // Reset the vector and action bools at every loop/call.
        Direction = Vector2.zero;
        IsJumping = false;
        IsAttacking = false;

        /// Code for single Direction.
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Direction += Vector2.up;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Direction += Vector2.left;
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Direction += Vector2.down;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Direction += Vector2.right;
        }

        // Input for jump.
        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        // Input for attack (left mouse-click)
        if(!IsInventoryActive && Input.GetMouseButton(0))
        {
            Attack();
        }

        // Test health bar.
        if(Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(5);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            Heal(3);
        }

        // Test stamina bar.
        if(Input.GetKeyDown(KeyCode.C))
        {
            UseStamina(2);
        }
    }

    /// <summary>
    /// Method to hide player after their death, reset statistics,
    /// and lastly shift player to appear by their house. This method
    /// is called at the end of the death animation.
    /// </summary>
    /// <returns></returns>
    public IEnumerator RespawnPlayer()
    {
        SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();
        playerRenderer.enabled = false; // Hide Player.

        yield return new WaitForSeconds(5f);

        transform.position = new Vector3(8f, -17f, -1f); // Set respawn position to the house.
        Start(); // Resets statistics (hp, stamina, exp, speed, damage, isalive...).
        EntityAnimator.SetFloat("y", -0.5f); // Make player face down when respawn.
        EntityAnimator.SetBool("Die", !IsAlive); // Exit death animation state.
        playerRenderer.enabled = true; // Make player visible again.
    }

    /// <summary>
    /// Method to decrease player's stamina and update UI (stamina bar).
    /// </summary>
    /// <param name="amount"></param>
    public void UseStamina(int amount)
    {
        if(StaminaPoints > 0)
        {
            StaminaPoints -= amount;
            StaminaPoints = (StaminaPoints < 0 ? 0 : StaminaPoints); // Ensure stamina is not a negative.
        }
        else
        {
            Debug.Log("Not enough stamina!!");
        }


        if(_regen != null) // If stamina is already regenerating...
        {
            StopCoroutine(_regen); // Resets (Disallow player to regenerate while usiong stamina).
        }

        // Starts the co-routine of regenerating stamina.
        _regen = StartCoroutine(RegenStamina());
        StaminaBar.SetCurrentValue(StaminaPoints);
    }

    /// <summary>
    /// Method to auto-regenerate player's stamina when player is idling (not using any stamina).
    /// </summary>
    /// <returns></returns>
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1.5f);

        while(StaminaPoints < MaxStamina)
        {
            StaminaPoints++;
            StaminaBar.SetCurrentValue(StaminaPoints);

            yield return _regenTick; // Create a 10 ms delay. 
        }
    }

    public void IncreaseHealth(int level)
    {
        MaxHP += (int)(HealthPoints * 0.03) * (int)((100 - level) * 0.03);
        HealthPoints = MaxHP;
        HealthBar.SetMaxValue(MaxHP);
    }

    public void IncreaseStamina(int level)
    {
        MaxStamina += (int)(StaminaPoints * 0.05) * (int)((100 - level) * 0.05);
        StaminaPoints = MaxStamina;
        StaminaBar.SetMaxValue(MaxStamina);
    }
}