using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : Unit {


	private Animator 		animator;
	private Combat 	 		_combat;
	public Text 			hpScribe;

	// Use this for initialization
	void Awake(){
		playerUnit = gameObject;
		unitSpriteRenderer = playerUnit.GetComponentInChildren<SpriteRenderer> ();//same as below
		animator = playerUnit.GetComponentInChildren<Animator> ();//gets animator for sprite
		unitType = UnitType.Alive;
		weight = 2;// 2 for grounded, 1 for flying(ignores broken terrain)
		playerX = 1;//Player's panel positon horizontal 0-5. B2 
		playerZ = 1;//Players panel position vert 0-2. B2
		moveFresh = 0;//Refresh counter for movement
		attackFresh = 0;//Refresh for attacking
		Damage = 0;//The players damage
		refreshA = (GameController.FrameRate/3);//
		refreshC = (GameController.FrameRate*6) + (chargeLag * GameController.FrameRate);//
		refreshM = (GameController.FrameRate /3);//
		chargeFresh = refreshC ;// initialize charge
	}

	void Start () {
		Battlefield.Field [playerX, playerZ].load = weight;//sets wght to current panel
		playerSide = Battlefield.Field [playerX, playerZ].side;//gets panel side color
		playerUnit.transform.position = new Vector3 (Battlefield.Field[playerX,playerZ].panelCord.x,transform.position.y,Battlefield.Field[playerX,playerZ].panelCord.z);// sets position
		hpScribe.text = (HP - Damage).ToString();//HP bar set
		Debug.Log (playerUnit.name + " set!");

		// Find Combat Component
		_combat = GetComponent<Combat>();
	}

	void FixedUpdate (){
		if ((HP - Damage) <= 0) {
			unitDeleted = true;
			Battlefield.Field [playerX, playerZ].load = 0;
			unitSpriteRenderer.color = Color.black;
			playerUnit.GetComponent<Collider> ().enabled = false;
			hpScribe.text = "0";
			Invoke ("DeleteUnit",1.5f);
			return;
		}

		if (moveFresh > 0){
			moveFresh = (moveFresh - 1);
		}

		if (attackFresh > 0) {
			attackFresh = (attackFresh-1);
		} 

		if (attackFresh == 0 && chargeFresh > 0) {
			chargeFresh = chargeFresh - 1;
		} 

		if (attackFresh <= 0){
			Attack ();
		}
		if (moveFresh <= 0) {
			Move ();
		}

		hpScribe.text = (HP - Damage).ToString();

	}

	// Setting Attacks so that they can work from a button call
	public void MeleeAttack ()
	{
		//Basic Melee Atk
		animator.SetTrigger ("playerAttack");
		attackFresh = refreshA + attackLength - (speed);
		moveFresh = attackFresh;
		chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
		_combat.Attack (Combat.eAttackType.melee, gameObject.tag);
	}

	public void BlasterAttack ()
	{
		//Basic Blaster Atk
		animator.SetTrigger ("playerAttack");
		attackFresh = refreshA + attackLength - (speed);
		moveFresh = attackFresh;
		chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
		print (gameObject.tag);
		_combat.Attack (Combat.eAttackType.blaster, gameObject.tag);
	}

	public void BombAttack ()
	{
		//Basic Bomb Atk
		animator.SetTrigger ("playerAttack");
		attackFresh = refreshA + attackLength - (speed);
		moveFresh = attackFresh;
		chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
		_combat.Attack (Combat.eAttackType.bomb, gameObject.tag);
	}

	void Attack(){

		if (Input.GetKeyDown (KeyCode.C) && cardSlot > 0)
		{
			//launches card attack
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
			Debug.Log ("Attacking!: Card Attack");
		} 
		else if (Input.GetKey (KeyCode.X) && chargeFresh <= 0)
		{
			//ChrgAtk
			animator.SetTrigger ("playerAttack");
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
			Debug.Log ("Attacking!: Charge Attack");
		} 
		else if (Input.GetKey (KeyCode.X) && chargeFresh > 0)
		{
			//Basic Atk
			animator.SetTrigger ("playerAttack");
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
		}
		// TODO Remove this else if statement. I am adding it to test the Bomb attack
		else if (Input.GetKey (KeyCode.W) && chargeFresh > 0)
		{
			//Basic Bomb Atk
			animator.SetTrigger ("playerAttack");
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
			_combat.Attack (Combat.eAttackType.bomb, gameObject.tag);
		} 
		// TODO Remove this else if statement as well. It was added to check Blaster Attack.
		else if (Input.GetKey (KeyCode.Q) && chargeFresh > 0)
		{
			//Basic Blaster Atk
			animator.SetTrigger ("playerAttack");
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
			print (gameObject.tag);
			_combat.Attack (Combat.eAttackType.blaster, gameObject.tag);

		}
		// TODO Remove this else if statement as well. It was added to check Melee Attack.
		else if (Input.GetKey (KeyCode.E) && chargeFresh > 0)
		{
			//Basic Melee Atk
			animator.SetTrigger ("playerAttack");
			attackFresh = refreshA + attackLength - (speed);
			moveFresh = attackFresh;
			chargeFresh = refreshC + (chargeLag * GameController.FrameRate) - (charge * GameController.FrameRate);
			_combat.Attack (Combat.eAttackType.melee, gameObject.tag);
		}
	}

	void Move() {
		//Use Move Right as a reference. All of them are practically the same in concept
		if (Input.GetKey(KeyCode.RightArrow)) {
			Debug.Log (playerUnit.name + " Moving Right!");
			if (playerX < 5) {// check player's column position[0-5] 
				targetX = playerX + 1;//sets a target for unit +1 for moving right -1 for left
				if (Battlefield.Field [targetX, playerZ].side == playerSide // checks for target panel's side (red or blue) matching player
					&& Battlefield.Field [targetX, playerZ].load == 0 // checks if target is empty
					&& Battlefield.Field [targetX, playerZ].maxLoad >= weight) {// checks if the panel can handle the weight
					prevPanel = Battlefield.Field[playerX,playerZ];
					Battlefield.Field [playerX, playerZ].load = 0;//empties load of old panel
					playerX = targetX;// sets new column (x) position
					playerUnit.transform.position = new Vector3 (Battlefield.Field[playerX,playerZ].panelCord.x,transform.position.y,Battlefield.Field[playerX,playerZ].panelCord.z);// moves player to panel
					animator.SetTrigger ("playerMove");//plays move animation
					Battlefield.Field [playerX, playerZ].load = weight;//loads weight to new panel
					CheckForCrack();
					Debug.Log ("Moved Right");
					moveFresh = refreshM - (speed * 2);//Frames the player needs to wait till they move again
					return;// end it here if moved
				}
			}

		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			Debug.Log (playerUnit.name + " Moving Down!");
			if (playerZ > 0) {// row positioning(z)
				targetZ = playerZ - 1;// Down is -1, Up is +1.[0-2]
				if (Battlefield.Field [playerX, targetZ].side == playerSide && Battlefield.Field [playerX, targetZ].load == 0 && Battlefield.Field [playerX, targetZ].maxLoad >= weight ) {
					prevPanel = Battlefield.Field[playerX,playerZ];
					Battlefield.Field [playerX, playerZ].load = 0;
					playerZ = targetZ;
					playerUnit.transform.position = new Vector3 (Battlefield.Field[playerX,playerZ].panelCord.x,transform.position.y,Battlefield.Field[playerX,playerZ].panelCord.z);
					animator.SetTrigger ("playerMove");
					Battlefield.Field [playerX, playerZ].load = weight;
					CheckForCrack ();
					Debug.Log ("Moved Left");
					moveFresh = refreshM - (speed * 2);
					return;
				}
			}
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			Debug.Log (playerUnit.name + " Moving Left!");
			if (playerX > 0) {
				targetX = playerX - 1;
				if (Battlefield.Field [targetX, playerZ].side == playerSide && Battlefield.Field [targetX, playerZ].load == 0 && Battlefield.Field [targetX, playerZ].maxLoad >= weight) {
					prevPanel = Battlefield.Field[playerX,playerZ];
					Battlefield.Field [playerX, playerZ].load = 0;
					playerX = targetX;
					playerUnit.transform.position = new Vector3 (Battlefield.Field[playerX,playerZ].panelCord.x,transform.position.y,Battlefield.Field[playerX,playerZ].panelCord.z);
					animator.SetTrigger ("playerMove");
					Battlefield.Field [playerX, playerZ].load = weight;
					CheckForCrack ();
					Debug.Log ("Moved Left");
					moveFresh = refreshM - (speed * 2);
					return;
				}
			}
		} 

		if (Input.GetKey(KeyCode.UpArrow)) {
			Debug.Log (playerUnit.name + " Moving Up!");
			if (playerZ < 2) {
				targetZ = playerZ + 1;
				if (Battlefield.Field [playerX, targetZ].side == playerSide && Battlefield.Field [playerX, targetZ].load == 0 && Battlefield.Field [playerX, targetZ].maxLoad >= weight) {
					prevPanel = Battlefield.Field[playerX,playerZ];
					Battlefield.Field [playerX, playerZ].load = 0;
					playerZ = targetZ;
					playerUnit.transform.position = new Vector3 (Battlefield.Field[playerX,playerZ].panelCord.x,transform.position.y,Battlefield.Field[playerX,playerZ].panelCord.z);
					animator.SetTrigger ("playerMove");
					Battlefield.Field [playerX, playerZ].load = weight;
					CheckForCrack ();
					Debug.Log ("Moved Left");
					moveFresh = refreshM - (speed * 2);
					return;
				}
			}
		} 


	}
		
}