using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierManager : MonoBehaviour {

	int NumLanes;

	public int Speed;

	public int StepSpanLength;

	public float currentBarrierZ = 0;
	
	public float nextBarrierZ = 0;

	public Material[] ChocolateMaterials;

	private GameObject player;

	private Queue<BarrierController> barrierManagersQueue;

	// Use this for initialization
	public void Reset(int numLanes) {

		NumLanes = numLanes;

		Debug.Log("LevelBarrierManager.Reset");

		player = GameObject.FindWithTag("Player");

		currentBarrierZ = 0;
		nextBarrierZ = 0;

		

		InitBarriers();
		LoadNextBarrier();
		LoadNextBarrier();
	}

	public void SetNumLanes(int numLanes){
		foreach (var item in barrierControllers)
		{
			item.SetNumLanes(numLanes);
		}
	}

	BarrierController[] barrierControllers;

	void InitBarriers(){

		barrierControllers = GetComponentsInChildren<BarrierController>();

		barrierManagersQueue = new Queue<BarrierController>(barrierControllers.Length);

		for (int i = 0; i < barrierControllers.Length; i++)
		{
			barrierControllers[i].Reset(NumLanes);

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
