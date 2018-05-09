using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextTypeWriter : MonoBehaviour 
{
    [SerializeField] string textToShow;
    [SerializeField] float minWaitTime = 0.125f;
    [SerializeField] float maxWaitTime = 0.225f;
    [SerializeField] GameObject nextToAnimate;

	Text text;

	void Awake () 
	{
		text = GetComponent<Text> ();
	}

    void Start()
    {
        text.text = ""; 
    }

    IEnumerator AnimateText()
	{
		foreach (char c in textToShow ) 
		{
			text.text += c;
			yield return new WaitForSeconds ( Random.Range( minWaitTime, maxWaitTime ) );
		}
		nextToAnimate.gameObject.SetActive( true );
	}

}