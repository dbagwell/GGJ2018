using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Player {Disease, Doctor};

public class TurnManager : MonoBehaviour {
	
	public GameObject passTurnScreen;
	public GameObject passTurnScreenText;

	Player currentPlayer = Player.Disease;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndTurnButtonPressed () {
		ToggleCurrentPlayer();
		// Setup passTurnScreen
		passTurnScreen.SetActive(true);
		// Update map
	}

	public void StartTurnButtonPressed () {
		passTurnScreen.SetActive(false);
	}

	void ToggleCurrentPlayer () {
		Image image = passTurnScreen.GetComponent<Image>();
		Text text = passTurnScreenText.GetComponent<Text>();

		switch (currentPlayer) {
		case Player.Disease: 
			image.color = Color.green;
			text.text = "Doctor's Turn";
			currentPlayer = Player.Doctor; 
			break;

		case Player.Doctor: 
			image.color = Color.red;
			text.text = "Disease's Turn";
			currentPlayer = Player.Disease; 
			break;
		}
	}
}
