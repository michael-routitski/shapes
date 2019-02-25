using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCubeManager : MonoBehaviour {

	Rigidbody rb;
	
	

	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision col){
		//if (col.collider.CompareTag("Barrier")){
		//	transform.SetParent(col.collider.transform);
			//rb.isKinematic = false;
			//rb.useGravity = true;
			//rb.AddRelativeForce(0,-10,0, ForceMode.Impulse);
		//}

	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Barrier"){
			SendMessageUpwards("BarrierHit", SendMessageOptions.RequireReceiver);
			//SendMessageUpwards("DeparentCubes", SendMessageOptions.RequireReceiver);
			
			transform.SetParent(other.transform);
			rb.isKinematic = false;
			rb.useGravity = true;
			rb.AddRelativeForce(Random.Range(-5,5),Random.Range(10,20),Random.Range(-5,5), ForceMode.Impulse);
		}
	}

}
