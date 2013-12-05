using UnityEngine;
using System;

public class InstructionsScreen: MonoBehaviour {
	
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
		
		//Background image
		GUI.Label(new Rect(0,0,Screen.width, Screen.height), "", bgStyle);
		
		GUI.Label(new Rect(w_center - 220,h_center - 100,500,60),"Kill them ducklins.");
		
		// NOTE: Rect(topLeftX,topLeftY,width,height)
		//Draw button
		if(GUI.Button (new Rect(Screen.width/20,(Screen.height-(Screen.height/4)),200,100), "BACK")) {
			MainGame.Instance.ShowMainMenu();
		}
	}
}

