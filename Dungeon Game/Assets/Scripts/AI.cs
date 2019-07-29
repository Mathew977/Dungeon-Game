using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class AI : Character {

    [SerializeField]
    private float shotRange = 1.0f; //Distance from which AI can shoot target
    [SerializeField]
    private Transform target; //The target to attack i.e. player
    [SerializeField]
    private GameObject waypoints; //The game object containing the waypoints for this AI to visit

    private AICharacterControl aiCharacter;
    private WaypointCollection waypointCollection;

    [SerializeField]
    protected DeathTypes deathTypes; //Indicates character's death type

    Animator animator1; //Will store a reference to the animator component attached to the game object
    // Use this for initialization
    protected override void Start ()
    {
        //Call parent's start method first
        base.Start();

        animator1 = GetComponent<Animator>();

        aiCharacter = GetComponent<AICharacterControl>();

        if (waypoints != null)
        {
            waypointCollection = waypoints.GetComponent<WaypointCollection>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTarget();

        bool attack = CanAttack();
        if (attack == true)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        aiCharacter.isAttacking = isAttacking;

    }

    private void UpdateTarget()
    {
        if (aiCharacter == null)
        {
            return;
        }
        
            float stoppingDistance = 0;

            Transform nextTarget;

            if (aiCharacter.target != null)
            {
                nextTarget = waypointCollection.GetNextTarget(aiCharacter.target, aiCharacter.agent.remainingDistance, out stoppingDistance);
            }
            else
            {
                nextTarget = waypointCollection.GetNextTarget(null, 0, out stoppingDistance);
            }

            if (nextTarget != null)
            {
            if (nextTarget.gameObject.tag == "Player")
                aiCharacter.isRunning = true;
            else
                aiCharacter.isRunning = false;

            aiCharacter.SetTarget(nextTarget);
            aiCharacter.agent.stoppingDistance = stoppingDistance;
            }
    }

    private bool CanAttack()
    {
        if (target == null)
            return false;
        RaycastHit hit;

        //Get the distance offset between the AI character and the target player
        Vector3 distanceOffset = (target.position - transform.position);

        //Use the square magnitude to improve performance
        float distanceToPlayer = distanceOffset.sqrMagnitude;

        //Cast ray shotrange meters in shot direction, to see if anything will block the rocket
        if (Physics.Raycast(transform.position, distanceOffset.normalized, out hit, shotRange))
        {
            //Check if it is going to hit the player. If yes, okay to shoot, return true
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        //Else player not in range, return false
        return false;
    }

    public void DieEnemy()
    {
        //Destroy(this.gameObject);
        animator1.SetBool("Dead", true);
        animator1.SetInteger("DeathType", (int)deathType);

        PlayerManager.Instance.Score++ ;
        Destroy(this.gameObject, dyingTime);
    }

    public void SetWaypoints(GameObject aiWaypoints)
    {
        waypoints = aiWaypoints;
        //Get the waypointCollection scripts attached to the waypoints game object
    }
}
