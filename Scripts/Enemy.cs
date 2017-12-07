using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {
	

	public int Level = 1;
	public int EnemyOrder = 0;//Get from battlefield for setup

	// Use this for initialization
	void Awake(){
		//animator = playerUnit.GetComponentInChildren<Animator> ();

	}

	void Start(){
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void DeleteUnit(){
		Enemy unitToDestroy = Battlefield.S.Enemies [EnemyOrder];
		unitToDestroy = null;
		unitDeleted = true;
		Battlefield.S.Field [playerX, playerZ].load = 0;
		unitSpriteRenderer.color = Color.black;
		playerUnit.GetComponent<Collider> ().enabled = false;
		Destroy (playerUnit,0.5f);
		Battlefield.S.Progress ();
	}
}
