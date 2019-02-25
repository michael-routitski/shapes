using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierManager : MonoBehaviour {

	public int NumLanes;

	public int Speed;

	public int StepSpanLength;

	float currentBarrierZ = 0;
	
	float nextBarrierZ = 0;

	private int CurrentStep;

	private GameObject player;

	private Queue<BarrierController> barrierManagersQueue;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");

		InitBarriers();
	}

	void InitBarriers(){

		var barrierControllers = GetComponentsInChildren<BarrierController>();

		barrierManagersQueue = new Queue<BarrierController>(barrierControllers.Length);

		for (int i = 0; i < barrierControllers.Length; i++)
		{
			barrierControllers[i]
				.transform
				.ActivateGameObject()
				.SetZ(0);
			
			barrierControllers[i].Regenerate(StepSpanLength);

			barrierManagersQueue.Enqueue(barrierControllers[i]);
		}
	}

	void LoadNextBarrier(){

		currentBarrierZ = nextBarrierZ;
		nextBarrierZ = currentBarrierZ + StepSpanLength;

		var barrierController = barrierManagersQueue.Dequeue();

		barrierController.Regenerate(StepSpanLength);
		barrierController.transform.SetZ(nextBarrierZ);

		barrierManagersQueue.Enqueue(barrierController);
	}

	// Update is called once per frame
	void Update () {

		if (player.transform.position.z > currentBarrierZ){
			LoadNextBarrier();
		}
	}
}
