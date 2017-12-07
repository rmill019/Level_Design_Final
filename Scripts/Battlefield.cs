using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Battlefield : MonoBehaviour {

	static public Battlefield S;

	const int Columns = 6;
	const int Rows = 3;
	public Panel[] Panels;//holds panel objects
	public  Panel [,] Field;//holds all Panel objects
	public  Unit playerUnit;
	//public GameObject playerUnit;// the unit controlled by the player
	[SerializeField]
	public Enemy[] Enemies;// a list of all units on the field
	public EnemyHoley[] HoleyPatrol;
	public int maxEnemies = 3;

	//HoleyControls
	//public static EnemyHoley[] HoleyArray;


	void Awake(){
		S = this;

		SetupField();

	}

	void Start () {
		SetupEnemies ();
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

	void SetupEnemies(){
		Enemies = new Enemy[3];
		int enemyCount = 0;
		foreach (Enemy e in Resources.FindObjectsOfTypeAll(typeof(Enemy))){
			if (enemyCount < 3) {
				e.EnemyOrder = enemyCount;
				Enemies [enemyCount] = e;
				enemyCount++;
			}else if(enemyCount >= 3){
				Destroy (e.gameObject);
			}
		}
		HoleyPatrol = new EnemyHoley[3];
		int holeyCount = 0;
		foreach (EnemyHoley h in Resources.FindObjectsOfTypeAll(typeof(EnemyHoley))){
			if (holeyCount < 3) {
				h.HoleyOrder = holeyCount;
				if (holeyCount < 1) {
					h.Acting = true;
				}
				HoleyPatrol [holeyCount] = h;
				holeyCount++;
			}
		}
	}
	public void HoleyTurnPass(EnemyHoley pastHoley){
		int holeyCheck = pastHoley.HoleyOrder;

		if (HoleyPatrol [(holeyCheck+1) % HoleyPatrol.Length] == null) {
			holeyCheck++;
		}
		if (HoleyPatrol [(holeyCheck+1) % HoleyPatrol.Length] == null) {
			holeyCheck++;
		}

		HoleyPatrol [(holeyCheck + 1) % HoleyPatrol.Length].moveFresh = HoleyPatrol [(holeyCheck + 1) % HoleyPatrol.Length].refreshM;
		HoleyPatrol [(holeyCheck + 1) % HoleyPatrol.Length].Acting = true;


	}
	public void Progress(){
		
	}
	public void GameOver(){
	}
	public void CheckForWin(){
	
	}


}
