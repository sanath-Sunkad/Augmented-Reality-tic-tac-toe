// based on: http://www.c-sharpcorner.com/UploadFile/75a48f/tic-tac-toe-game-in-C-Sharp/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// https://github.com/AyARL/UnityGUIExamples/blob/master/EventTrigger/Assets/TriggerSetup.cs
//using UnityEngine.UI;
//using UnityEngine.Events;
using UnityEngine.EventSystems;

public class gameLogic : MonoBehaviour
{
	bool move = true;
	bool winPlayer = false;
	bool winComputer = false;
	bool equal = false;
	public Transform[] modelPlaces;
	public GameObject[] planePlaces;
	//Transform
	public Transform[] modelPrefabs;
	public GameObject cylinderPrefab;
	public GameObject allContent;


	int i = 0;
	int a = 0;
	int b = 0;
	public char[] OX;
	GameObject[] spawnPoints;
	GameObject currentPoint;
	int index;
	public string AirClick;
	public bool Clicked = false;
	public bool hit;

	Transform X;
	Transform O;

	void Start ()
	{
		OX = new char[9];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (move) {
			if ((Input.GetMouseButtonUp (0) || Clicked) && winComputer == false && equal == false) {

				RaycastHit hitInfo = new RaycastHit ();
				if (Clicked == true)
					hit = true;

				if (Clicked == false)
					hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);

				if (hit) {
					if (Clicked == true)
						AirClick = GameObject.Find ("CodeContainer").GetComponent<ProgressBar> ().hitInfo.collider.name;
				
					if (Clicked == false)
						AirClick = hitInfo.collider.name;

					/*if(AirClick == "replayButton") {
						replay();
					}
*/
					a++;
					move = false;
					//Debug.Log (hitInfo.collider.name);
					for (int i = 0; i < 9; i++) {
						if (AirClick == planePlaces [i].name) {
							OX [i] = 'X';
							Debug.Log ("i: " + i);
							GameObject.Find (planePlaces [i].name).SetActive (false);
							X = Instantiate (modelPrefabs [0], planePlaces [i].transform.position, planePlaces [i].transform.rotation) as Transform;
							X.transform.parent = GameObject.Find ("ImageTarget").transform;
							X.name = "x" + i;
							Debug.Log ("XXXXXXXXXXXXXX:" + X);
							Vector3 pos = X.transform.position;    
							X.transform.position = new Vector3 (pos.x, pos.y + 0.4f, pos.z);

						}
					}
					//Debug.Log ("LEEEEENGTH: " + a);
					if (OX [0] == 'X' && OX [1] == 'X' && OX [2] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [2]);
						Debug.Log ("You Win! - 1st row " + OX [0] + " " + OX [1] + " " + OX [2]);
					}
					if (OX [3] == 'X' && OX [4] == 'X' && OX [5] == 'X') {
						winPlayer = true;
					
						CreateCylinderBetweenPoints (modelPlaces [3], modelPlaces [5]);
						Debug.Log ("You Win! - 2nd row " + OX [3] + " " + OX [4] + " " + OX [5]);
					}
					if (OX [6] == 'X' && OX [7] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [6], modelPlaces [8]);
						Debug.Log ("You Win! - 3rd row " + OX [6] + " " + OX [7] + " " + OX [8]);
					}
					if (OX [0] == 'X' && OX [3] == 'X' && OX [6] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [6]);
						Debug.Log ("You Win! - 1st column " + OX [0] + " " + OX [3] + " " + OX [6]);
					}
					if (OX [1] == 'X' && OX [4] == 'X' && OX [7] == 'X') {
						winPlayer = true;
					
						CreateCylinderBetweenPoints (modelPlaces [1], modelPlaces [7]);
						Debug.Log ("You Win! - 2nd column " + OX [1] + " " + OX [4] + " " + OX [7]);
					}
					if (OX [2] == 'X' && OX [5] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [2], modelPlaces [8]);
						Debug.Log ("You Win! - 3rd column " + OX [2] + " " + OX [5] + " " + OX [8]);
					}
					if (OX [0] == 'X' && OX [4] == 'X' && OX [8] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [8]);
						Debug.Log ("You Win! - 1st Diagonal " + OX [0] + " " + OX [4] + " " + OX [8]);
					}
					if (OX [2] == 'X' && OX [4] == 'X' && OX [6] == 'X') {
						winPlayer = true;

						CreateCylinderBetweenPoints (modelPlaces [2], modelPlaces [6]);
						Debug.Log ("You Win! - 2nd Diagonal  " + OX [2] + " " + OX [4] + " " + OX [6]);
					}
					if (a == 5 && winPlayer == false) {
						equal = true;
						Debug.Log ("Equal " + a);
					}
				}
				Clicked = false;
			}

			if (move == false && winPlayer == false && equal == false) {
				StartCoroutine (TimeDelay ());
			}
				

			if (winPlayer || winComputer || equal) {
				//if(Clicked == true)
				//AirClick = GameObject.Find ("CodeContainer").GetComponent<ProgressBar> ().hitInfo.collider.name;

				if (winPlayer) {
					Debug.Log ("PLAYER WINS");
			//		allContent.SetActive (true);
			
					// turn victory model/effects
				}
				if (winComputer) {
					Debug.Log ("COMPUTER WINS");
					// turn victory model/effects
				}
				if (equal) {
					Debug.Log ("EQUAL");
					// turn victory model/effects
				}
			
			}


		}

		if (Input.GetMouseButtonUp (0)) {
			RaycastHit hitInfo = new RaycastHit ();
			hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit) {
				if (hitInfo.collider.name == "replayButton")
					replay ();
			}
		}

	}



	private void CreateCylinderBetweenPoints (Transform modelPlaces1, Transform modelPlaces2)
	{
		Vector3 pos = Vector3.Lerp (modelPlaces1.transform.position, modelPlaces2.transform.position, (float)0.5);
		GameObject segObj = (GameObject)Instantiate (cylinderPrefab);
		//segObj.transform.parent = GameObject.Find("ImageTarget").transform;
		segObj.transform.position = pos;    
		segObj.transform.localPosition = new Vector3 (pos.x, pos.y + 0.85f, pos.z);
		segObj.transform.up = modelPlaces2.transform.position - modelPlaces1.transform.position;
	}

	IEnumerator TimeDelay ()
	{
		yield return new WaitForSeconds (0.4f);
		spawnPoints = GameObject.FindGameObjectsWithTag ("Plane");
		//Debug.Log ("SpawnPoints: " + spawnPoints);
		index = Random.Range (0, spawnPoints.Length);
		currentPoint = spawnPoints [index];
		Debug.Log ("PC: " + currentPoint.name);
		move = true;
		b++;
		for (int i = 0; i < 9; i++) {
			if (currentPoint.name == planePlaces [i].name) { //hitInfo.collider.name
				OX [i] = 'O';
				Debug.Log ("i: " + i);
				GameObject.Find (planePlaces [i].name).SetActive (false);
				O = Instantiate (modelPrefabs [1], planePlaces [i].transform.position, planePlaces [i].transform.rotation) as Transform;
				O.transform.parent = GameObject.Find ("ImageTarget").transform;
				O.name = "o" + i;
				//Debug.Log(O);
				Vector3 pos = O.transform.position;    
				O.transform.position = new Vector3 (pos.x, pos.y + 0.4f, pos.z);
			}
		}

		if (OX [0] == 'O' && OX [1] == 'O' && OX [2] == 'O') {
			winComputer = true;
		
			CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [2]);
			Debug.Log ("Computer Wins! - 1st row " + OX [0] + " " + OX [1] + " " + OX [2]);
		}
		if (OX [3] == 'O' && OX [4] == 'O' && OX [5] == 'O') {
			winComputer = true;
		
			CreateCylinderBetweenPoints (modelPlaces [3], modelPlaces [5]);
			Debug.Log ("Computer Wins! - 2nd row " + OX [3] + " " + OX [4] + " " + OX [5]);
		}
		if (OX [6] == 'O' && OX [7] == 'O' && OX [8] == 'O') {
			winComputer = true;
		
			CreateCylinderBetweenPoints (modelPlaces [6], modelPlaces [8]);
			Debug.Log ("Computer Wins! - 3rd row " + OX [6] + " " + OX [7] + " " + OX [8]);
		}
		if (OX [0] == 'O' && OX [3] == 'O' && OX [6] == 'O') {
			winComputer = true;
		
			CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [6]);
			Debug.Log ("Computer Wins! - 1st column " + OX [0] + " " + OX [3] + " " + OX [6]);
		}
		if (OX [1] == 'O' && OX [4] == 'O' && OX [7] == 'O') {
			winComputer = true;

			CreateCylinderBetweenPoints (modelPlaces [1], modelPlaces [7]);
			Debug.Log ("Computer Wins! - 2nd column " + OX [1] + " " + OX [4] + " " + OX [7]);
		}
		if (OX [2] == 'O' && OX [5] == 'O' && OX [8] == 'O') {
			winComputer = true;

			CreateCylinderBetweenPoints (modelPlaces [2], modelPlaces [8]);
			Debug.Log ("Computer Wins! - 3rd column " + OX [2] + " " + OX [5] + " " + OX [8]);
		}
		if (OX [0] == 'O' && OX [4] == 'O' && OX [8] == 'O') {
			winComputer = true;
		
			CreateCylinderBetweenPoints (modelPlaces [0], modelPlaces [8]);
			Debug.Log ("Computer Wins! - 1st Diagonal " + OX [0] + " " + OX [4] + " " + OX [8]);
		}
		if (OX [2] == 'O' && OX [4] == 'O' && OX [6] == 'O') {
			winComputer = true;

			CreateCylinderBetweenPoints (modelPlaces [2], modelPlaces [6]);
			Debug.Log ("Computer Wins! - 2nd Diagonal  " + OX [2] + " " + OX [4] + " " + OX [6]);
		}
		if (b == 5 && winComputer == false) {
			equal = true;
			Debug.Log ("Equal " + b);
		}
	}
		

	public void replay ()
	{
		
		if (GameObject.FindGameObjectsWithTag ("XOdelete") != null) {
			for (int i = 0; i < GameObject.FindGameObjectsWithTag ("XOdelete").Length; i++) {
				Destroy (GameObject.FindGameObjectsWithTag ("XOdelete") [i]);
			}
		}

		for (int i = 0; i < 9; i++) {
			if (!planePlaces [i].activeSelf)
				planePlaces [i].SetActive (true);
		}

		if (GameObject.Find ("Cylinder(Clone)") != null)
			Destroy (GameObject.Find ("Cylinder(Clone)"));
		

		move = true;
		winPlayer = false;
		winComputer = false;
		equal = false;
		Clicked = false;

		a = 0;
		b = 0;

		OX = new char[9];

	}
}