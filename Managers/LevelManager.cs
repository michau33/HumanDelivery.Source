using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

	public void RestartLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene( UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex );
	}

	public void LoadNextLevel() {
		UnityEngine.SceneManagement.SceneManager.LoadScene( UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1 );
	}

	public void ExitGame() {
		Application.Quit();
	}

	public IEnumerator RestartLevelWithDelay( float time ) {
		yield return new  WaitForSeconds( time );
		RestartLevel();
	}
}
