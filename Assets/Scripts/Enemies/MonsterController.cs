using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterController : EntityController
{
    private GameObject targetObj = null; // get player object
    private Transform target = null; //get player transform
    private PlayerController Player = null;
    [SerializeField]
    private int damage;
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
    private int maxMovement;
    [SerializeField]
    private int damageDelt;
    [SerializeField]
    private float attackCooldownInSeconds = 1;
    private float timeStamp = -1; //timeStamp starts negative so we can initially set the timeStamp


    private MonsterBehaviour monsterState = MonsterBehaviour.Wandering;
    private int wanderMovement = 0;
    private int wanderdirection;
    private bool playerSeen = false;

    private bool playerInSight = false;
    public bool PlayerInSight { get => playerInSight; set => playerInSight = value; }

    protected override void Start()
    {
        targetObj = GameObject.FindWithTag("Player");
        target = targetObj.transform;
        Player = targetObj.GetComponent(typeof(PlayerController)) as PlayerController;

        base.Start();
    }

    protected override void Update()
    {
        playerInSight = checkVisibility(); //check if player is in range of monster

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
                combatTarget();
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
    private bool checkVisibility()
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

    // handles combat if AI is in Attacking behaviour
    private void combatTarget()
    {
        // value of AI's X value relative to the Target, positive is right of the target, negative is left of the target
        float RelativeX = Mathf.Floor(this.transform.position.x - target.transform.position.x);

        // value of AI's Y value relative to the Target, positive is above the target, negative is below the target
        float RelativeY = Mathf.Floor(this.transform.position.y - target.transform.position.y);

        this.Direction = Vector2.zero;

        if(RelativeX != 0) //check if AI is on same x level as target
        {
            if(RelativeX < 0) // check if the AI is on the left or right of the target. 
            {
                // target is to the left of the target, move right
                this.Direction += Vector2.right;
            }
            else
            {
                //target is to the right of the target, move left
                this.Direction += Vector2.left;
            }
        }
        else if(RelativeY != 0) // check if target is on the same Y level
        {
            if(RelativeY < 0)  // check if AI is above or below the target
            {
                //AI is below target, move up
                this.Direction += Vector2.up;
            }
            else
            {
                // AI is above target, move down
                this.Direction += Vector2.down;
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
                    AttackCounter = 5f;
                }
            }
            else
            {
                attack();
            }
        }
    }

    private void attack()
    {
        
        if(timeStamp <= -1)
        {
            timeStamp = Time.time;
        }

        if(Time.time >= timeStamp)
        {
            base.Attack();
            timeStamp = Time.time + attackCooldownInSeconds;
            Player.TakeDamage(damageDelt);
        }
    }

    //generates random movement for the AI
    private void wander()
    {
        if(wanderMovement == maxMovement || wanderMovement == 0)
        {
            wanderdirection = Random.Range(1, 4);
            wanderMovement = 0;
        }
        switch(wanderdirection)
        {
            case 1:
                this.Direction = Vector2.up;
                wanderMovement++;
                break;
            case 2:
                this.Direction = Vector2.down;
                break;
            case 3:
                this.Direction = Vector2.right;
                break;
            case 4:
                this.Direction = Vector2.left;
                break;
        }
    }
}
