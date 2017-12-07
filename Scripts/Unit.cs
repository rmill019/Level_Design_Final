using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UnitType {
	Alive,//an enemy or player
	Obstacle//
}
public enum UnitCondition{
	Normal,
	Flinch
}
public class Unit : MonoBehaviour {

	protected int maxStat = 5;

	public GameObject playerUnit; //the player's character\
	public SpriteRenderer unitSpriteRenderer;
	public UnitType unitType;
	public FieldSide playerSide;
	public Animator animator;

	public int moveFresh; //when it hits 0 the player can move again
	public int refreshM = 20; //frames to refresh movement (essentially player speed)

	public int attackFresh;//while at zero the player can attack
	public int refreshA = 20;//frames to refresh attack.
	public int attackLength;//how many frames the player is attacking at

	public int chargeFresh;// determines if player attack is charged
	public int refreshC = 500; //frames to charge
	public int chargeLag; // additional seconds to charge

	public int cardSlot; //checks how many cards are in the slot

	public int speed = 1; // boosts speed of attacks and boosts movement speed
	public int power = 1; // increases basic and charge attack power
	public int charge = 1;// reduces charge frames
	public int HP = 100; //Max Damage the player can take
	public int Damage = 0;
	public int weight = 2;
	public bool unitDeleted = false;

	public int playerX; //the player's x-axis
	public int playerZ; //the player's z-axis
	public int targetX;// target panels
	public int targetZ;
	public Panel prevPanel;//Previous Panel
	public SortingLayer unitLayer;

	void Update(){
		
	}

	public void LayerChange(){
		switch (playerZ) {
		case 0:
			unitSpriteRenderer.sortingLayerName = "RowA";
			break;
		case 1:
			unitSpriteRenderer.sortingLayerName = "RowB";
			break;
		case 2:
			unitSpriteRenderer.sortingLayerName = "RowC";
			break;
		}
	}

	public virtual void DeleteUnit(){
		unitDeleted = true;
		Battlefield.S.Field [playerX, playerZ].load = 0;
		unitSpriteRenderer.color = Color.black;
		playerUnit.GetComponent<Collider> ().enabled = false;
		Destroy (playerUnit,1f);

	}
		
	public virtual void CheckForCrack(){
		if (prevPanel.condition == Condition.CRACKED) {
			prevPanel.BreakPanel();
		}
	}
	// Use this for initialization
}
