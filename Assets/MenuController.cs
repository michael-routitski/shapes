using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    MenuUIManager UI = null;

    public void Reset(){
        
    }

    public void SetScores(int? score, State state, int indexOfScoreOnLedderboard){
        UI = GetComponentInChildren<MenuUIManager>();
        
        if (score.HasValue){
            if (indexOfScoreOnLedderboard == 0){
                UI.SetCurrentScore("New Record!", score.Value);
            }
            else if (indexOfScoreOnLedderboard < 10){
                UI.SetCurrentScore("New Top 10", score.Value);
            }
            else{
                UI.SetCurrentScore("Your Score", score.Value);
            }
        }
        
        UI.SetHighScores(state, indexOfScoreOnLedderboard);
	}
}
