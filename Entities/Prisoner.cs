using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour, ICollectable {
#region Variables
	[SerializeField] EntityType entityType;
	[SerializeField] PrisonerStats stats;

	private StateMachine stateMachine = new StateMachine ();
    private SpriteRenderer spriteRenderer;
	private Rigidbody2D rb2D;
	private bool canMove = true;

	private bool isBeingCollected = false;
	public bool IsBeingCollected { get { return isBeingCollected; } set { isBeingCollected = value; } }
	public EntityType EntityType { get { return entityType; } protected set {} }
#endregion

	void Awake() {
		rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

    void Start() {
        spriteRenderer.sprite = entityType.sprite;

		stateMachine.ChangeState (new StateRoam( this.gameObject, stats.MovementSpeed, stats.ChangeDirectionMinTime, stats.ChangeDirectionMaxTime ) );
    }

	void Update() {
		if (canMove) {
			stateMachine.ExecuteState ();
		}
	}

	public void OnCollectBegin() {
		canMove = false;
		rb2D.gravityScale = 0f;
	}

	public void OnCollectEnd() {
		rb2D.gravityScale = 1f;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if( other.collider.CompareTag("Ground") ) {
			canMove = true;
		}
	}

	void Destroy() {
		Destroy( this.gameObject );
	}
}
