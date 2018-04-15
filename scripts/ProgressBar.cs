using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
	public Camera cameraLeft;
	public GameObject RadialProgressBarLeft;
	public Transform LoadingBarLeft;
	public Transform TextIndicatorLeft;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

	public bool clicked = false;
	Collider hitTemp;
	public RaycastHit hitInfo;
	bool hit;
	bool start = false;

	// Update is called once per frame
	void Update () {
		Ray ray = cameraLeft.ScreenPointToRay (RadialProgressBarLeft.transform.position);
		hitTemp = hitInfo.collider;
		hit = Physics.Raycast (ray, out hitInfo);
				
		//Debug.Log ("hitTemp: " + hitTemp + "  hit.collider: " + hitInfo.collider);
		if (hitTemp != null || hitInfo.collider != null) {
			if (hitTemp == hitInfo.collider && clicked == false && (hitInfo.collider.name == "replayButton" || hitInfo.collider.name == "Plane1" || hitInfo.collider.name == "Plane2" || hitInfo.collider.name == "Plane3" || hitInfo.collider.name == "Plane4" || hitInfo.collider.name == "Plane5" || hitInfo.collider.name == "Plane6" || hitInfo.collider.name == "Plane7" || hitInfo.collider.name == "Plane8" || hitInfo.collider.name == "Plane9")) {
				if (currentAmount < 100) {
					currentAmount += speed * Time.deltaTime;
					TextIndicatorLeft.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
				} else {
					TextIndicatorLeft.GetComponent<Text> ().text = "100%";
					clicked = true;
					if (hitInfo.collider.name == "replayButton")
						GameObject.Find ("CodeContainer").GetComponent<gameLogic> ().replay ();

					//Debug.Log ("CLICKED: " + clicked + " " + hitInfo.collider.name);
					GameObject.Find ("CodeContainer").GetComponent<gameLogic> ().Clicked = true;
				}
				LoadingBarLeft.GetComponent<Image> ().fillAmount = currentAmount / 100;
			}
			if (hitTemp != hitInfo.collider) {
				clicked = false;
				currentAmount = 0;
				LoadingBarLeft.GetComponent<Image> ().fillAmount = 0;
				TextIndicatorLeft.GetComponent<Text> ().text = "0%";
			}
		}

	}


}
