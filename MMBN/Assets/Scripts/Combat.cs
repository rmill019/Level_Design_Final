using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	// Attack type enum
	public enum eAttackType  {
		blaster,
		bomb,
		melee
	}

	[Header("Weapon Settings")]
	public GameObject			projectilePrefab;
	public float				projectileXOffset;
	public float				projectileYOffset;
	public GameObject			bombPrefab;
	private Vector3				_spawnPos;
	private GameObject 			meleeTrigger;


	// Use this for initialization
	void Start () {

		// Grab the MeleeTrigger attached to this Unit
		meleeTrigger = GameObject.Find ("MeleeTrigger");;
		// Set meleeTrigger to inactive when it is created
		meleeTrigger.gameObject.SetActive (false);

		// Check to see that meleeTrigger was found
		if (meleeTrigger == null)
			Debug.LogError ("No MeleeTrigger is attached to " + this.gameObject.name);
		else if (meleeTrigger != null)
			print ("MeleeTrigger found");
		
	}
	
	// Update is called once per frame
	void Update () {
		// This is the position where ammo should initially spawn so that it does not instantiate inside of the character
		// but instead in a more natural looking position in front of the character.
		_spawnPos = transform.position + new Vector3 (projectileXOffset, projectileYOffset, 0);
	}


	public void Attack (eAttackType eAttack, string tag)
	{
		GameObject tGO;
		switch (eAttack) 
		{
		case eAttackType.blaster:
				//Projectile spawning
			if (tag == "Enemy") {
				Debug.Log ("Blaster Attack Initiated");
				tGO = Instantiate (projectilePrefab, _spawnPos, Quaternion.identity);
				tGO.GetComponent<Projectile> ().xVelocity *= -1;
				tGO.GetComponent<SpriteRenderer> ().flipX = true;
			} 
			else
			{
				Debug.Log ("Blaster Attack Initiated");
				tGO = Instantiate (projectilePrefab, _spawnPos, Quaternion.identity);
			}
			break;

		case eAttackType.bomb:
			// Bomb spawning
			if (tag == "Enemy") {
				Debug.Log ("Bomb Attack Initiated");
				tGO = Instantiate (bombPrefab, _spawnPos, Quaternion.identity);
				// Grab the bomb component attached to tGO
				Bomb bomb = tGO.GetComponent<Bomb> ();
				// Set it's PanelRange value to EnemyPanelRange;
				bomb.PanelRange = bomb.EnemyPanelRange;
				projectileXOffset *= -1;
				tGO.GetComponent<SpriteRenderer> ().flipX = true;
			} 
			else 
			{
				Debug.Log ("Blaster Attack Initiated");
				tGO = Instantiate (bombPrefab, _spawnPos, Quaternion.identity);
				// Grab the bomb component attached to tGO
				Bomb bomb = tGO.GetComponent<Bomb> ();
				// Set it's PanelRange value to PlayerPanelRange;
				bomb.PanelRange = bomb.PlayerPanelRange;
			}
			break;

		case eAttackType.melee:
			// MeleeTrigger Spawning
			Debug.Log ("Melee Attack Initiated");
			// Activate the MeleeTrigger attached to this Unit
			// Grab the Melee Script attached to meleeTrigger and call the SetLifetime function
			meleeTrigger.GetComponent<Melee> ().SetLifetime ();
			break;
		}
	}
}
