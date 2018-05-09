using UnityEngine;

[CreateAssetMenu( menuName = "Prisoner/Stats", fileName="PrisonerStats")]
public class PrisonerStats : ScriptableObject {
	[SerializeField] float movementSpeed;
	public float MovementSpeed { get { return movementSpeed; } }
	[SerializeField] float changeDirectionMinTime;
	public float ChangeDirectionMinTime { get { return changeDirectionMinTime; } }
	[SerializeField] float changeDirectionMaxTime;
	public float ChangeDirectionMaxTime { get { return changeDirectionMaxTime; } }
}
