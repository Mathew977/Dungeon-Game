using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public GameObject waypoints;
        public WaypointCollection waypointsCollection;

        private float m_CurrentSpeed = 3.0f;

        public bool isRunning { get; set; }

        public bool isAttacking { get; set; }

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, m_CurrentSpeed, false, false, isAttacking);
            else
                character.Move(agent.desiredVelocity, 0f, false, false, isAttacking);
        }

        public void SetTarget(Transform aiTarget)
        {
            this.target = aiTarget;
        }

        public void SetWaypoints(GameObject aiWaypoints)
        {
            waypoints = aiWaypoints;

            //Get the waypointcollection scripts attached to the waypoints game object
            waypointsCollection = waypoints.GetComponent<WaypointCollection>();

            //Set the agent to the AI prefab's transform this script is attached to
            waypointsCollection.SetAgent(this.transform);
        }
    }
}
