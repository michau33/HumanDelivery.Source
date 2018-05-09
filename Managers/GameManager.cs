using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager> {

	public Transform[] spawnPoints;
	public GameObject [] prisoners;
	public GameObject [] prisonersToShow;
	public GameObject raceCanvas;
	public Transform prisonersPlaceholder;
	public GameObject image;
	public int maxPrisoners = 10;

	private int counter = 0;
	private GameObject currentRaceToCollect;
	private int currentRaceToCollectIndex = 0;
	
	// Use this for initialization
	void Awake () {
		prisonersToShow = prisoners;
		RandomizeArray( prisonersToShow );
		ShowPrisonersOrder();

        currentRaceToCollect = prisonersToShow[currentRaceToCollectIndex];

		StartCoroutine( SpawnPrisoners() );	
	}
	
	IEnumerator SpawnPrisoners() {
		while( counter <= maxPrisoners ) {
			counter++;
			int randomPrisoner = GetRandom( 0, prisoners.Length );
			int randomSpawnpoint = GetRandom( 0, spawnPoints.Length );

			Instantiate( prisoners[ randomPrisoner ], spawnPoints[ randomSpawnpoint].transform.position, spawnPoints[ randomSpawnpoint ].transform.rotation  ); 
			yield return new WaitForSeconds( Random.Range( .3f, 1.7f ) );
		}
	}

	int GetRandom( int a, int b) {
		return 1;
	}
		
	 public void CatchEntitity( Race race ) {
		 if(  race == currentRaceToCollect.GetComponent<Prisoner>().EntityType.race ) {
			 foreach( GameObject prisoner in prisonersToShow ) {
				 	if( prisoner == prisonersToShow[ currentRaceToCollectIndex ] ) {
						prisonersToShow[ currentRaceToCollectIndex ] = null;
					 }
			 }

			if( currentRaceToCollectIndex < prisoners.Length ) {
				currentRaceToCollectIndex++;
		 		currentRaceToCollect = prisonersToShow[ currentRaceToCollectIndex ];
			}
			
		 	ClearPrisonersOrder();
		 	ShowPrisonersOrder();
		 }

		 switch( race ) {
			case Race.Black:
				DropText("NEGRO");
			 	break;
			case Race.Yellow:
				DropText("CHINA BOI");
				break;
			case Race.White:
				DropText("WHITE");
				break;
			case Race.Indian:
				DropText("CHIEF");
				break;
			default:
				break;
		 }
	 }

	void DropText( string text ) {
			GameObject temp = Instantiate( raceCanvas, GameObject.FindGameObjectWithTag("Player").transform.position,  GameObject.FindGameObjectWithTag("Player").transform.rotation, transform ) as GameObject;
			temp.GetComponentInChildren<Text>().text = text;
			temp.GetComponent<Rigidbody2D>().AddForce( new Vector2( Random.Range( -5f, 5f ), Random.Range( -1f, 1f ) ) * .5f, ForceMode2D.Impulse );
			temp.GetComponent<Rigidbody2D>().angularVelocity = Random.Range( -100f, 100f );
			Destroy( temp, 2f );
	}

	 void RandomizeArray( GameObject [] arr ) {
		 for( int i = 0; i < arr.Length; i++ ) {
			 int randomIndex = Random.Range( 0, arr.Length );
			 GameObject temp = arr[i];
			 arr[i] = arr[ randomIndex ];
			 arr[randomIndex] = temp;
		 }
	 }

	 void ShowPrisonersOrder() {
		 foreach( GameObject prisoner in prisonersToShow ) {
			 if( prisoner != null ) {
				 GameObject temp = Instantiate( image, prisonersPlaceholder.position, prisonersPlaceholder.rotation, prisonersPlaceholder );
			 	temp.GetComponent<Image>().sprite = prisoner.GetComponentInChildren<SpriteRenderer>().sprite;
			 }
		 }

		 if( prisoners == null ) {
			 Debug.Log("Teoretycznie powinienes wygrac gre");
		 }
	 }

	 void ClearPrisonersOrder() {
		 foreach( Image image in prisonersPlaceholder.GetComponentsInChildren<Image>() ) {
			 Destroy( image.gameObject );
		 }
	 }
}
