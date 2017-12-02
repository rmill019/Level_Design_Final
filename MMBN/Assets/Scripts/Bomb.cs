using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile {
	
	protected Vector3			p0;
	protected Vector3			p1;
	protected Vector3			p2;
	protected PlayerControls 	playerControl;
	protected Transform 		playerTrans;

	[Header("This section defines the bomb's properties")]
	[Tooltip("How many panels to the right ('Front') will the bomb travel.")]
	public int 					panelRange;
	public int 					movementDuration;
	public float 				bombThrowHeight;

	protected float 			timeBeforeExplosion;		// This should match movementDuration if we want it to explode on impact.
	[Tooltip("This is how much will be subracted from the bombs final z position. Needed for cleaner appearance")]
	public float 				bombZOffset;

	private bool				isMoving;
	private float 				u;
	private float 				timeStartExplosion;
	private Animator 			anim;

	protected override void Awake () {
		rigid = GetComponent<Rigidbody> ();
		boxColl = GetComponent<BoxCollider> ();
		// TODO need to check if we're spawning from Player or Enemy
		playerControl = GameObject.Find ("PlayerUnit_Basic").GetComponent<PlayerControls>();;

	}

	// Use this for initialization
	void Start () {
		timeBeforeExplosion = movementDuration;
		playerTrans = GameObject.Find ("PlayerUnit_Basic").GetComponent<Transform> ();
		boxColl.isTrigger = true;
		rigid.useGravity = false;
		isMoving = true;
		anim = GetComponent<Animator> ();

		// These two are inherited from Base class "Projectile"
		startTime = Time.time;
		duration = startTime + lifeTime;

		// Sets the time when the bomb will play explosion animation
		timeStartExplosion = startTime + timeBeforeExplosion;

		// TODO Add a system to make sure that timeBefore explosion and base.duration are not equal because
		// then the bomb will explode and disappear (Destroy) at the same time

		// Set the positions that the bomb will travel once it's spawned
		p0 = playerTrans.position;				// This is the initial position which is where the player is currently located
		p0.y = playerControl.projectileOffset;
		// TODO What happens if we're at the edge of the field?
		// p2 is the final location where the bomb will end up which depends on value given to the variable "panelRange"
		p2 = new Vector3 (Battlefield.Field [playerControl.playerX + panelRange, playerControl.playerZ].panelCord.x, transform.position.y, Battlefield.Field[playerControl.playerX, playerControl.playerZ].panelCord.z);
		p2.z -= bombZOffset;					// Final z position of the bomb must be modified to give it a cleaner appearance on the battlefield
		p1 = (p2 - p0) / 2;						// p1 is the midway point between p0 and p2
		p1.y = bombThrowHeight;					// We want an arc on the bomb so the y-value of the midway point needs to be raised by the value indicated with "bombThrowHeight" variable.
	}
	
	// Update is called once per frame
	void Update () {
	
		BombThrow (p0, p1, p2);
		CheckLifetime ();
		
	}

	/*****************************
	 * 	User Defined Functions   *
	 * **************************/
	// This function is using 3-pt interpolation to give the bomb arc a smooth movement
	void BombThrow (Vector3 pos0, Vector3 pos1, Vector3 pos2)
	{
		if (isMoving)
		{
			// TODO Movement needs to stop once the position is reached. But this should stop on the
			// correct panel due to how we grab pos2 in Start ()
			u = (Time.time - startTime) / movementDuration;
			if (u >= 1)
			{
				isMoving = false;
			}
		}

		// 3 Point Bezier Curve Calculation
		Vector3 p01, p12, p012;

		p01 = (1 - u) * pos0 + u * pos1;
		p12 = (1 - u) * pos1 + u * pos2;

		// Final position?
		p012 = (1 - u) * p01 + u * p12;

		// Set the final position on this gameObject;
		transform.position = p012;

		// TODO Set the bomb to start ticking down and flickering to explode

	}


	public override void CheckLifetime ()
	{
		// If it is Time for the bomb to explode then play the clip named "explosionBomb"
		if (Time.time >= timeStartExplosion)
		{
			anim.CrossFade ("explosionBomb", 0f);
			anim.speed = 1;
		}
		// Call the Base class CheckLifetime function to see if it should be Destroyed.
		base.CheckLifetime ();
	}
}
