using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

	BarrierGenerator generator;

	BoxCollider ShapeReformTrigger;

	public int RandomShapeIndex = -1;

	ShapeGenerator shapeGenerator;

	// Use this for initialization
	public void Reset (int numLanes) {
		if (!generator){
			generator = GetComponent<BarrierGenerator>();
		}
		
		generator.Reset(numLanes);

		if (!ShapeReformTrigger){
			ShapeReformTrigger = GetComponent<BoxCollider>();
		}
		
		if (!shapeGenerator){
			shapeGenerator = GameObject.FindWithTag("Player").GetComponentInChildren<ShapeGenerator>();
		}
	}
	
	public void Regenerate(float stepSpanLength, Material material){

		generator.Generate(material);

		// Get this for player shape regen
		RandomShapeIndex = generator.RamdomShapeIndex;

		// move the trigger that will trigger the player shape regen
		ShapeReformTrigger.center = new Vector3(0f, 1f, - (stepSpanLength - 5f));
	}

	// Update is called once per frame
	public void SetNumLanes (int numLanes) {
		generator.SetNumLanes(numLanes);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			shapeGenerator.FormShape(RandomShapeIndex);
			SendMessageUpwards("HandleSugarCubePass", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log("OnCollisionEnter");
		
		if (collision.rigidbody.tag == "Player"){
			Debug.Log("OnCollisionEnter with Player");
		}
	}
}
