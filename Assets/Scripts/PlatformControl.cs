using UnityEngine;
using System;
using System.Collections;

public class PlatformControl : MonoBehaviour {

	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;

	public float MoveSpeed;

	//Non Public Variable
	float waypointDistance;
	int fromWaypoint;

	// Use this for initialization
	 void Awake () {
		globalWaypoints = new Vector3[localWaypoints.Length];

		for (int i =0; i < localWaypoints.Length; i++) {
			globalWaypoints[i] = localWaypoints[i] + transform.position;
			//print(globalWaypoints[i].ToString());
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 Velocity = MovePlatform();
		transform.Translate (Velocity);
	}

	Vector3 MovePlatform ()
	{
		//Set Waypoint To Next Waypoint
		int toWaypoint = fromWaypoint + 1;	
		float dis = Vector3.Distance (globalWaypoints [fromWaypoint], globalWaypoints [toWaypoint]);
		waypointDistance += Time.deltaTime * MoveSpeed / dis;

		Vector3 newWayPos = Vector3.Lerp (globalWaypoints [fromWaypoint], globalWaypoints [toWaypoint], waypointDistance);

		//If Platformer Has Arrive To The Next Waypoint
		if (waypointDistance >= 1) {
			//Set it Back to 0
			waypointDistance = 0;
			//Move To The Next Waypoints
			fromWaypoint++;

			if(fromWaypoint >= globalWaypoints.Length - 1) {
				fromWaypoint = 0;
				Array.Reverse(globalWaypoints);
			}
		}
		return newWayPos - transform.position;
	}
		
	void OnDrawGizmos ()
	{
		if (localWaypoints != null) {
			Gizmos.color = Color.cyan;
			float size = .2f;

			for(int i = 0; i < localWaypoints.Length; i++) {
				//Set Waypoints Position to Global Position (My Position) If The Game Is Played....
				Vector3 WaypointPos = (Application.isPlaying)? globalWaypoints[i] : localWaypoints[i] + transform.position;

				//Draw Cross Line
				Gizmos.DrawLine(WaypointPos - Vector3.up * size, WaypointPos + Vector3.up * size);
				Gizmos.DrawLine(WaypointPos - Vector3.right * size, WaypointPos + Vector3.right * size);
			}
		}
	}
}
