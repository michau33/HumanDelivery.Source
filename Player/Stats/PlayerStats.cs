using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats", fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject {
	[SerializeField] float horizontalSpeed;
	[SerializeField] float verticalSpeed;
	[SerializeField] float rotationSpeed;

	public float HorizontalSpeed { get { return horizontalSpeed; } } 
	public float VerticalSpeed { get { return verticalSpeed; } } 
	public float RotationSpeed { get { return rotationSpeed; } }


}
