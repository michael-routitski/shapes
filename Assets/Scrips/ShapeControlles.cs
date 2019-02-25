using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeControlles : MonoBehaviour {

 	public float forwardSpeed = 0.1f;
    public float sidewaysSpeed = 0.2f;

	Vector3 finalPosition;

	private enum Direciton { Left, Right };

	public float CurrentX = 0;

	public float TargetX = 0;

	public float InputX;

	public float stepX; // 2

	public float boundX; // e.g. 7  as in -7, -5, -3, -1, 1, 3, 5, 7  for the max tracks

	void SetCurrentTarget() { 

		CurrentX = transform.position.x;

		TargetX = (InputX < 0) ? (CurrentX - stepX) : (CurrentX + stepX);
		 
		TargetX = Mathf.Clamp(TargetX, -boundX, boundX);

		Debug.Log("Target set at " + TargetX);
	}

	void Start () {
		finalPosition = transform.position;
		//currentSidewaysDirection = 0;
	}
	
	float speed = 0.2f;

	// Update is called once per frame
	void FixedUpdate () {
		
		//MoveForward();

		finalPosition = transform.position;

		// forward =
		finalPosition.z = finalPosition.z + forwardSpeed;

		if (CanProcessNextInput()){
			InputX = Input.GetAxis("Horizontal");

			if (InputX != 0)
				SetCurrentTarget();

		}
		
		if (finalPosition.x != TargetX){

			finalPosition.x += InputX > 0 ? sidewaysSpeed : -sidewaysSpeed; 

			if (Mathf.Abs(finalPosition.x -TargetX) < sidewaysSpeed){
				finalPosition.x = TargetX;
			}	
		}
		
		transform.position = finalPosition;

	}

	bool CanProcessNextInput(){
		return (
			transform.position.x == -6f ||  
			transform.position.x == -3f ||  
			transform.position.x == -0f ||  
			transform.position.x == 3f ||  
			transform.position.x == 6f);
	}

	void MoveForward(){
		transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
	}

	void MoveSideways(){
		if (TargetX != CurrentX){

		}
	}

	bool isMovingSideways = false;

	IEnumerator MoveSidewaysCR() 
	{
		isMovingSideways = true;

		if(TargetX < CurrentX){
			while(transform.position.x > TargetX){
				transform.Translate(-0.25f, 0, 0);
				yield return new WaitForEndOfFrame();
			}
		}
		else {
			while(transform.position.x < TargetX){
				transform.Translate(0.25f, 0, 0);
				yield return new WaitForEndOfFrame();
			}
		}

		isMovingSideways = false;

		yield return null;
	}

}
