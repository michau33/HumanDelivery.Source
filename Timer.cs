using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField] float counterTime = 90f;

	private Text counter;

	void Awake () {
        counter = GetComponent<Text>();
	}

	void Update () {
        counterTime -= Time.deltaTime;
        counter.text = counterTime.ToString("F2") + "s";
	
        if ( counterTime <= 0f ) {
			LevelManager.instance.RestartLevel();
		}
	}
}
