using UnityEngine;
using System;

public class MainMenuScreen: MonoBehaviour {
	
	public GUISkin skin;
	public Texture2D backdrop;
	GUIStyle bgStyle = new GUIStyle();
	
	public void Update(){
		if (Input.GetKey(KeyCode.Escape)) Application.Quit(); // end game when Escape is pressed
	}
	
	void OnGUI(){
		GUI.skin = skin;
		bgStyle.normal.background = backdrop;
		
		int w_center = (Screen.width/2);
		int h_center = (Screen.height/2);
		int w_double = (2*(Screen.width));
		int h_triple = (3*(Screen.height));
		
		//Background image
		GUI.Label(new Rect(0,0,Screen.width, Screen.height), "", bgStyle);
		
		// NOTE: Rect(topLeftX,topLeftY,width,height)
		//Draw buttons
		if(GUI.Button (new Rect ((Screen.width - (Screen.width/2)),(h_center+180),(Screen.width/3 - 50) ,65),
					   "START GAME")) {
			MainGame.Instance.StartGame();
		}
		else if(GUI.Button (new Rect((Screen.width - (Screen.width/2)),(h_center+255),(Screen.width/3 - 50) ,65),
							"INSTRUCTIONS")) {
			MainGame.Instance.ShowInstructions();
		}
	}
}

