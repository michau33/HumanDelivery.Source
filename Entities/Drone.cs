using UnityEngine;

public class Drone : MonoBehaviour {

	[Header("Patrolling")]
	[SerializeField] PathManager pathManager;
	[SerializeField] float droneVelocity = 5f;
	
	private StateMachine stateMachine = new StateMachine();

    void Start() {
		stateMachine.ChangeState( new StatePatrol( this.transform, pathManager.waypoints, droneVelocity ) );
	}

	void Update() {
		stateMachine.ExecuteState();
	}	
}
