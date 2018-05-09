using UnityEngine;

public class StateMachine {

	private IState currentState;
	private IState previousState;

	public void ChangeState( IState newState ) {
		if( this.currentState != null ) {
			this.currentState.Exit();
		}

		this.previousState = this.currentState;
		this.currentState = newState;
		this.currentState.Enter();
	}

	// if the current state is not null executes it
	public void ExecuteState() {
		var runningState = this.currentState;
		if( runningState != null ) {
			runningState.Execute ();
		}
	}

	// we switch to what we were doing previously if previous state exists.
	public void SwitchToPreviousState() {
		if (previousState != null) {
			this.currentState.Exit ();
			this.currentState = this.previousState;
			this.currentState.Enter ();
		}
	}
}
