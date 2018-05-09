using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class StatePatrol : IState {

	Transform objToMove = null;
	List<Waypoint> wayPoints = new List<Waypoint>();
	float speed;
	int wayPointID;
	float reachDistance = 1.0f;
	bool isGoingForward = true;
	bool isGoingBackward = false;

	public StatePatrol( Transform objToMove, List<Waypoint> wayPoints, float speed ) {
		wayPointID = 0;

		this.objToMove = objToMove;
		this.wayPoints = wayPoints;
		this.speed = speed;
	}
	public void Enter() {

	}

	public void Execute() {
		// creating this vector because I don't want to move object in y axis
		Vector3 movementVector = new Vector3( 
			this.wayPoints[ wayPointID ].position.x,
			this.wayPoints[ wayPointID ].position.y,
			this.objToMove.position.z
		);
			
		// simply changing object position from current position to waypoint position with certain speed
		this.objToMove.position = Vector3.MoveTowards(
			this.objToMove.position,
			movementVector,
			this.speed * Time.deltaTime
		);

		/*
		// calculate rotation to make moving object constantly look at the waypoint
		Quaternion lookRotation = Quaternion.LookRotation( movementVector - this.objToMove.position );
		// interpolating from a to b by rotation speed
		this.objToMove.rotation = Quaternion.Slerp( this.objToMove.rotation, lookRotation, this.rotationSpeed * Time.deltaTime );
		 */

		// distance between object which should move and its current waypoint
		float distance = Vector3.Distance( this.wayPoints[ wayPointID ].position, this.objToMove.position );

		if( distance <= reachDistance ) {
			if( isGoingForward ) {
				wayPointID++;
				if( wayPointID >= this.wayPoints.Count ) {
					isGoingForward = false;
					isGoingBackward = true;
				}
			}
			if( isGoingBackward ) {
				wayPointID--;
				if( wayPointID <= 0 && isGoingBackward ) {
					isGoingForward = true;
					isGoingBackward = false;
				}
			}	
		}
	}

	public void Exit() {
		
	}
}
