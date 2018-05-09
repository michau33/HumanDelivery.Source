using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	void Start() {
		EventManager.instance.AddListener(this, "TogglePauseButton");
	}

	public void TogglePauseButton() {
		if(this.gameObject.activeInHierarchy) {
			this.gameObject.SetActive(false);
		} 
	}
}