using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator : MonoBehaviour {

	public bool NeedsGenerating;

	public bool NeedsReleasing;

	public int ShapeIndex;

	public GameObject[] Cubes;

	void LoadCubes(){
		Cubes = new GameObject[9];
		Cubes[0] = transform.Find("Cubes/Cube_1").gameObject;
		Cubes[1] = transform.Find("Cubes/Cube_2").gameObject;
		Cubes[2] = transform.Find("Cubes/Cube_3").gameObject;
		Cubes[3] = transform.Find("Cubes/Cube_4").gameObject;
		Cubes[4] = transform.Find("Cubes/Cube_5").gameObject;
		Cubes[5] = transform.Find("Cubes/Cube_6").gameObject;
		Cubes[6] = transform.Find("Cubes/Cube_7").gameObject;
		Cubes[7] = transform.Find("Cubes/Cube_8").gameObject;
		Cubes[8] = transform.Find("Cubes/Cube_9").gameObject;
	}

	public void Reset(){
		for (int i=0; i<9; i++){
			Cubes[i].transform.SetParent(transform.GetChild(0));
			Cubes[i].transform.localPosition = Vector3.zero;
			Cubes[i].transform.localRotation = Quaternion.identity;

			var rb = Cubes[i].GetComponent<Rigidbody>();
			rb.isKinematic = true;
			rb.useGravity = false;
		}

		Cubes[0].transform.localPosition = new Vector3(-0.5f, 0.5f, 0);
		Cubes[1].transform.localPosition = new Vector3( 0f,   0.5f, 0);
		Cubes[2].transform.localPosition = new Vector3( 0.5f, 0.5f, 0);
		
		Cubes[3].transform.localPosition = new Vector3(-0.5f, 0, 0);
		Cubes[4].transform.localPosition = new Vector3( 0f,   0, 0);
		Cubes[5].transform.localPosition = new Vector3( 0.5f, 0, 0);
		
		Cubes[6].transform.localPosition = new Vector3(-0.5f, -0.5f, 0);
		Cubes[7].transform.localPosition = new Vector3( 0f,   -0.5f, 0);
		Cubes[8].transform.localPosition = new Vector3( 0.5f, -0.5f, 0);
	}

	public void FormShape(int shapeIndex){
		for (int i=0; i<9; i++){
			Cubes[i].SetActive(Shape.List[shapeIndex].Matrix[i] > 0);
		}
		ShapeIndex = shapeIndex;
	}

	// Use this for initialization
	void Start () {
		LoadCubes();
	}
	
	public void DeparentCubes(float forceForward){
		for (int i=0; i<9; i++){
			if (Cubes[i].transform.parent != transform.GetChild(0)){
				Cubes[i].transform.SetParent(null);

				var rb = Cubes[i].GetComponent<Rigidbody>();
				rb.isKinematic = false;
				rb.useGravity = true;
				rb.AddForce(0,0,forceForward, ForceMode.Impulse);
			}
		}
	}
	
	public void ParentCubes(){
		for (int i=0; i<9; i++){
			Cubes[i].transform.SetParent(transform);
			
		}
	}

	void OnGUI(){
		if (NeedsGenerating){
			NeedsGenerating = false;
			FormShape(ShapeIndex);
		}
	}

}
