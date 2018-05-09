using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
#region Variables

	[ SerializeField ] ControlType controlType;
	[ SerializeField ] float maxRotation = 15f;
	float horizontal, 
		  vertical, 
		  shipRotation = 0;

	Player player;
	bool canMove = true;
	public bool CanMove { get { return canMove; } set { canMove = value; } }

#endregion

	void Awake() {
		player = GetComponent<Player>();
	}

	public void HandleMovementInput() {
		horizontal = AndroidJoystick.Horizontal;
        vertical = AndroidJoystick.Vertical;

		if( canMove ) {
			Move();
		}
	}

	void Move() {
		transform.position += new Vector3( horizontal * Time.deltaTime * player.PlayerStats.HorizontalSpeed, vertical * Time.deltaTime * player.PlayerStats.VerticalSpeed  );
		shipRotation = -horizontal * player.PlayerStats.RotationSpeed * maxRotation; 
		transform.GetChild( 0 ).transform.eulerAngles = new Vector3( 0f, 0f, shipRotation );
	}

	public void HandleCollectorInput() {
		if( player.Collector ) {
			if (InputManager.CollectButtonDown( this.controlType ) && !player.Collector.IsOnCooldown ) {  
				Debug.Log("I should start collecting");
				// TODO FIX IT
				//AudioUtility.GetSource( this.gameObject, 256 ).PlayOneShot( collectAudio ); // Get audioSource with priority of 256 to avoid conflits   
				player.Collector.StartCollecting();
			}
        	if (InputManager.CollectButtonUp( this.controlType)  && player.Collector.IsCollecting ) {
           		player.Collector.StopCollecting();
        	}
		} else {
			Debug.LogWarning("Error in HandleCollectorInput method. You need collector in the scene");
		}
		
	}
}
