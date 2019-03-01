using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    const int BLOCKS_PER_LEVEL = 2;

    const int INIT_NUM_LANES = 5;
    const int MAX_NUM_LANES = 8;

    private LevelUIManager UI = null;

    int CurrentLevel = 1;

    int NumLanes = INIT_NUM_LANES;

    int CurrentBlocksRemaining = 10;

    int BlocksTotal = 10;

    int CurrentScore = 0;

    CameraFollow cameraFollow;

    PlayerController player;

    LevelBarrierManager barriers;

    void Update(){
        if (cameraFollow){
            cameraFollow.RunUpdate();
        }
    }

    public void StartLevel(){
        UI = GetComponentInChildren<LevelUIManager>(true);
        cameraFollow = GetComponentInChildren<CameraFollow>(true);
        player = GetComponentInChildren<PlayerController>(true);
        barriers = GetComponent<LevelBarrierManager>();

        NumLanes = INIT_NUM_LANES;
        CurrentScore = 0;
        CurrentLevel = 1;
        CurrentBlocksRemaining = BLOCKS_PER_LEVEL;

        player.Reset(NumLanes);

        barriers.Reset(NumLanes);

        UpdateUI();

        GetComponentInChildren<CameraFollow>().Reset();
    }

    public void HandleSugarCubePass() {
        
        CurrentScore++;
        CurrentBlocksRemaining--;
        if (CurrentBlocksRemaining <= 0){
            CurrentLevel++;
            CurrentBlocksRemaining = BLOCKS_PER_LEVEL;

            if (NumLanes < MAX_NUM_LANES){
                NumLanes++;
            }
            else{
                NumLanes = INIT_NUM_LANES;
            }

            player.Reset(NumLanes);

            barriers.Reset(NumLanes);
        }

        UpdateUI();
    }

    public void HandleSugarCubeCrash() {
        SendMessageUpwards("EndLevel", CurrentScore, SendMessageOptions.RequireReceiver);
    }
    
    void UpdateUI(){

        UI.SetTotal(BlocksTotal);
        UI.SetLevel(CurrentLevel);
        UI.SetProgress(CurrentBlocksRemaining);
        UI.SetScore(CurrentScore);
    }

}
