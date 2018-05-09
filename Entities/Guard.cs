using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {
	[SerializeField] PathManager pathManager;
	[SerializeField] GuardStats stats;
	[SerializeField] GameObject dangerIndicator;
	
	private StateMachine stateMachine = new StateMachine();
	private Transform target;
	private bool isPlayerSpotted;

	void Awake() {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start() {
		stateMachine.ChangeState( new StatePatrol( this.transform , pathManager.waypoints, stats.MovementSpeed ) );

		// this event is triggered in attack state ( when player is in range )
		EventManager.instance.AddListener(this, "PlayerDetected");
		// this event is triggered in when exiting attack state
		EventManager.instance.AddListener(this, "PlayerLost");
	}
	
	void Update() {
		stateMachine.ExecuteState ();

		if ( ( Vector2.Distance ( this.transform.position, target.position ) <= stats.PlayerSpottedDistance ) ) {
			if ( isPlayerSpotted == false ) {
				isPlayerSpotted = true;

				stateMachine.ChangeState ( new StateAttack ( this, target, stats.CatchAfter ) );
			}
		} 

		if ( ( Vector2.Distance ( this.transform.position, target.position ) > stats.PlayerSpottedDistance ) ) {
			if( isPlayerSpotted == true ) {
				isPlayerSpotted = false;
				stateMachine.SwitchToPreviousState();
			}
		}
	}

	void PlayerDetected() {
		dangerIndicator.SetActive( true );
	}

	void PlayerLost() {
		dangerIndicator.SetActive( false );
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( this.transform.position, stats.PlayerSpottedDistance );
	}
}
