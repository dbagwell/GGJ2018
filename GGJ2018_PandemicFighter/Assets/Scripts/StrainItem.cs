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
			updateTextBackgroundAndImage();
			text.text = disease.name;
		}
	}

	public Sprite diseaseSprite;
	public Sprite cureSprite;

    public TurnManager tm;
	public Image icon;
	public Image textBackground;
	public Text text;

	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateTextBackgroundAndImage () {
        tm = FindObjectOfType<TurnManager>();
        if (disease.isCured) {
            if (tm.currentPlayer == Player.Disease)
            {
                icon.raycastTarget = false;
                textBackground.raycastTarget = false;
                text.raycastTarget = false;
            }
            textBackground.color = Color.green;
            icon.sprite = cureSprite;
		} else {
			textBackground.color = Color.red;
			icon.sprite = diseaseSprite;
		}
	}
}
