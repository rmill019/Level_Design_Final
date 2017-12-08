using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power_Gauge : MonoBehaviour {

	public Transform powerGauge;
	public Transform powerGauge1;
	public Transform powerGauge2;
	[SerializeField] private float progress;
	[SerializeField] private float speed;
	private int fill = 0;

	public bool fillIt = false;

	void Awake() {
		powerGauge.GetComponent<Image> ().fillAmount = progress / 100;
		powerGauge1.GetComponent<Image> ().fillAmount = progress / 100;
		powerGauge2.GetComponent<Image> ().fillAmount = progress / 100;
	}

	// Update is called once per frame
	void Update () {

		if (fill == 0) {
			progress += speed * Time.deltaTime;
			powerGauge.GetComponent<Image> ().fillAmount = progress / 100;

			if (progress >= 100) {
				fill = 1;
				progress = 0;

				powerGauge.GetComponent<Image> ().color = new Color32 (0, 255, 0, 255);
			}

		} else if (fill == 1) {
			progress += speed * Time.deltaTime;
			powerGauge1.GetComponent<Image> ().fillAmount = progress / 100;

			if (progress >= 100) {
				fill = 2;
				progress = 0;

				powerGauge1.GetComponent<Image> ().color = new Color32 (0, 255, 0, 255);
			}

		} else if (fill == 2) {
			progress += speed * Time.deltaTime;
			powerGauge2.GetComponent<Image> ().fillAmount = progress / 100;

			if (progress >= 100) {
				fill = 3;
				progress = 0;

				powerGauge2.GetComponent<Image> ().color = new Color32 (0, 255, 0, 255);
			}
		}


	//	theVoid ();


	}

	void theVoid () {


		if (fillIt == true && fill == 0) {
			progress += 25;
			powerGauge.GetComponent<Image> ().fillAmount = progress / 100;
			fillIt = false;


		} else if (fillIt == true && fill == 1) {
			progress += 25;
			powerGauge1.GetComponent<Image> ().fillAmount = progress / 100;
			fillIt = false;

			

		} else if (fillIt == true && fill == 2) {
			progress += 25;
			powerGauge2.GetComponent<Image> ().fillAmount = progress / 100;
			fillIt = false;


		}
				
				

		if (progress >= 100 && fill == 0){
			fill = 1;
			progress = 0;
		}
		else if (progress >= 100 && fill == 1){
			fill = 2;
			progress = 0;
		}
		else if (progress >= 100 && fill == 2){
			fill = 3;
			progress = 0;
		}
		



	}

	IEnumerator pause(){

		yield return new WaitForSeconds (5);
	}
}


