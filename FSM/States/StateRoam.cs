using UnityEngine;
using System.Collections;

public class StateRoam : IState {
	private GameObject owner;
	private float movementSpeed;
	private float changeDirectionAfterMin;
	private float changeDirectionAfterMax;
	private float time;
	private bool facingRight;

	public StateRoam( GameObject owner, float movementSpeed, float changeDirectionAfterMin, float changeDirectionAfterMax ) {
		this.owner = owner;
		this.movementSpeed = movementSpeed;
		this.changeDirectionAfterMin = changeDirectionAfterMin;
		this.changeDirectionAfterMax = changeDirectionAfterMax;
	}

	public void Enter() {
		facingRight = true;
		time = 0f;
	}

	public void Execute() {
		time += Time.deltaTime;

		this.owner.transform.Translate( GetDirection() * this.movementSpeed * Time.deltaTime );

		if( time >= Random.Range( changeDirectionAfterMin, changeDirectionAfterMax ) ) {
			facingRight = !facingRight;
			time = 0f;
		}
	}

	public void Exit() {

	}

	private Vector2 GetDirection() {
		return facingRight ? Vector2.right : Vector2.left;
	}
}
