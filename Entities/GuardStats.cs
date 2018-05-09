using UnityEngine;

[CreateAssetMenu( menuName = "Guard/Options", fileName = "GuardStats")]
public class GuardStats : ScriptableObject {
	[SerializeField] float movementSpeed;
	public float MovementSpeed { get { return movementSpeed; } }

	[SerializeField] float catchAfter;
	public float CatchAfter { get { return catchAfter; } }

	[SerializeField] float playerSpottedDistance;
	public float PlayerSpottedDistance { get { return playerSpottedDistance; } }
}
