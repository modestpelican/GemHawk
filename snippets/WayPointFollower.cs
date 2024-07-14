using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < .1f)    // distance btw platform and waypoint
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        // giving it a nudge*
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed); 
    }
}



// *
// speed: no of game units to move per sec
// but update: called every frame [2 game units per frame - vv fast]
// Time.deltaTime : time passed since the last frame
// frame rate inc, time dec x frame rate dec, time inc
// thus making it frame rate independant
