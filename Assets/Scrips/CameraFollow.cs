using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float Distance;

	public float Height;

	Transform Target;

	Vector3 CamPosition = new Vector3();
	Vector3 LookAtPosition = new Vector3();

	// Use this for initialization
	void Start () {
		Target = GameObject.FindGameObjectWithTag("Player").transform;
		CamPosition = new Vector3(0,4,0);
		LookAtPosition = new Vector3(0,1,Target.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		CamPosition.y = Height;
		CamPosition.z = Target.transform.position.z - Distance;
		transform.position = CamPosition;

		LookAtPosition.z = Target.transform.position.z;
		transform.LookAt(LookAtPosition);
	}
}
