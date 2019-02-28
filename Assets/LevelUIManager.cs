using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

	public Text LevelText;

	public Text ProgressText;

	public Text ScoreText;

	string[] levels = {
		"Level 0",
		"Level 1",
		"Level 2",
		"Level 3",
		"Level 4",
		"Level 5",
		"Level 6",
		"Level 7",
		"Level 8",
		"Level 9"
	};

	private int totalBlocks;

	public void SetLevel(int value){
		if (value < 1 || value > levels.Length-1){
			value = 0;
		}
		this.LevelText.text = levels[value];
	}

	public void SetProgress(int value){
		this.ProgressText.text = value.ToString();
	}

	public void SetTotal(int value){
		this.totalBlocks = value;
	}

	public void SetScore(int value){
		this.ScoreText.text = value.ToString();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
