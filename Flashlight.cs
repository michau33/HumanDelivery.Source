using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

#region Variables
    [Header("Settings")]
	[ SerializeField ] float rotationSpeed = 5f;
	[ SerializeField ] float maxAngle = 25f;
	[ SerializeField ] float minAngle = -25f;

    [Header("Visuals & Audio")]
	[ SerializeField ] Color defaultColor;
	[ SerializeField ] Color dangerColor;
	[ SerializeField ] AudioClip alarm;

	public float killAfter = 1.5f;
	public bool canRotate = true;

	bool direction = false;
	bool inDanger = false;

	Quaternion maxAngleQuaternion;
	Quaternion minAngleQuaternion;
    //Vector3 defaultScale;

    SpriteRenderer _sprite;
    GameObject _target;
	AudioSource _audio;

    float time = 0f;
#endregion

    void Start() { 
		//defaultScale = transform.localScale;
        _sprite = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
	}

	void Update () {
        if (canRotate)
            RotateFlashlight();

        HandleDanger();
	}

    void RotateFlashlight()
    {
        Quaternion maxAngleQuaternion = Quaternion.AngleAxis(maxAngle, Vector3.forward);
        Quaternion minAngleQuaternion = Quaternion.AngleAxis(minAngle, Vector3.forward);

        if( transform.rotation != maxAngleQuaternion || transform.rotation != minAngleQuaternion ) {
            transform.rotation = Quaternion.RotateTowards( transform.rotation, direction  
                                 ? minAngleQuaternion 
                                 : maxAngleQuaternion
                                 , rotationSpeed * Time.deltaTime );
        } else {
            direction = !direction;
        }
    }

    void HandleDanger()
    {
        if( inDanger)
        {
            time += Time.deltaTime;
            if (time >= killAfter)
            {
                _target.SendMessage("Die");
            }

            _audio.PlayOneShot(alarm);
            _sprite.color = dangerColor;

            // Do you really need it ?
            // transform.localScale = new Vector3( transform.localScale.x, 5f, transform.localScale.z );
        }
        else {
            _sprite.color = defaultColor;
            // transform.localScale = defaultScale;
        }
        
    }

#region Triggers
    void OnTriggerEnter2D( Collider2D other ) {
		if( other.tag == "Player" ) {
			inDanger = true;
			_target = other.gameObject;
		}
	}
	
	void OnTriggerExit2D( Collider2D other ) {
		if( other.tag == "Player" ) {
			inDanger = false;
			_target = null;
		}
	}
#endregion
}
