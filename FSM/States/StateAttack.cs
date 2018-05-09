using UnityEngine;

public class StateAttack : IState {

	private Transform target;
	private Component owner;
	private float killAfter;
	private float time;

	public StateAttack( Component owner, Transform target, float killAfter ) {
		this.target = target;
		this.owner = owner;
		this.killAfter = killAfter;
	}

	public void Enter() {
		time = 0f;
		EventManager.instance.PostNotification(this.owner, "PlayerDetected");
	}

	public void Execute() {
		if (this.target) {
			time += Time.deltaTime;

			if (time >= killAfter) {
				Debug.Log ("AAA ZABIŁEM CIE");
			}
		}
	}

	public void Exit() {
		EventManager.instance.PostNotification(this.owner, "PlayerLost");
	}
}
