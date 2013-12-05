using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoreScreen: MonoBehaviour {
	
	public GUISkin skin;
	public Texture2D backdrop;
	GUIStyle bgStyle = new GUIStyle();
	
	int totalYellow = 0;
	int totalBlue = 0;
	
	string yLevel1 = "--";
	string yLevel2 = "--";
	string yLevel3 = "--";
	string bLevel1 = "--";
	string bLevel2 = "--";
	string bLevel3 = "--";
	
	public void Update(){
		if (Input.GetKey(KeyCode.Escape)) Application.Quit(); // end game when Escape is pressed
	}
	
	void Start() {
		if(MainGame.Instance.YellowLevel1 != -1) { yLevel1 = MainGame.Instance.YellowLevel1.ToString(); totalYellow += MainGame.Instance.YellowLevel1; }
		if(MainGame.Instance.YellowLevel2 != -1) { yLevel2 = MainGame.Instance.YellowLevel2.ToString(); totalYellow += MainGame.Instance.YellowLevel2;}
		if(MainGame.Instance.YellowLevel3 != -1) { yLevel3 = MainGame.Instance.YellowLevel3.ToString(); totalYellow += MainGame.Instance.YellowLevel3;}
		if(MainGame.Instance.BlueLevel1 != -1) { bLevel1 = MainGame.Instance.BlueLevel1.ToString(); totalBlue += MainGame.Instance.BlueLevel1;}
		if(MainGame.Instance.BlueLevel2 != -1) { bLevel2 = MainGame.Instance.BlueLevel2.ToString(); totalBlue += MainGame.Instance.BlueLevel2;}
		if(MainGame.Instance.BlueLevel3 != -1) { bLevel3 = MainGame.Instance.BlueLevel3.ToString(); totalBlue += MainGame.Instance.BlueLevel3;}
		
		StartCoroutine(wait ());
	}
	
	IEnumerator wait() {
		yield return new WaitForSeconds(4);
		print ("Broadcasting AdvanceLevel");
		Messenger.Broadcast("AdvanceLevel");
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
		
		
		GUI.Box(new Rect(250,205,350,60),new GUIContent("Yellow"));
		GUI.Box(new Rect(750,205,350,60),new GUIContent("Pink"));
		
		GUI.Box(new Rect(250,405,350,60),new GUIContent(yLevel1));
		GUI.Box(new Rect(750,405,350,60),new GUIContent(bLevel1));
		GUI.Box(new Rect(250,475,350,60),new GUIContent(yLevel2));
		GUI.Box(new Rect(750,475,350,60),new GUIContent(bLevel2));
		GUI.Box(new Rect(250,545,350,60),new GUIContent(yLevel3));
		GUI.Box(new Rect(750,545,350,60),new GUIContent(bLevel3));
		GUI.Box(new Rect(250,615,350,60),new GUIContent(totalYellow.ToString()));
		GUI.Box(new Rect(750,615,350,60),new GUIContent(totalBlue.ToString()));
	}
}

