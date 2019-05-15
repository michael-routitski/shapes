using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

 	int NumLanes;

	public float InitialForwardSpeed;

	public float SpeedIncreaseFactor;

	public float currentForwardSpeed;

	float[] lanesX;

	const float BlockWidth = 2.4f;

	public float InputH;

	public float InputV;

	public bool IsHorizontalMoveInProgress = false;

	public bool IsRotationInProgress = false;

	float NumHorizontalSteps = 12f;

	int currentLaneIndex = 0;

	int targetLaneIndex = 0;

	bool CanMove = true;

	ShapeGenerator shape;

	public void SetNumLanes(int lines){
		NumLanes = lines;
		InitLanes();
	}

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
		transform.Translate(0, 0, currentForwardSpeed);
	}

	void Rotate(){

		if (!CanMove) return;

		IsRotationInProgress = true;

		StartCoroutine(RotateCR());
	}

	IEnumerator RotateCR(){

		var stepZ = 90f / NumHorizontalSteps;

		for (int i=0; i<NumHorizontalSteps; i++){	

			shape.transform.Rotate(0, 0, stepZ);
			
			yield return new WaitForFixedUpdate();
		}
		
		//yield return new WaitForFixedUpdate();

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

		while(currentForwardSpeed > 0.001){
			currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, 0, haltSpeed);
			yield return new WaitForFixedUpdate();
		}

		currentForwardSpeed = 0;

		yield return new WaitForSeconds(1);

		//yield return new WaitForSeconds(0.33f);
		SendMessageUpwards("HandleSugarCubeCrash");

		yield return null;
	}

	public void Reset (int numLanes) {

		NumLanes = numLanes;

		if (!shape){
			shape = GetComponentInChildren<ShapeGenerator>();
		}
		
		shape.Reset();

		shape.transform.rotation = Quaternion.identity;

		InitLanes();

		currentLaneIndex = lanesX.Length / 2;

		transform.position = new Vector3(lanesX[currentLaneIndex], transform.position.y, -25f);

		IsHorizontalMoveInProgress = false;
		IsRotationInProgress = false;
		CanMove = true;
		currentForwardSpeed = InitialForwardSpeed;
	}

	public void IncreaseForwardSpeed(){
		currentForwardSpeed = currentForwardSpeed * SpeedIncreaseFactor;
	}

	public void ResetForwardSpeed(){
		currentForwardSpeed = this.InitialForwardSpeed;
	}

	// Update is called once per frame
	void FixedUpdate () {

		InputH = Input.GetAxis("Horizontal");
		InputV = Input.GetAxis("Vertical");

		MoveForward();

		if (!IsHorizontalMoveInProgress){
			if (InputH > 0.05f)
				MoveRight();
			else if (InputH < -0.05f)
				MoveLeft();
		}

		if (!IsRotationInProgress){
			if (InputV > 0.2f){
				IsRotationInProgress = true;
				Rotate();
			}
		}
	}
}
