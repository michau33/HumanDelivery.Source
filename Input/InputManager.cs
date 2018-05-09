using UnityEngine;

public static class InputManager {
	// isn't horizontal same for both controllers ????
	public static float Horizontal( ControlType controlType = ControlType.Keyboard ) {
        return Input.GetAxis("Horizontal");
	}

	public static float Vertical( ControlType controlType = ControlType.Keyboard ) {
        return Input.GetAxis("Vertical");
	}

	public static bool CollectButtonDown( ControlType controlType = ControlType.Keyboard ) {
		switch( controlType ) {
			case ControlType.Keyboard:
                return Input.GetKeyDown(KeyCode.Space);
			case ControlType.Gamepad:
                return Input.GetButtonDown("Gamepad_A");
			default:
                return Input.GetKeyDown(KeyCode.Space);
        }
	}

    public static bool CollectButtonUp( ControlType controlType = ControlType.Keyboard ) {
		switch( controlType ) {
			case ControlType.Keyboard:
                return Input.GetKeyUp(KeyCode.Space);
			case ControlType.Gamepad:
                return Input.GetButtonUp("Gamepad_A");
			default:
                return Input.GetKeyUp(KeyCode.Space);
        }
	}

	public static bool CollectButton( ControlType controlType = ControlType.Keyboard) {
        switch (controlType) {
            case ControlType.Keyboard:
                return Input.GetKey(KeyCode.Space);
            case ControlType.Gamepad:
                return Input.GetButton("Gamepad_A");
            default:
                return Input.GetKey(KeyCode.Space);
        }
    }
}
