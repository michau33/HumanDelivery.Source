using System.Collections;
using UnityEngine;

public class SceneFading : MonoBehaviour {
#region Variables
	public Texture2D fadeOutTexture; 	   	   // the texture that will overlay the screen i.e. black image
	public float fadeSpeed = 0.8f; 					// the fading speed
	
	private int drawDepth = -1000; 	 			 // the texture's order in the draw hierarch: a low number means it renders on the top
	private float alpha = 1.0f; 						// the texture's alpha value ( 0, 1 )
	private int fadeDir = -1;							 // -1 = fadeIn , 1 = fadeOut
#endregion

	void OnGUI () {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// clamp the number between 0 and 1 ( alpha value )
		alpha = Mathf.Clamp01( alpha );
		// set color of texture. All color values should remain the same
		GUI.color = new Color( GUI.color.r, GUI.color.g, GUI.color.b, alpha );
		// black texture will render on top
		GUI.depth = drawDepth;
		// draw texture on entire screen area
		GUI.DrawTexture( new Rect( 0, 0, Screen.width, Screen.height ), fadeOutTexture );
	}

	public float BeginFade ( int direction ) {
		fadeDir = direction;
		// return the fadespeed variable so it's easy to time the Application.LoadLevel();
		return ( fadeSpeed );
	}

	void OnLevelWasLoaded() {
		// FadeIn
		BeginFade( -1 ); 
	}
}
