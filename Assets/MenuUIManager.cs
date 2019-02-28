using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class MenuUIManager : MonoBehaviour {

	public Text CurrentScoreText;

	public Text LadderBoardText;

	public void SetCurrentScore(string message, int score){
		this.CurrentScoreText.text = string.Format("{0} {1}", message, score);
	}

	public void SetHighScores(State state, int currentScoreIndex){
		var sb = new StringBuilder();

		if (state.HighScores == null){
			this.LadderBoardText.text = "No high scores yet!";
			return;
		}

		for (int i = 0; i < state.HighScores.Count; i++){
			if (i==currentScoreIndex){
				sb.AppendLine(string.Format(
					"<color=green><b>{0}:</b> {1} ({2})</color>", 
					i, 
					state.HighScores[i].Score, 
					state.HighScores[i].Time.ToString("dd/MMM/yy")));
			}
			else{
				sb.AppendFormat(string.Format(
					"<b>{0}:</b> {1} ({2})", 
					i, 
					state.HighScores[i].Score, 
					state.HighScores[i].Time.ToString("dd/MMM/yy")));
			}
		}

		this.LadderBoardText.text =sb.ToString(); 
	}
}
