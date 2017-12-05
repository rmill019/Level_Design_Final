using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {
	[Tooltip("This should be a small float value")]
	public float 					amountToFade;
	public float					timesToFlicker;

	private Renderer				rend;
	private Enemy					enemy;
	private Projectile				projectile;

	// Only way to be able to call StopCoroutine in Unity
	private Coroutine				flickerCoroutine;

	// Use this for initialization
	void Start () {
		// The Renderer is attached to the child of the root Transform of the GameObject
		rend = transform.GetComponentInChildren<Renderer> ();
		flickerCoroutine = null;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// All Damage for the prototype happens when OnTriggerEnter() is called
	void OnTriggerEnter (Collider coll)
	{
		// Switch based on the tag that the gameObject tied to the collider entering the trigger has
		switch (coll.gameObject.tag)
		{
		// TODO Will this work when an enemy fires at player. Should we put a tag for Player Ammo and Enemy Ammo
		case "Ammo":
			enemy = this.gameObject.GetComponent<Enemy> ();
			projectile = coll.gameObject.GetComponent<Projectile> ();
			if (enemy == null || projectile == null)
			{
				Debug.LogError ("Enemy Component not found on: " + this.gameObject.name + 
					"\nOr Projectile Component not found on " + coll.gameObject.name + ".");
				return;
			}
			// Subtract the amount of damage the Projectile component does from the Enemy Component Script
			// TODO The next 10 lines are repeated for every case in this switch statement. How can we refactor it to make it cleaner?
			if (enemy.EState == Enemy.enemyState.normal)
				enemy.HP -= projectile.Damage;
			print ("Ammo hit Enemy");
			// Check if this gameObject has died (HP less than or equal to 0)
			if (enemy.HP <= 0)
			{
				Die ();
			}
			flickerCoroutine = StartCoroutine (Flicker ());
			break;

		case "Bomb":
			
			if (this.gameObject.tag == "Panel")
			{
				Debug.LogError ("Bomb hit Panel");
				Panel panel = this.gameObject.GetComponent<Panel> ();
				panel.condition = Condition.CRACKED;
			} else
			{
				Debug.LogError ("Bomb hit Enemy");
				enemy = GetComponent<Enemy> ();
				Bomb bomb = coll.gameObject.GetComponent<Bomb> ();
				// If no Enemy or Bomb Component is attached to this then Log it out and return;
				if (enemy == null || bomb == null)
				{
					Debug.LogError ("Enemy Component not found on: " + this.gameObject.name +
					"\nOr Bomb Component not found on " + coll.gameObject.name + ".");
					return;
				}
				// Subtract the amount of damage the Bomb component does from the Enemy
				// But only if they are on the normal state where damage can be done
				if (enemy.EState == Enemy.enemyState.normal)
				{
					enemy.HP -= bomb.Damage;
				}
				// Check if the gameObject attached to this Script has died (HP less than or equal to 0)
				if (enemy.HP <= 0)
				{
					Die ();
				}
				flickerCoroutine = StartCoroutine (Flicker ());
			}
			break;

		case "Melee":
			Debug.LogError ("MELEE ONTRIGGERENTER FIRED");
			enemy = GetComponent<Enemy> ();
			Melee melee = coll.gameObject.GetComponent<Melee> ();
			// If no Enemy or Melee component is attached to this gameObject or the gameObject entering the trigger volume then return
			if (enemy == null || melee == null)
			{
				Debug.LogError ("Enemy Component not found on: " + this.gameObject.name +
					"\nOr Melee Component not found on " + coll.gameObject.name + ".");
				return;
			}
			// Subtract the amount of damage the Melee component does from the Enemy
			// But only if they (Enemy) are on the normal state where damage can be done
			if (enemy.EState == Enemy.enemyState.normal)
			{
				enemy.HP -= melee.Damage;
			}
			// Check if the gameObject attached to this Script has died (HP less than or equal to 0)
			if (enemy.HP <= 0)
			{
				Die ();
			}
			flickerCoroutine = StartCoroutine (Flicker ());
			// Now set the MeleeTrigger to false so that it does not keep damaging
			melee.colld.gameObject.SetActive (false);
			break;
		}
	}
		


	/*****************************
	 *  Self Defined Functions	 *
	 * ***************************/

	public virtual void Die ()
	{
		Destroy (this.gameObject);
	}


	// This is only called if HP is still above 0
	IEnumerator Flicker ()
	{
		float alpha = 1;
		// We set the Enemy's state to invincible because while the Enemy is Flickering they should not be able to be damaged.
		enemy.EState = Enemy.enemyState.invincible;

		for (int i = 0; i < timesToFlicker; i++)
		{
			for (float f = alpha; f >= 0; f -= amountToFade)
			{
				Color c = rend.material.color;
				c.a = f;
				rend.material.color = c;
				alpha = f;
				yield return null;
			}

			for (float f = alpha; f <= 1; f += amountToFade)
			{
				Color c = rend.material.color;
				c.a = f;
				rend.material.color = c;
				alpha = f;
				yield return null;
			}
			yield return null;
		}
		// The Enemy is done Flickering so now we set his state back to normal and the Enemy will be able to take damage again. (Invincibility frames over)
		enemy.EState = Enemy.enemyState.normal;
	}
}
