using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battlefield : MonoBehaviour {
	const int Columns = 6;
	const int Rows = 3;
	public Panel[] Panels;//holds panel objects
	public static Panel [,] Field;//holds all Panel objects
	public GameObject playerUnit;// the unit controlled by the player
	public static List<GameObject> fieldUnits;// a list of all units on the field


	void Awake(){
		SetupField(); 
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void SetupField(){
		Field = new Panel[6, 3];
		foreach(Panel p in Panels){
			Field [(p.column - 1), (p.row - 1)] = p;
		}
		//Adds panels to panel array

	}
}
