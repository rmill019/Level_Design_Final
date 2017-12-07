using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static int FrameRate = 60;
	public int ScreenWidth;
	public int ScreenLength;
	

	void Awake(){
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = FrameRate;
	}

	// Use this for initialization
	void Start () {
		Screen.SetResolution (ScreenWidth,ScreenLength,true);

		Debug.Log ("Screen size set: "+ ScreenWidth + " x " + ScreenLength);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
