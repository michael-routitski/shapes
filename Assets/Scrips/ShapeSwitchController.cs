using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSwitchController : MonoBehaviour {

	public GameObject[] Shapes;

	public int CurrentIndex = -1;
	public int NextIndex = -1;

	public bool IsInTransition = false;

	// Use this for initialization
	void Start () {
		CurrentIndex = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (IsInTransition){
			//make the shape smaller first
			//once its halffize frow the next shape
			if (Shapes[CurrentIndex].transform.localScale.x > 0.5){
				
				Shapes[CurrentIndex].transform.localScale = Shapes[CurrentIndex].transform.localScale * 0.95f;
			}
			else {
				Shapes[NextIndex].transform.localScale = Shapes[NextIndex].transform.localScale * 1.05f;
			}
			if (Shapes[NextIndex].transform.localScale.x >= 1){
				//stop growing the next chape
				Shapes[NextIndex].transform.localScale = Vector3.one;

				// thats is, finished 
				CurrentIndex = NextIndex;
				NextIndex = (Shapes.Length - 1 == CurrentIndex) ? 0 : CurrentIndex + 1;

				IsInTransition = false;
			}
		}
	}

	public void Transit(){
		IsInTransition = true;
	}
}
