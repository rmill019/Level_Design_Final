using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHammer : Enemy {

	private Panel homePanel;//= Battlefield.S.Field [playerX, playerZ];
	private Panel atkPanel; //= Battlefield.S.Field [Battlefield.S.playerUnit.playerX + 1, playerZ];
	private bool attacking = false;

	void Start () {
		//this.GetComponent<EnemyHoley> ().enabled = true;
		moveFresh = 30;
		refreshM = (GameController.FrameRate);
		refreshA = (GameController.FrameRate);
		playerUnit = gameObject;
		unitSpriteRenderer = playerUnit.GetComponentInChildren<SpriteRenderer> ();//same as below
		animator = playerUnit.GetComponentInChildren<Animator> ();//gets animator for sprite
		HP = Level * 70;
		power = Level;
		speed = Level;
		charge = Level;
		weight = 2;

		//Debug.Log("This Hammer: " + playerX + " X " + playerZ + " Z " );
		Battlefield.S.Field [playerX,playerZ].load = weight;//sets wght to current panel
		playerSide = Battlefield.S.Field [playerX, playerZ].side;//gets panel side color
		LayerChange();
		homePanel = Battlefield.S.Field [playerX, playerZ];
		playerUnit.transform.position = new Vector3 (
			Battlefield.S.Field[playerX,playerZ].panelCord.x,
			0.0f,
			Battlefield.S.Field[playerX,playerZ].panelCord.z);// sets position
	}



	// Update is called once per frame
	void Update () {
		if ((HP - Damage) <= 0) {
			DeleteUnit();
			return;
		}

		if (moveFresh == 0 && attackFresh == 0 && !attacking) {
			Move ();
		} else {
			if (moveFresh > 0){
				moveFresh = (moveFresh - 1);
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
		
		animator.SetTrigger ("playerMove");
		Debug.Log ("Hammer Attacks");
		moveFresh = refreshM ;
		attackFresh = refreshA ;
		homePanel = Battlefield.S.Field [playerX, playerZ];

		playerUnit.transform.position = new Vector3 (atkPanel.panelCord.x,0.0f,atkPanel.panelCord.z);// moves player to panel
		atkPanel.load = weight;//loads weight to new panel
		prevPanel = homePanel;
		CheckForCrack();

		//Insert Attack Here
		animator.SetTrigger ("playerAttack");

		for(int i =0; i<refreshA+60;i++){
			if (i == refreshA + 29) {
				Invoke ("GoHome", 1f);
			}
		}
	}

	public void GoHome(){
		attacking = false;
		atkPanel.load = 0;
		playerUnit.transform.position = new Vector3 (homePanel.panelCord.x,0.0f,homePanel.panelCord.z);// moves player to panel
		prevPanel = atkPanel;
		moveFresh = refreshM;
		attackFresh = refreshA;
		attacking = false;

	}

	public void Move(){
		if (playerZ == Battlefield.S.playerUnit.playerZ) {
			if (Battlefield.S.Field[Battlefield.S.playerUnit.playerX+1,playerZ].load == 0 &&
				Battlefield.S.Field[Battlefield.S.playerUnit.playerX+1,playerZ].maxLoad >= weight) {
				atkPanel = Battlefield.S.Field [Battlefield.S.playerUnit.playerX + 1, playerZ];
				moveFresh = refreshM;
				attackFresh = refreshA;
				attacking = true;
				Invoke ("Attack", 0.3f);
			}
		} 
	}
		
}
