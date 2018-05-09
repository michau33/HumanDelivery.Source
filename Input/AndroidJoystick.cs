using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AndroidJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	Image backgroundImage;
	Image joystickImage;
	Vector3 inputVector;

	static float horizontal;
	public static float Horizontal {
		get {
			return horizontal;
		}
	}
	static float vertical;
	public static float Vertical {
		get {
			return vertical;
		}
	}

	void Awake() {
		backgroundImage = this.GetComponent<Image>();
		joystickImage = this.transform.GetChild(0).GetComponent<Image>();
	}

	void Update() {
		horizontal = inputVector.x;
		vertical = inputVector.z; 
	}

	public virtual void OnDrag( PointerEventData e ) {
		Vector2 position = Vector2.zero;
		if( RectTransformUtility.ScreenPointToLocalPointInRectangle( 
			backgroundImage.rectTransform, 
			e.position,
			e.pressEventCamera,
			out position 
		) ) {
			position.x = ( position.x / backgroundImage.rectTransform.sizeDelta.x );
			position.y = ( position.y / backgroundImage.rectTransform.sizeDelta.y );


			inputVector = new Vector3( position.x * 2 + 1, 0f, position.y * 2 - 1 );
			inputVector = inputVector.magnitude > 1f ? inputVector.normalized : inputVector;

			joystickImage.rectTransform.anchoredPosition = new Vector3 (
				inputVector.x * ( backgroundImage.rectTransform.sizeDelta.x / 6f ),
				inputVector.z * ( backgroundImage.rectTransform.sizeDelta.y / 6f )
			);
		}
	}

	public virtual void OnPointerDown( PointerEventData e ) {
		OnDrag( e );
	}

	public virtual void OnPointerUp( PointerEventData e ) {
		inputVector = Vector2.zero;
		joystickImage.rectTransform.anchoredPosition = inputVector;
	}
}
