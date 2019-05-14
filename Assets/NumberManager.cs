using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour {

	public GameObject[] Templates;

	public GameObject[][] Digits;

	public string Value;

	private string _prevValue;

	public float Spacing;

	public bool NeedsGenerating;

	// Use this for initialization
	void Start () {

		InstantiateTemplates();
	}
	
	void InstantiateTemplates(){

		Digits = new GameObject[4][];

		for (int digitIndex = 0; digitIndex < 4; digitIndex++){
			Digits[digitIndex] = new GameObject[10];
			for (var i = 0; i < Templates.Length; i++){
				InstantiateTemplateDigit(digitIndex, i);
			}
		}
	}

	void InstantiateTemplateDigit(int numberIndex, int i){

		Digits[numberIndex][i] = GameObject.Instantiate(Templates[i]);
		Digits[numberIndex][i].SetActive(false);
		Digits[numberIndex][i].transform.SetParent(transform);
		Digits[numberIndex][i].transform.SetX(numberIndex * Spacing);
	}

	void OnGUI(){
		if (NeedsGenerating){
			NeedsGenerating = false;
			Generate();
		}
	}

	void Update () {
		
		if (_prevValue != Value){
			_prevValue = Value;
			Generate();
		}
	}

	void Generate(){

		int digit;

		for (int digitIndex = 0; digitIndex < 4; digitIndex++){
			if (Value.Length > digitIndex){
				digit = int.Parse(Value[digitIndex].ToString());
				ShowDigit(digitIndex, digit);
			}
			else {
				ShowDigit(digitIndex, null);
			}
		}
	}

	void HideAllDigits(){
		for (int digitIndex = 0; digitIndex < 4; digitIndex++){
			for (int digit = 0; digit < 4; digit++){
				Digits[digitIndex][digit].SetActive(false);
			}
		}
	}

	void ShowDigit(int digitIndex, int? digit){
		if (digit.HasValue){
			Digits[digitIndex][digit.Value].SetActive(true);
		}
	}
}
