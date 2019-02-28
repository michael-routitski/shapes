using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCubeManager : MonoBehaviour {

	Rigidbody rb;
	
	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Barrier"){
			SendMessageUpwards("BarrierHit", SendMessageOptions.DontRequireReceiver);
			
			transform.SetParent(other.transform);
			rb.isKinematic = false;
			rb.useGravity = true;
			rb.AddRelativeForce(Random.Range(-5,5),Random.Range(10,20),Random.Range(-5,5), ForceMode.Impulse);
		}
	}

}
