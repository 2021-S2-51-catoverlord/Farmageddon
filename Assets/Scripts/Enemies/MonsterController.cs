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
    private bool debugToggle;
    [SerializeField]
    private float MaxSearch = 10f; // base range of the monster
    [SerializeField]
    [Range(0f, 360f)]
    private float visionArc = 45f; //angle of vison on a enemy in degrees, max 360f
    [SerializeField]
    [Range(1, 1000)]
    private int searchRange = 5; //how far the range of the monster becomes once they have seen the target

    private MonsterBehaviour monsterState = MonsterBehaviour.Wandering;

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

        if (playerInSight && !playerSeen) //player seen for the first time, change state to attacking and increase the vision range
        {
            playerSeen = true;
            monsterState = MonsterBehaviour.Attacking;
            MaxSearch = MaxSearch * searchRange;
        }
        else if (playerSeen && !playerInSight) //player has been seen but is no longer in vision range
        {
            monsterState = MonsterBehaviour.Searching;

        }
        if (debugToggle)
        {
            Debug.Log(monsterState);
        }
        switch (monsterState)
        {
            case MonsterBehaviour.Attacking:
                combatTarget();
                break;
            case MonsterBehaviour.Searching:
                //wander();
                break;
            case MonsterBehaviour.Wandering:
               // wander();
                break;
        }

        base.Update();

    }

    //returns true if a straight line can be drawn between this object and the target, givin the target is within the visible arc
    private bool checkVisibility()
    {
        //find direction of target
        Vector3 directionToTarget = target.position - transform.position;

        //find degrees from forward direction
        float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);

        float distanceToTarget = directionToTarget.magnitude;

        bool withinArc = degreesToTarget < (visionArc);

        float rayDistance = Mathf.Min(MaxSearch, distanceToTarget);

        //create a ray that goes from current location to direction
        Ray2D ray = new Ray2D(transform.position, directionToTarget);

        //store info on the hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget,MaxSearch);

        //fire raycast, does it hit anything?
        if (debugToggle)
        {
            Debug.DrawRay(transform.position, directionToTarget);
        }
        
        if (hit)
        {
            if (debugToggle)
            {
                Debug.Log("raycast hit");
                Debug.Log(hit.transform.position.ToString());
            }
            if (hit.collider.transform == target)
            {
                //then we can see the target
                playerInSight = true;
                if (debugToggle)
                {
                    Debug.Log("player in sight");
                }
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

        this.direction = Vector2.zero;

        if (RelativeX != 0) //check if AI is on same x level as target
        {
            if (RelativeX < 0) // check if the AI is on the left or right of the target. 
            {
                // target is to the left of the target, move right
                this.direction += Vector2.right;
            }
            else
            {
                //target is to the right of the target, move left
                this.direction += Vector2.left;
            }
        }
        else if (RelativeY != 0) // check if target is on the same Y level
        {
            if (RelativeY < 0)  // check if AI is above or below the target
            {
                //AI is below target, move up
                this.direction += Vector2.up;
            }
            else
            {
                // AI is above target, move down
                this.direction += Vector2.down;
            }
        }
    }

    //generates random movement for the AI
    private void wander()
    {
        this.direction = Vector2.zero;
        switch (Random.Range(1,4))
        {
            case 1:
                this.direction = Vector2.up;
                break;
            case 2:
                this.direction = Vector2.down;
                break;
            case 3:
                this.direction = Vector2.right;
                break;
            case 4:
                this.direction = Vector2.left;
                break;
        }

    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log("Collision");
        if (collisionInfo.gameObject == targetObj)
        {
            Player.TakeDamage(damage);
        }
    }

}