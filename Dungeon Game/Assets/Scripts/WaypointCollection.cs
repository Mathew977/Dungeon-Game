using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class WaypointCollection : MonoBehaviour
{
    [SerializeField]
    private bool isRandomRoute;

    [SerializeField]
    private bool isLooping;

    [SerializeField]
    private bool showGizmos;

    [SerializeField]
    private Transform agent;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float targetRange = 5.0f;

    [SerializeField]
    private float targetStoppingDistance = 3.0f;

    [SerializeField]
    private Color GizmosColour = Color.red;

    [SerializeField]
    private int thinkingTime = 30;

    //[SerializeField]
    private float waypointRadius = 1.0f;

    private int numWaypoints; // the number of waypoints
    private bool waypointsRouteFinished; // indicate if the AI has completed the route
    private int currentIndex; // the current index of the next way point the AI is travelling towards

    private List<Transform> waypoints; // Stores the list of all way points available
    private Stack<int> visitedWaypoints; // Keeps track of the visited way points indexes, including the current one travelling towards
    private bool thinking; // indicate if AI is currently thinking/chatting/etc.
    private Timer thinkingTimer; // Timer to act as a stopwatch to keep track of the thinking time and stop when up


    // Use this for initialization
    void Start()
    {
        InitialiseWaypoints();
        InitialiseThinkingTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform GetNextTarget(Transform currentTarget, float remainingDistance, out float stoppingDistance)
    {
        stoppingDistance = 0f;
        if (target == null || agent == null) return null;

        // Calculate AI character's current distance from the target's current position
        // Use square magnitude to improve performance
        float distanceToPlayer = (target.position - agent.transform.position).sqrMagnitude;


        if (distanceToPlayer <= targetRange * targetRange)
        {
            if (currentTarget != target)
            {
                thinkingTimer.Enabled = thinking = false;
                stoppingDistance = targetStoppingDistance;
                return target;
            }
        }
        else
        {
            if (currentTarget == target)
            {
                return GetCurrentWaypoint();
            }
            else if (remainingDistance <= 0f)
            {
                if (waypointsRouteFinished)
                {
                    if (isLooping && !thinking)
                        thinkingTimer.Enabled = thinking = true;
                    return null;
                }
                else
                {
                    return GetNextWaypoint();
                }
            }
        }

        return null;
    }

    private void InitialiseWaypoints()
    {
        waypoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
            Waypoint waypoint = child.GetComponent<Waypoint>();
            waypoint.ShowGizmos = showGizmos;
            waypoint.MarkerRadius = waypointRadius;
        }
        numWaypoints = waypoints.Count;

        if (numWaypoints > 0)
        {
            currentIndex = 0;
            visitedWaypoints = new Stack<int>();
            visitedWaypoints.Push(currentIndex);
        }
    }

    private void InitialiseThinkingTimer()
    {
        // Create a timer and set a two second interval.
        thinkingTimer = new System.Timers.Timer();
        thinkingTimer.Interval = thinkingTime * 1000;

        // Hook up the Elapsed event for the timer. 
        thinkingTimer.Elapsed += ThinkingTimer_Elapsed;
    }

    private void ThinkingTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (thinking)
        {
            //Debug.Log("Stopped thinking: " + DateTime.Now); // + "  (" + thinkingDuration + ")");
            thinkingTimer.Enabled = thinking = waypointsRouteFinished = false;
            visitedWaypoints.Clear();
        }
    }


    private int GetNextIndex(int lastIndex)
    {
        return (lastIndex + 1) % numWaypoints;

        //the above line does exactly the same as the following
        //if (lastIndex < numWaypoints - 1)
        //{
        //    return lastIndex + 1;
        //}
        //else
        //{
        //    return 0;
        //}
    }

    private int GetNextRandomIndex(int lastIndex)
    {
        // Generate a random number. Random returns a float, you need to convert to integer as indices can only be ints.
        int randomIndex = UnityEngine.Random.Range(0, numWaypoints);

        // If number of waypoints is greater than one, make sure random number generated is not the same as the last one or agent will be stuck at same spot
        // Keep trying till you get a different one.
        if (numWaypoints > 1)
        {
            // check that it has not just generated the same index as the last one
            // if yes, continue trying for a different random index
            while (lastIndex == randomIndex)
            {
                randomIndex = UnityEngine.Random.Range(0, numWaypoints);
            }
        }

        return randomIndex;
    }

    public Transform GetCurrentWaypoint()
    {
        return waypoints[currentIndex].transform;
    }

    private Transform GetNextWaypoint()
    {
        if (waypointsRouteFinished) return null; // GetCurrentWaypoint(); && !isLooping

        if (isRandomRoute)
        {
            currentIndex = GetNextRandomIndex(currentIndex);
        }
        else
        {
            currentIndex = GetNextIndex(currentIndex);
        }

        // Add new waypoint to visit
        visitedWaypoints.Push(currentIndex);

        // If not looping, check if this was the last way point to visit
        waypointsRouteFinished = numWaypoints == visitedWaypoints.Count;

        return waypoints[currentIndex];
    }


    private void OnDrawGizmos()
    {
        if (waypoints == null)
            InitialiseWaypoints();

        if (showGizmos)
        {
            Gizmos.color = GizmosColour;
            for (int i = 0; i < numWaypoints; i++)
            {
                if (isRandomRoute)
                {
                    for (int j = 0; j < numWaypoints; j++)
                    {
                        Gizmos.DrawLine(waypoints[i].position, waypoints[j].position);
                    }
                }
                else
                {

                    if (i < numWaypoints - 1)
                    {
                        Gizmos.DrawLine(waypoints[i].position, waypoints[GetNextIndex(i)].position);
                    }
                    else
                    {
                        Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                    }
                }
            }
        }
    }
    public void SetAgent(Transform aiAgent)
    {
        agent = aiAgent;
    }
}