using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHUDManager : MonoBehaviour {

	public NumberManager LevelNumberManager;
	public NumberManager LevelScoreManager;

	public void SetLevel(int value){
		LevelNumberManager.Value = value.ToString();
	}

	public void SetScore(int value){
		LevelScoreManager.Value = value.ToString();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
