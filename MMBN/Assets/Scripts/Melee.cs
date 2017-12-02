using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

	[SerializeField]
	protected int				damage;
    private float 				_timeActivated;
	[SerializeField]
	private float				lifetimeDuration;
	private float 				_timeToDeactvate;
	private Collider 			_collider;


	void Awake () {
	}

	// Use this for initialization
	void Start () {
		_collider = GetComponent<Collider> ();
		_collider.isTrigger = true;				// We handle all our damage through OnTriggerEnter so we want this to be a Trigger not a Collider

	}
	
	// Update is called once per frame
	void Update () {
		CheckLifetime ();
		
	}


	/*********************************
	 *     Self Defined Functions    *
	 * *******************************/
	public void CheckLifetime ()
	{
		if (Time.time >= _timeToDeactvate)
		{
			this.gameObject.SetActive (false);
		}
	}


	public void SetLifetime ()
	{
		this.gameObject.SetActive (true);
		_timeActivated = Time.time;
		_timeToDeactvate = _timeActivated + lifetimeDuration;
	}


	/*****************
	 *   Properties  *
	 * **************/
	public int Damage 
	{
		get { return damage; }
		set { damage = value; }
	}

	public Collider colld
	{
		get { return _collider; }
	}

}
