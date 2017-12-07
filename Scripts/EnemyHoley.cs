using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoley : Enemy {
	
	public bool Acting = false;
	public int HoleyOrder;

	// Use this for initialization
	void Start () {
		//this.GetComponent<EnemyHoley> ().enabled = true;

		moveFresh = 30;
		refreshM = (GameController.FrameRate);
		refreshA = (GameController.FrameRate);
		playerUnit = gameObject;
		unitSpriteRenderer = playerUnit.GetComponentInChildren<SpriteRenderer> ();//same as below
		animator = playerUnit.GetComponentInChildren<Animator> ();//gets animator for sprite
		HP = Level * 40;
		power = Level;
		speed = Level;
		charge = Level;
		weight = 2;

		Battlefield.S.Field [playerX, playerZ].load = weight;//sets wght to current panel
		playerSide = Battlefield.S.Field [playerX, playerZ].side;//gets panel side color
		LayerChange();
		playerUnit.transform.position = new Vector3 (
			Battlefield.S.Field[playerX,playerZ].panelCord.x,
			0.0f,
			Battlefield.S.Field[playerX,playerZ].panelCord.z);// sets position
	}


	
	// Update is called once per frame
	void Update () {

		if ((HP - Damage) <= 0) {
			DeleteHoley();
			return;
		}

		if (moveFresh == 0 && attackFresh == 0 && Acting) {
			Move ();
		} else {
			if (moveFresh > 0){
				moveFresh--;
			}
			if (attackFresh > 0){
				attackFresh--;
			}
		}

		if (playerUnit == null) {
			Start ();
		}
	}

	public void Attack(){
		
		int atkDelay = 21 - speed;
		for (int atkWait = 0; atkWait< atkDelay; atkWait++){
			
		}

		Debug.Log ("Holey Attacks");

		//Insert Attack Here
		animator.SetTrigger ("playerAttack");
		moveFresh = refreshM - (speed * 2);
		attackFresh = refreshA;
		Acting = false;
		Battlefield.S.HoleyTurnPass (this);
	}

	public void Move(){
		if (playerZ != Battlefield.S.playerUnit.playerZ) {
			if (playerZ<Battlefield.S.playerUnit.playerZ) {
				if (playerZ < 2) {
					targetZ = playerZ + 1;
					if (Battlefield.S.Field [playerX, targetZ].side == playerSide &&
					    Battlefield.S.Field [playerX, targetZ].load == 0 &&
					    Battlefield.S.Field [playerX, targetZ].maxLoad >= weight) {
						prevPanel = Battlefield.S.Field [playerX, playerZ];
						Battlefield.S.Field [playerX, playerZ].load = 0;
						playerZ = targetZ;
						playerUnit.transform.position = new Vector3 (Battlefield.S.Field [playerX, playerZ].panelCord.x, transform.position.y, Battlefield.S.Field [playerX, playerZ].panelCord.z);
						LayerChange ();
						animator.SetTrigger ("playerMove");
						Battlefield.S.Field [playerX, playerZ].load = weight;
						CheckForCrack ();
						Debug.Log ("Moved Left");
						moveFresh = refreshM - (speed * 2);
						return;
					} else {
						Attack ();
					}
				} 
			}else if(playerZ>Battlefield.S.playerUnit.playerZ){
				if(playerZ > 0){
					targetZ = playerZ - 1;
					if (Battlefield.S.Field [playerX, targetZ].side == playerSide &&
						Battlefield.S.Field [playerX, targetZ].load == 0 &&
						Battlefield.S.Field [playerX, targetZ].maxLoad >= weight) {
						prevPanel = Battlefield.S.Field [playerX, playerZ];
						Battlefield.S.Field [playerX, playerZ].load = 0;
						playerZ = targetZ;
						playerUnit.transform.position = new Vector3 (Battlefield.S.Field [playerX, playerZ].panelCord.x, transform.position.y, Battlefield.S.Field [playerX, playerZ].panelCord.z);
						LayerChange ();
						animator.SetTrigger ("playerMove");
						Battlefield.S.Field [playerX, playerZ].load = weight;
						CheckForCrack ();
						Debug.Log ("Moved Left");
						moveFresh = refreshM - (speed * 2);
						return;
					} else {
						Attack ();
					}
				}
			}
		} else {
			Attack ();
		}
	}

	public void DeleteHoley(){
		Battlefield.S.HoleyTurnPass (this);
		EnemyHoley unitToDelete = Battlefield.S.HoleyPatrol [HoleyOrder];
		Enemy unitToDestroy = Battlefield.S.Enemies [EnemyOrder];
		unitToDelete = null;
		unitToDestroy = null;
		unitDeleted = true;
		Battlefield.S.Field [playerX, playerZ].load = 0;
		unitSpriteRenderer.color = Color.black;
		playerUnit.GetComponent<Collider> ().enabled = false;
		Destroy (playerUnit,0.5f);
		Battlefield.S.Progress ();
	}
}

