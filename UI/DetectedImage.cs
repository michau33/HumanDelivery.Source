using UnityEngine;

public class DetectedImage : MonoBehaviour {
	Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
	}

	void Start() {
		EventManager.instance.AddListener(this, "PlayerDetected");
		EventManager.instance.AddListener(this, "PlayerLost");
	}

	public void PlayerDetected(Component sender) {
		animator.Play("FadeIn");
	}

	public void PlayerLost(Component sender) {
		animator.Play("FadeOut");
	}
}
