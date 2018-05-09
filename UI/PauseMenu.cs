using UnityEngine;


public class PauseMenu : MonoBehaviour {

	static bool isPaused = false;
	public static bool IsPaused {
		get { return isPaused; }
	}

	void Start() {
		this.gameObject.SetActive(false);
	}

	public void TogglePauseMenu() {
		if(!this.gameObject.activeInHierarchy) {
			isPaused = true;
			Time.timeScale = 0f;
			this.gameObject.SetActive(true);
			EventManager.instance.PostNotification(this, "TogglePauseButton");
		} else {
			isPaused = false;
			Time.timeScale = 1f;			
			this.gameObject.SetActive(false);
		}
	}
}
