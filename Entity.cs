using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    [SerializeField] Rigidbody2D _rb2D;
    
    bool canMove = true;

    public virtual void Die() {
        Destroy(this.gameObject);
    }
    
	void OnCollisionEnter2D( Collision2D collisionInfo ) {
        // when you drop entity into the ground it can start moving
        if ( collisionInfo.collider.tag == "Ground" ) {
			canMove = true;
		}
	}
}

[ System.Serializable ]
public class EntityType {
	public Race race;
	public Sprite sprite;
}
