using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[ SerializeField ] Vector2 offset;
	[ SerializeField ]	float smoothSpeed = 0.125f;
	
	Transform target;

	void Start() {
		target = GameObject.FindObjectOfType<Player> ().transform;
	}

	void LateUpdate() {
		if( target ) {
			Vector2 desiredPosition = new Vector2( target.position.x, target.position.y ) + offset;
			Vector2 smoothedPosition = Vector2.Lerp( transform.position, desiredPosition, smoothSpeed );
			transform.position = new Vector3( smoothedPosition.x, smoothedPosition.y, transform.position.z );
		}
	}
}
