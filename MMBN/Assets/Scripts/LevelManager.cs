using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager S;

	void Awake () {
		// Set singleton instance of LevelManager
		if (S == null)
			S = this;
		else
			Destroy (this.gameObject);

		DontDestroyOnLoad (S);
	}


	public void LoadLevel (string levelName)
	{
		SceneManager.LoadScene (levelName);
	}

	public void LoadLevel (int levelIndex)
	{
		SceneManager.LoadScene (levelIndex);
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public string GetSceneName ()
	{
		return SceneManager.GetActiveScene ().name;
	}
}
