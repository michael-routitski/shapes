using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	public int NumLanes;

	public float ForwardSpeed;

	float[] lanesX;

	const float BlockWidth = 2f;

	int currentLaneIndex = 0;

	int targetLaneIndex = 0;

	public float InputH;

	public float InputV;

	public bool IsHorizontalMoveInProgress = false;

	public bool IsRotationInProgress = false;

	Transform Shape;

	bool CanMove = true;

	ShapeGenerator shapeGenerator;

	void InitLanes(){
		
		lanesX = new float[NumLanes];
		
		for (int i = 0; i < NumLanes; i++){
			lanesX[i] = GetXFor(i);
		}
	}

	float GetXFor(int index){

		return -(float)(NumLanes * BlockWidth) / 2.0f + (float)index * (BlockWidth) + BlockWidth / 2.0f;
	}
 
	void MoveForward(){
		transform.Translate(0, 0, ForwardSpeed);
	}

	void Rotate(){

		if (!CanMove) return;

		IsRotationInProgress = true;

		StartCoroutine(RotateCR());
	}

	IEnumerator RotateCR(){

		var stepZ = 90f / NumHorizontalSteps;

		for (int i=0; i<NumHorizontalSteps; i++){	

			Shape.transform.Rotate(0, 0, stepZ);
			
			yield return new WaitForFixedUpdate();
		}
		
		IsRotationInProgress = false;

		yield return null;
	}

	void MoveLeft(){

		if (!CanMove) return;

		if (currentLaneIndex == 0) return;

		targetLaneIndex = currentLaneIndex - 1;

		IsHorizontalMoveInProgress = true;

		StartCoroutine(MoveXCR(true));
	}

	void MoveRight(){

		if (!CanMove) return;

		if (currentLaneIndex == lanesX.Length - 1) return;

		targetLaneIndex = currentLaneIndex + 1;

		IsHorizontalMoveInProgress = true;

		StartCoroutine(MoveXCR(false));
	}

	public float NumHorizontalSteps = 20f;

	IEnumerator MoveXCR(bool left){

		var stepX = (float)BlockWidth / NumHorizontalSteps;

		for (int i=0; i<NumHorizontalSteps; i++){	

			if (left){
				transform.Translate(-stepX, 0, 0);
			}
			else {
				transform.Translate(stepX, 0, 0);
			}
			yield return new WaitForFixedUpdate();
		}
		
		currentLaneIndex = targetLaneIndex;
		
		IsHorizontalMoveInProgress = false;

		yield return null;
	}

	public void BarrierHit(){
		CanMove = false;

		StartCoroutine(BarrierHitCR());
	}

	IEnumerator BarrierHitCR(){

		

		GetComponentInChildren<ShapeGenerator>().DeparentCubes(50f);

		float haltSpeed = Random.Range(0.1f, 0.18f);

		while(ForwardSpeed > 0.001){
			ForwardSpeed = Mathf.Lerp(ForwardSpeed, 0, haltSpeed);
			yield return new WaitForFixedUpdate();
		}

		ForwardSpeed = 0;

		//yield return new WaitForSeconds(0.33f);
		SendMessageUpwards("HandleSugarCubeCrash");

		yield return null;
	}

	// Use this for initialization
	public void Reset () {
		InitLanes();
		transform.position = new Vector3(lanesX[currentLaneIndex], transform.position.y, transform.position.z);
		Shape = transform.Find("Shape");
		transform.position = new Vector3(0,0,-20f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		InputH = Input.GetAxis("Horizontal");
		InputV = Input.GetAxis("Vertical");

		MoveForward();

		if (!IsHorizontalMoveInProgress){
			if (InputH > 0f)
				MoveRight();
			else if (InputH < 0)
				MoveLeft();
		}

		if (!IsRotationInProgress){
			if (InputV > 0)
				Rotate();
		}
	}
}
