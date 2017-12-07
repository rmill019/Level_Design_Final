using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition{
	NORMAL,//Default state
	BROKEN,//
	CRACKED,//
}
public enum FieldSide{
	RED,
	BLUE
}

public class Panel: MonoBehaviour{
	public int row;
	public int column;
	public GameObject panel;//the physical panel
	public Vector3 panelCord;//holds the panel's cordinates to share
	//public Unit unitOverPanel;

	public Condition condition;
	private int PanelFrames;//Counts a panels active frames until they must reset condition
	private int ResetSideFrames = 1800;
	private int ResetPanelFrames = 1800;// once panel frames reach this number the panel regains it's default side/condition
	public int load;//holds the weight on the panel
	//Weights change if there are objects on them
	// normal objects weigh 2, flying weigh 1, no object weighs 0
	//If there is weight on a panel 
	public int maxLoad; // Default is 2 but broken panels hold 1
	private FieldSide startSide;//the starting side of the panel
	private FieldSide altSide;
	public FieldSide side; //deterimines who can move into the panel normally
	private int sideFrames;//counts frames for panel side changing
	private Color sideColorDefault;
	private Color sideColorAlt;


	void Awake(){ //The default initialization of the panel
		panel = gameObject;
		ResetSideFrames = GameController.FrameRate * 20;
		ResetPanelFrames = GameController.FrameRate * 20;
		panelCord = panel.transform.position;
		condition = Condition.NORMAL;
		PanelFrames = 0;
		if (column < 4 ) {//sets side colors
			startSide = FieldSide.RED;
			altSide = FieldSide.BLUE;
			sideColorDefault = Color.red;
			sideColorAlt = Color.blue;
		} else {
			startSide = FieldSide.BLUE;
			altSide = FieldSide.RED;
			sideColorDefault = Color.blue;
			sideColorAlt = Color.red;
		}
		gameObject.GetComponent<Renderer> ().material.color = sideColorDefault;
		maxLoad = 2;
		side = startSide;
		sideFrames = 0;
	}
	void PanelOperations(){// resets panels to normal after a while
		if (side != startSide) {
			gameObject.GetComponent<Renderer> ().material.color = sideColorAlt;
			side = altSide;
			if (sideFrames >= ResetSideFrames) {
				side = startSide;
				sideFrames = 0;
				gameObject.GetComponent<Renderer> ().material.color = sideColorDefault;
			} else {
				sideFrames++;
			}
		}

		if (PanelFrames >= ResetPanelFrames) {//Resets panel condition
			condition = Condition.NORMAL;
			Color tColor = GetComponent<Renderer> ().material.color;
			tColor.a = 1f;
			GetComponent<Renderer> ().material.color = tColor;
			PanelFrames = 0;
		} else {
			PanelFrames++;
		}


		switch(condition){
		case Condition.NORMAL:
			AlphaChanger (1f);
			maxLoad = 2;
			break;
		case Condition.CRACKED:
			AlphaChanger (0.5f);
			maxLoad = 2;
			break;
		case Condition.BROKEN:
			AlphaChanger (0f);
			maxLoad = 1;
			break;
		default:
			AlphaChanger (1f);
			maxLoad = 2;
			break;
		}

		if (condition == Condition.BROKEN) {
			GetComponent<Collider> ().enabled = false;
		} else {
			GetComponent<Collider> ().enabled = true;
		}
	}//Has the panel reset itself and it's frames
	/*void PanelConditionChange(){

	}*///the panel's operations when changing// sets panelframes to 0
	/*void PanelSideChange(){

	}*///has the panel's side changeF


	public void CrackPanel(){//cracks panel
		condition = Condition.CRACKED;
		AlphaChanger (0.5f);
		maxLoad = 2;
		PanelFrames = 0;
	}

	public void BreakPanel(){//breaks panel
		condition = Condition.BROKEN;
		AlphaChanger (0f);
		maxLoad = 1;
		PanelFrames = 0;
	}

	void AlphaChanger(float panelAlpha){//alt aplha
		Color tColor = GetComponent<Renderer> ().material.color;
		tColor.a = panelAlpha;
		GetComponent<Renderer> ().material.color = tColor;
	}

	void FixedUpdate(){
		PanelOperations ();
	}
}
