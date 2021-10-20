using UnityEngine;

public class MonsterController : EntityController
{
    //object variables
    private GameObject targetObj = null; // get player object
    private Transform target = null; //get player transform
    private PlayerController Player = null;
    private DayNightCycleBehaviour TimeController = null;
    //search and movement variables
    [SerializeField]
    private float baseSearch = 10f; // base range of the monster
    [SerializeField]
    [Range(0f, 360f)]
    private float visionArc = 45f; //angle of vison on a enemy in degrees, max 360f
    [SerializeField]
    [Range(1, 1000)]
    private int searchBoost = 5; //how far the range of the monster becomes once they have seen the target
    [SerializeField]
    [Range(5, 100)]
    private int maxMovement; //used in wander to determine how long the ai moves in a single direction.
    private MonsterBehaviour monsterState = MonsterBehaviour.Wandering;
    private int wanderMovement = 0;
    private int wanderdirection;
    //combat variables
    [SerializeField]
    private int baseDmg;
    [SerializeField]
    private int scalingByDay = 20;
    private int enemyDamage;
    [SerializeField]
    private int baseHP;
    [SerializeField]
    private float attackCooldownInSeconds = 1;
    private float timeStamp = -1; //timeStamp starts negative so we can initially set the timeStamp
    private bool playerSeen = false;
    private bool playerInSight = false;


    public bool PlayerInSight { get => playerInSight; set => playerInSight = value; }

    protected override void Start()
    {
        TimeController = Resources.FindObjectsOfTypeAll<DayNightCycleBehaviour>()[0];
        ScaleOnSpawn();
        targetObj = GameObject.FindWithTag("Player");
        target = targetObj.transform;
        Player = targetObj.GetComponent(typeof(PlayerController)) as PlayerController;

        base.Start();
    }

    protected override void Update()
    {
        playerInSight = CheckVisibility(); //check if player is in range of monster

        if(playerInSight && !playerSeen) //player seen for the first time, change state to attacking and increase the vision range
        {
            playerSeen = true;
            monsterState = MonsterBehaviour.Attacking;
            baseSearch *= searchBoost;
        }
        else if(playerSeen && !playerInSight) //player has been seen but is no longer in vision range
        {
            monsterState = MonsterBehaviour.Searching;
        }
        else if(playerSeen && playerInSight) //player is seen and has been seen before
        {
            monsterState = MonsterBehaviour.Attacking;
        }

        switch(monsterState)
        {
            case MonsterBehaviour.Attacking:
                CombatTarget();
                break;
            case MonsterBehaviour.Searching:
                //wander();
                this.Direction = Vector2.zero;
                break;
            case MonsterBehaviour.Wandering:
                //wander();
                break;
        }

        base.Update();
    }

    //returns true if a straight line can be drawn between this object and the target, givin the target is within the visible arc
    private bool CheckVisibility()
    {
        //find Direction of target
        Vector3 directionToTarget = target.position - transform.position;

        //find degrees from forward Direction
        float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        float distanceToTarget = directionToTarget.magnitude;

        bool withinArc = degreesToTarget < (visionArc);

        float rayDistance = Mathf.Min(baseSearch, distanceToTarget);

        //create a ray that goes from current location to Direction
        Ray2D ray = new Ray2D(transform.position, directionToTarget);

        //store info on the hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, baseSearch);

        //fire raycast, does it hit anything?
        if(hit)
        {

            if(hit.collider.transform == target)
            {
                //then we can see the target
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }

        return playerInSight;
    }
    private void ScaleOnSpawn()
    {
        //increase 
        enemyDamage = Mathf.FloorToInt(baseDmg * ((TimeController.TotalDayCount + scalingByDay) / scalingByDay));

        this.MaxHP = Mathf.FloorToInt(baseHP * ((TimeController.TotalDayCount + scalingByDay) / scalingByDay));

        Debug.Log("dmg" + Mathf.FloorToInt(baseDmg * ((TimeController.TotalDayCount + scalingByDay) / scalingByDay)));
        Debug.Log("HP" + Mathf.FloorToInt(baseHP * ((TimeController.TotalDayCount + scalingByDay) / scalingByDay)));
    }
    /**
     * AI action logic \/
     */

    // handles combat if AI is in Attacking behaviour
    private void CombatTarget()
    {
        // value of AI's X value relative to the Target, positive is right of the target, negative is left of the target
        float RelativeX = Mathf.Floor(transform.position.x - target.transform.position.x);

        // value of AI's Y value relative to the Target, positive is above the target, negative is below the target
        float RelativeY = Mathf.Floor(transform.position.y - target.transform.position.y);

        Direction = Vector2.zero;

        if(RelativeX != 0) //check if AI is on same x level as target
        {
            if(RelativeX < 0) // check if the AI is on the left or right of the target. 
            {
                // target is to the left of the target, move right
                Direction += Vector2.right;
            }
            else
            {
                //target is to the right of the target, move left
                Direction += Vector2.left;
            }
        }
        else if(RelativeY != 0) // check if target is on the same Y level
        {
            if(RelativeY < 0)  // check if AI is above or below the target
            {
                //AI is below target, move up
                Direction += Vector2.up;
            }
            else
            {
                // AI is above target, move down
                Direction += Vector2.down;
            }
        }
        else
        {
            if(IsAttacking)
            {
                AttackCounter -= Time.deltaTime;
                if(AttackCounter <= 0)
                {
                    StopAttack();
                    AttackCounter = 3f;
                }
            }
            else
            {
                Attack();
            }
        }
    }

    public override void Attack()
    {

        if(timeStamp <= -1)
        {
            timeStamp = Time.time;
        }

        if(Time.time >= timeStamp)
        {
            base.Attack();
            timeStamp = Time.time + attackCooldownInSeconds;
            Player.TakeDamage(baseDmg);
        }
    }

    //generates random movement for the AI
    private void Wander()
    {
        if(wanderMovement == maxMovement || wanderMovement == 0)
        {
            wanderdirection = Random.Range(1, 4);
            wanderMovement = 0;
        }
        switch(wanderdirection)
        {
            case 1:
                Direction = Vector2.up;
                wanderMovement++;
                break;
            case 2:
                Direction = Vector2.down;
                break;
            case 3:
                Direction = Vector2.right;
                break;
            case 4:
                Direction = Vector2.left;
                break;
        }
    }
}
