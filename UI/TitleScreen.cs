using UnityEngine;

public class TitleScreen : MonoBehaviour {

	// for now load game after certain amount of time. In the future game will load when all assets load.
	[SerializeField] float timeToLoad = 5f;

	float time = 0f;

	void Update() {
		time += Time.deltaTime;
		if( time >= timeToLoad ) {
			LevelManager.instance.LoadNextLevel();
		}
	}
}
