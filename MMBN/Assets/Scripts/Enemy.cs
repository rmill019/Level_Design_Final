using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Used to keep track of if the enemy can be damaged in their current state
	public enum enemyState { normal, invincible };

	[SerializeField]
	private int					_hp;
	private int					_projectileDamageDealt;
	[SerializeField]
	private enemyState 			eState;

	void Start () {
		eState = enemyState.normal;
	}


	/******************
	 *   Properties  *
	 * ***************/
	public int HP
	{
		get { return _hp; }
		set { _hp = value; }
	}

	public int ProjectileDamageDealt
	{
		get { return _projectileDamageDealt; }
		set { _projectileDamageDealt = value; }
	}

	public enemyState EState
	{
		get { return eState; }
		set { eState = value; }
	}
}
