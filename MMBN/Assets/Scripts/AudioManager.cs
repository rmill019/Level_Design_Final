using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager 		S;
	public AudioClip 				gameOver;
	public AudioClip 				gameWon;

	void Awake () {
		if (S == null)
		{
			S = this;
		}
		else
		{
			Destroy (this);
		}
			
		S.GetComponent<AudioSource> ().Play ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
