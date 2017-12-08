using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health_Bar : MonoBehaviour {

	public Transform 				health_Text;
	public Transform 				healthBar;		
	public int 						damage;
	// Health Bar GameObject (NOT THE SPRITE)
	[SerializeField] private float 	_health;	// Current amount of Health to start with.
	[SerializeField] private float 	_speed;			// Speed that Health fills/drains.
	private float					_healthAfterDamage;

	[Header("Set Reference to Player")]
	[SerializeField]
	private PlayerControls 			_player;

	void Start () {
		Health = _player.HP;
		HealthAfterDamage = Health;
		Debug.LogWarning ("Player Health is set to: " + Health);
		
	}


	// Update is called once per frame
	void Update () {

		if (HealthAfterDamage < Health) {
			//current amount of health fills/drains in deltaTime
			Health -= _speed * Time.deltaTime;
			// Health Text component updates and 
			health_Text.GetComponent<Text> ().text = "Health: " + ((int)Health + 1.0).ToString ();		// Health + 1.0 is necessary because the float rounds down and prints an inconsistent value.
		} 
		else
		{
			
		}
		//Health bar Image updates. Since the Fill amount in the image component
		//only goes from 0 - 1, the fill amount has to be currentamount divided
		//by 100.
		healthBar.GetComponent<Image>().fillAmount = _health/100;
		if (Input.GetKeyDown (KeyCode.D))
		{
			HealthAfterDamage -= 10;
			_player.HP -= 10;

			// TODO Ask Emil how he handles Destroying the player. This function may be better off being placed there.
			if (_player.HP <= 0)
			{
				LevelManager.S.LoadLevel (1);
			}
		}

	}

	// Properties

	public float HealthAfterDamage
	{
		get { return _healthAfterDamage; }
		set { _healthAfterDamage = value; }
	}

	public float Health
	{
		get { return _health; }
		set { _health = value; }
	}
}
