using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrainItem : MonoBehaviour {

	Disease disease;
	public Disease Disease {
		get {
			return disease;
		}
		set {
			disease = value;
			updateTextBackground();
			text.text = disease.name;
		}
	}

	public Image icon;
	public Image textBackground;
	public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateTextBackground () {
		if (disease.isCured) {
			textBackground.color = Color.green;
		} else {
			textBackground.color = Color.red;
		}
	}
}
