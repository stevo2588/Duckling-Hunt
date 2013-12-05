using UnityEngine;
using System;

public class GameOver: MonoBehaviour {
	
	public GUISkin skin;
	public Texture2D yellowTeamBackground;
	public Texture2D blueTeamBackground;
	GUIStyle bgStyle = new GUIStyle();
	private bool yellowWins = false;
	
	public void Update() {
		if (Input.GetKey(KeyCode.Escape)) Application.Quit(); // end game when Escape is pressed
	}
	
	void OnGUI() {
		GUI.skin = skin;
		
		//Switch background image based on yellowWins
		if (yellowWins == true) bgStyle.normal.background = yellowTeamBackground;
		else bgStyle.normal.background = blueTeamBackground;
		
		int w_center = (Screen.width/2);
		int h_center = (Screen.height/2);
		int w_double = (Screen.width*2);
		
		//Background Image
		GUI.Label(new Rect(0,0,Screen.width, Screen.height), "", bgStyle);
		
		//Game over message
		if (yellowWins == true) {
			GUI.Label(new Rect((w_center), (h_center-(Screen.height/8)), w_center, (Screen.height/16)),
					  "Yellow Wins!");
		}
		else {
			GUI.Label(new Rect((w_center-(w_double/5)),(h_center-(Screen.height/4)), w_center, (Screen.height/16)),
					   "Blue Wins!");
		}
	}
}