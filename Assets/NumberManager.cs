using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour {

	public GameObject[] Templates;

	public GameObject[][] Digits;

	public MeshRenderer[][] DigitsRenderers;

	public string Value;

	private string _prevValue;

	public float Spacing;

	public bool NeedsGenerating;

	int MAX_LENGTH = 4;

	// Use this for initialization
	void Start () {

		InstantiateTemplates();
		Generate();
	}
	
	void InstantiateTemplates(){

		Digits = new GameObject[MAX_LENGTH][];
		DigitsRenderers = new MeshRenderer[MAX_LENGTH][];

		for (int digitIndex = 0; digitIndex < MAX_LENGTH; digitIndex++){
			Digits[digitIndex] = new GameObject[10];
			DigitsRenderers[digitIndex] = new MeshRenderer[10];
			for (var i = 0; i < Templates.Length; i++){
				InstantiateTemplateDigit(digitIndex, i);
			}
		}
	}

	void InstantiateTemplateDigit(int numberIndex, int i){

		Digits[numberIndex][i] = GameObject.Instantiate(Templates[i]);
		
		Digits[numberIndex][i].transform.SetParent(transform);
		
		Digits[numberIndex][i].transform.localPosition = new Vector3(
			numberIndex * Spacing, 
			Templates[i].transform.localPosition.y, 
			0);
		
		Digits[numberIndex][i].transform.localScale = Vector3.one;

		Digits[numberIndex][i].name = string.Format("Digit_{0}_{1}", numberIndex, i);
		
		DigitsRenderers[numberIndex][i] = Digits[numberIndex][i].GetComponent<MeshRenderer>();

		DigitsRenderers[numberIndex][i].enabled = false;
	}

	void Update () {
		
		if (_prevValue != Value){
			_prevValue = Value;
			Generate();
		}
	}

	void Generate(){

		int digit; 

		HideAllDigits();

		// Validate that the value is integer
		int res = 0;
		if (Value == null || 
			Value.Length == 0 || 
			!int.TryParse(Value, out res)){

			return;
		}

		for (int digitIndex = 0; digitIndex < MAX_LENGTH; digitIndex++){
			if (Value.Length > digitIndex){

				if (int.TryParse(Value[digitIndex].ToString(), out digit)){
					ShowDigit(digitIndex, digit);
				}
				else {
					ShowDigit(digitIndex, null);
				}
			}
			else {
				ShowDigit(digitIndex, null);
			}
		}
	}

	void HideAllDigits(){
		for (int digitIndex = 0; digitIndex < MAX_LENGTH; digitIndex++){
			for (int digit = 0; digit < 10; digit++){
				DigitsRenderers[digitIndex][digit].enabled = false;
			}
		}
	}

	void ShowDigit(int digitIndex, int? digit){
		if (digit.HasValue){
			DigitsRenderers[digitIndex][digit.Value].enabled = true;
		}
	}
}
