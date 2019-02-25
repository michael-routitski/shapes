using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

	BarrierGenerator generator;

	BoxCollider ShapeReformTrigger;

	public int RandomShapeIndex = -1;

	ShapeGenerator shapeGenerator;

	// Use this for initialization
	void Awake () {
		generator = GetComponent<BarrierGenerator>();
		ShapeReformTrigger = GetComponent<BoxCollider>();
		shapeGenerator = GameObject.FindWithTag("Player").GetComponentInChildren<ShapeGenerator>();
	}
	
	public void Regenerate(float stepSpanLength){

		generator.Generate();

		// Get this for player shape regen
		RandomShapeIndex = generator.RamdomShapeIndex;

		// move the trigger that will trigger the player shape regen
		ShapeReformTrigger.center = new Vector3(0f, 1f, - (stepSpanLength - 5f));
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			shapeGenerator.FormShape(RandomShapeIndex);
		}
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log("OnCollisionEnter");
		
		if (collision.rigidbody.tag == "Player"){
			Debug.Log("OnCollisionEnter with Player");
		}
	}
}
