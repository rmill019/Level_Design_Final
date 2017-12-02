using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	protected Rigidbody				rigid;
	protected BoxCollider 			boxColl;
	protected Transform				parent;
	protected SpriteRenderer 		parentRend;

	// Make this a property?
	public float					xVelocity = 50f;
	[SerializeField]
	protected int 					damage;				// How much damage this instance of projectile deals

	// Time properties
	protected float					startTime;
	protected float					endTime;
	protected float					duration;
	public float					lifeTime;


	protected virtual void Awake () {
		rigid = GetComponent<Rigidbody> ();
		boxColl = GetComponent<BoxCollider> ();
	}

	// Use this for initialization
	void Start () {
		boxColl.isTrigger = true;
		rigid.useGravity = false;
		startTime = Time.time;
		duration = startTime + lifeTime;

//		// If the parent GameObject sprite is flipped on the x-axis then the projectile must move left
//		if (parentRend.flipX == true)
//		{
//			xVelocity *= -1;
//		}
		
		// Move the projectile as soon as it's loaded
		rigid.velocity = new Vector3 (xVelocity, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckLifetime ();
		
	}

	void OnTriggerEnter (Collider coll)
	{
		// If a projectile has hit an Enemy
		if (coll.gameObject.tag == "Enemy" && this.gameObject.tag == "Ammo")
		{
			Destroy (this.gameObject);
		}
	}


	/*****************************
	 *       Properties			*
	 * *************************/

	public int Damage
	{
		get { return damage; }
		set { damage = value; }
	}


	/***************************
	 * 	User defined functions *
	 * ************************/
	public virtual void CheckLifetime ()
	{
		if (Time.time >= duration)
		{
			Destroy (this.gameObject);
		}
	}
}
