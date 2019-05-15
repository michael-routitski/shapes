using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    const int BLOCKS_PER_LEVEL = 2;
    const int BLOCKS_PER_STAGE = 10;

    const int INIT_NUM_LANES = 2;
    const int MAX_NUM_LANES = 8;

    private LevelHUDManager UI = null;

    int CurrentLevel = 1;

    int NumLanes = INIT_NUM_LANES;

    //int CurrentBlocksRemaining = 10;

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
        UI = GetComponentInChildren<LevelHUDManager>(true);
        cameraFollow = GetComponentInChildren<CameraFollow>(true);
        player = GetComponentInChildren<PlayerController>(true);
        barriers = GetComponent<LevelBarrierManager>();

        NumLanes = INIT_NUM_LANES;
        CurrentScore = -1; // -1 cause the first pass is triggered before any actuall pass, in the collider preceeding the barrier.
        CurrentLevel = 1;
        //CurrentBlocksRemaining = BLOCKS_PER_LEVEL;

        player.Reset(NumLanes);

        barriers.Reset(NumLanes);

        UpdateUI();

        GetComponentInChildren<CameraFollow>().Reset();
    }

    public void HandleSugarCubePass() {
        
        CurrentScore++;
        //CurrentBlocksRemaining--;
        if (CurrentScore > 0 && CurrentScore % BLOCKS_PER_LEVEL == 0){
            
            if (CurrentScore % (BLOCKS_PER_LEVEL * BLOCKS_PER_STAGE) == 0){

                if (NumLanes < MAX_NUM_LANES){
                NumLanes++;
                }
                else{
                    NumLanes = INIT_NUM_LANES;
                }

                player.Reset(NumLanes);

                player.ResetForwardSpeed();

                barriers.Reset(NumLanes);

            }
            else{
                player.IncreaseForwardSpeed();
            }
            

            CurrentLevel++;
        }

        UpdateUI();
    }

    public void HandleSugarCubeCrash() {
        SendMessageUpwards("EndLevel", CurrentScore, SendMessageOptions.RequireReceiver);
    }
    
    void UpdateUI(){

        //UI.SetTotal(BlocksTotal);
        UI.SetLevel(CurrentLevel);
        //UI.SetProgress(BLOCKS_PER_LEVEL - CurrentLevel % BLOCKS_PER_LEVEL);
        UI.SetScore(CurrentScore);
    }

}
