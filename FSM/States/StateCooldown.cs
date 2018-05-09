using UnityEngine;

public class StateCooldown : IState {

	private float time;
	private float cooldownTime;
	private bool isOnCooldown;
	public StateCooldown( float cooldownTime, bool isOnCooldown ) {
		this.isOnCooldown = isOnCooldown;
		this.cooldownTime = cooldownTime;
	}

	public void Enter() {
		Debug.Log("Entering cooldown");
		time = 0f;
	}

	public void Execute() {
		if( isOnCooldown ) {
			time += Time.deltaTime;

			if( time >= cooldownTime ) {
				this.isOnCooldown = false;
				time = 0f;
			}
		}
	}
	
	public void Exit() {
		Debug.Log("Cooldown has ended");
		isOnCooldown = false;
	}
}